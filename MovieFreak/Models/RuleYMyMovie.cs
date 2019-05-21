using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieFreak.Models
{
    public class RuleYMyMovie
    {
        [Key]
        [Column(Order = 0)]
        public int RuleY_RuleYID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MyMovie_MyMovieID { get; set; }

        public virtual MyMovie MyMovie { get; set; }
        public virtual RuleY RuleY { get; set; }
    }
}