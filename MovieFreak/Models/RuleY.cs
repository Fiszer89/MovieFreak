using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieFreak.Models
{
    public class RuleY
    {
        public int RuleYID { get; set; }

        public virtual ICollection<MyMovie> MyMovie { get; set; }
        public virtual ICollection<MyRule> MyRule { get; set; }
    }
}