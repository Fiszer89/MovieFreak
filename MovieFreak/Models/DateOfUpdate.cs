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
    public class DateOfUpdate
    {
        public int DateOfUpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}