using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieFreak.Models
{
    public class MyRule
    {
        public int MyRuleID { get; set; }
        public double Support { get; set; }
        public double Confidene { get; set; }
        public int RuleXID { get; set; }
        public int RuleYID { get; set; }

        public virtual RuleX RuleX { get; set; }
        public virtual RuleY RuleY { get; set; }
    }
}