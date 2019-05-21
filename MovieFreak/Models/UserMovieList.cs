using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace MovieFreak.Models
{
    public class UserMovieList
    {
        public int UserMovieListID { get; set; }
        public string UserName { get; set; }

        public virtual List<UserRating> UsersRtings { get; set; }
    }
}