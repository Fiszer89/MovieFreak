using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFreak.Models
{
    public class UserRating
    {
        public int UserRatingID { get; set; }
        public double Rating { get; set; }
        public int UserMovieLIstID { get; set; }
        public int MyMovieID { get; set; }

        public virtual MyMovie MyMovie { get; set; }
        public virtual UserMovieList UsetMovieList { get; set; }
    }
}