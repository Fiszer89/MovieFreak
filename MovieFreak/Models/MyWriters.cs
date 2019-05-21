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
    [Bind(Exclude="MyWriterID")]
    public class MyWriters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MyWritersID { get; set; }
        public string WriterName { get; set; }

        public virtual List<MyMovie> MyMovieList { get; set; }
    }
}