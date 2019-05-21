using MovieFreak.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MovieFreak.DAL
{
    public class MovieContext : DbContext
    {
        public MovieContext()
            : base("DefaultConnection")
        { 

        }
          
        public DbSet<MyMovie> Movies { get; set; }
        public DbSet<UserMovieList> UserMovieLists { get; set; }
        public DbSet<MyActor> Actors { get; set; }
        public DbSet<MyGeneres> Geners { get; set; }
        public DbSet<MyWriters> Writers { get; set; }
        public DbSet<MyGeneresMyMovie> GenereMovie { get; set; }
        public DbSet<MyMovieMyActor> MovieActor { get; set; }
        public DbSet<MyWritersMyMovie> WriterMovie { get; set; }
        public DbSet<DateOfUpdate> Updates { get; set; }
        public DbSet<UserRating> Ratings { get; set; }
        public DbSet<RuleX> RulesX { get; set; }
        public DbSet<RuleY> RulesY { get; set; }
        public DbSet<MyRule> Rules { get; set; }
        public DbSet<RuleXMyMovie> RuleXMovie { get; set; }
        public DbSet<RuleYMyMovie> RuleYMovie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();             
                    
        }
    }
}