using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieFreak.Models;

namespace MovieFreak.ViewModels
{
    public class RatingView
    {
        public IEnumerable<MyMovie> Movies { get; set; }
        public IEnumerable<UserMovieList> Lists { get; set; }
        public IEnumerable<UserRating> Ratings { get; set; }
    }
}