using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieFreak.Models
{
    [Bind(Exclude = "MyActorID")]
    public class MyActor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MyActorID { get; set; }
        public string ActorName { get; set; }

        public virtual List<MyMovie> MyMovieList { get; set; }
    }
}