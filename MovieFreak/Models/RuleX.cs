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
    public class RuleX
    {
        public int RuleXID { get; set; }

        public virtual ICollection<MyMovie> MyMovie { get; set; }
        public virtual ICollection<MyRule> MyRule { get; set; }
    }
}