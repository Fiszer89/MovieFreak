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
    [Bind(Exclude = "MyMovieID")]
    public class MyMovie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MyMovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public Decimal Rating { get; set; }
        [DisplayName("Movie Art URL")]
        public string MovieArtUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual ICollection<MyActor> MoviesActors { get; set; }
        public virtual ICollection<MyGeneres> MoviesGeneres { get; set; }
        public virtual ICollection<MyWriters> MovieWriters { get; set; }
        public virtual ICollection<UserRating> MovieRatings { get; set; }
        public virtual ICollection<RuleX> RuleX { get; set; }
        public virtual ICollection<RuleY> RuleY { get; set; }
    }
}