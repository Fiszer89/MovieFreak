using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieFreak.Models;

namespace MovieFreak.ViewModels
{
    public class RecommendedView
    {
        public IEnumerable<MyGeneres> Generes { get; set; }
        public IEnumerable<MyActor> Actors { get; set; }
        public IEnumerable<MyWriters> Writers { get; set; }
        public IEnumerable<MyMovie> Movies { get; set; }
    }
}