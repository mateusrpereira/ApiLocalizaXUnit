using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    //Fábrica de contexto para criar: bd, tabela, migração...
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para Criar as Migrações

            //var connectionString = "Persist Security Info=True;Server=localhost;Port=3306;Database=dbapi4;Uid=root;Pwd=root";
            var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=dbapi4;MultipleActiveResultSets=true;User ID=sa;Password=Sql@123;TrustServerCertificate=True";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(connectionString));

            return new MyContext(optionsBuilder.Options);
        }
    }
}
