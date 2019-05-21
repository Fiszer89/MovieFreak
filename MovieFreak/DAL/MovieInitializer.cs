using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieFreak.Models;

namespace MovieFreak.DAL
{
    public class MovieInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            //TestData
            /*
            var Movies = new List<MyMovie>
            {
                new MyMovie{MyMovieID = 0, Title = "Test", Director="test", Rating = 1, ReleaseDate = DateTime.Now, MovieArtUrl = "test"}
            };
            Movies.ForEach(s => context.Movies.Add(s));
            context.SaveChanges();
            var Actors = new List<MyActor>
            {
                new MyActor{MyActorID = 0, ActorName = "test"}
            };
            Actors.ForEach(a => context.Actors.Add(a));
            context.SaveChanges();
            var Writers = new List<MyWriters>
            {
                new MyWriters{MyWritersID = 0, WriterName = "test"}
            };
            Writers.ForEach(w => context.Writers.Add(w));
            context.SaveChanges();
            var Generes = new List<MyGeneres>
            {
                new MyGeneres{MyGeneresID = 0, GenereName = "test"}
            };
            Generes.ForEach(g => context.Geners.Add(g));
            context.SaveChanges();
            var UserListMovies = new List<UserMovieList>();
            */
        }
    }
}