using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            serviceCollection.AddScoped<IUfRepository, UfImplementation>();
            serviceCollection.AddScoped<IMunicipioRepository, MunicipioImplementation>();
            serviceCollection.AddScoped<ICepRepository, CepImplementation>();

            //var connectionString = "Server=localhost;Database=dbAPI;Uid=root;Pwd=root";
            //serviceCollection.AddDbContext<MyContext>(
            //   options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            //);

            // serviceCollection.AddDbContext<MyContext>(
            //         options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"),
            //         ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DB_CONNECTION")))
            //     );

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }
            else
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"),
                    new MySqlServerVersion(ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DB_CONNECTION"))))
                );
            }

            //"Server=.\\SQLEXPRESS;Initial Catalog=dbapi;MultipleActiveResultSets=true;User ID=sa;Password=sql@123;TrustServerCertificate=True"

            // serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(
            //         Environment.GetEnvironmentVariable("DB_CONNECTION"),
            //         new MySqlServerVersion(ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DB_CONNECTION")))));
        }
    }
}
