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
    public class RuleXMyMovie
    {
        [Key]
        [Column(Order = 0)]
        public int RuleX_RuleXID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MyMovie_MyMovieID { get; set; }

        public virtual MyMovie MyMovie { get; set; }
        public virtual RuleX RuleX { get; set; }
    }
}