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
    public class MyWritersMyMovie
    {
        [Key]
        [Column(Order = 0)]
        public int MyMovieID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MyWritersID { get; set; }

        public virtual MyMovie MyMovie { get; set; }
        public virtual MyWriters MyWriter { get; set; }
    }
}