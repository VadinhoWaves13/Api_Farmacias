using Api_Farmacias.Model;
using Microsoft.EntityFrameworkCore;
namespace Api_Farmacias.Database
{
    public class Appdbcontext:DbContext
    {
        
        public DbSet<Farmacia> farmacias{get;set;}
        public DbSet<Localizacao> localizacaos{get;set;}
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:"Host=localhost;Username=postgres;password=vadinhowaves12345;database=farmácia");
        }

       
    }
}
