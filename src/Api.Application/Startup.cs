using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Api.CrossCutting.Mappings;
using AutoMapper;
using System.Reflection;
using Api.Data.Context;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlServerConnection = string.Empty;

            if (_environment.IsEnvironment("Testing"))
            {
                if (Environment.GetEnvironmentVariable("DB_TYPE") == null)
                    sqlServerConnection = "Server=localhost\\SQLEXPRESS;Database=dbapi4;Trusted_Connection=True;User Id=sa;Password=Sql@123;TrustServerCertificate=True;" ?? string.Empty;
                else
                    sqlServerConnection = @$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbtestapi;Integrated Security=True;";
            }
            else
                sqlServerConnection = "Server=localhost\\SQLEXPRESS;Database=dbapi4;Trusted_Connection=True;User Id=sa;Password=Sql@123;TrustServerCertificate=True;" ?? string.Empty;

            //Environment.SetEnvironmentVariable("DB_CONNECTION", "Server=localhost;Port=3306;Database=dbAPI_Integration;Uid=root;Pwd=root");
            Environment.SetEnvironmentVariable("DB_CONNECTION", sqlServerConnection);
            //Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
            Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");
            Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
            Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
            Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
            Environment.SetEnvironmentVariable("Seconds", "28800");


            //Injeção de dependência
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signinConfigurations = new SigningConfigurations();
            services.AddSingleton(signinConfigurations);

            // var tokenConfigurations = new TokenConfigurations();
            // new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //     Configuration.GetSection("TokenConfigurations"))
            //     .Configure(tokenConfigurations);
            // services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signinConfigurations.Key;
                // paramsValidation.ValidAudience = tokenConfigurations.Audience;
                // paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api Localiza - POSTECH",
                    Description = ".NET 7.0 / Modelagem DDD",
                    TermsOfService = new Uri("https://github.com/mateusrpereira"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mateus Ramos Pereira",
                        Email = "pereira.mateusramos@gmail.com",
                        Url = new Uri("https://github.com/mateusrpereira")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Termo de Licença de Uso",
                        Url = new Uri("https://github.com/mateusrpereira")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Localiza com .NET 7.0 / DDD - POSTECH");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            //app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
