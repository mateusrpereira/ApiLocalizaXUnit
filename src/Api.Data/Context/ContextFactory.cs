using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //var connectionString = "Persist Security Info=True;Server=localhost;Database=dbapi4;User=root;Password=root";
            var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=dbapi4;MultipleActiveResultSets=true;User ID=sa;Password=Sql@123;TrustServerCertificate=True";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(connectionString));

            return new MyContext(optionsBuilder.Options);
        }
    }
}
