using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class DataBase : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataBase(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public DbSet<Credits> credits { get; set; }
        public DbSet<Cast> casts { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Images> images { get; set; }
        public DbSet<Poster> posters { get; set; }
        public DbSet<SpokenLanguage> spokenLanguages { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<ProductionCompany> productionCompanies { get; set; }
        public DbSet<ProductionCountry> productionCountries { get; set; }
        public DbSet<BelongsToCollection> belongsToCollections { get; set; }


    }
}
