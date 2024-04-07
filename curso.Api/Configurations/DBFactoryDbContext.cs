using curso.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.Api.Configurations
{
    public class DBFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        /// <summary>
        ///  //PM> Add-Migration Base-inicial
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=SERVER;Database=Curso;Trusted_Connection=True;Encrypt=False");
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);
            return contexto;
           
        }
    }
}
