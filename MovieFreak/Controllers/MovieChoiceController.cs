using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFreak.DAL;
using MovieFreak.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MovieFreak.Controllers
{
    [Authorize]
    public class MovieChoiceController : Controller
    {
        MovieContext db = new MovieContext();

        // GET: MovieChoice       
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            var AllMovies = db.Movies;
            var RandomMovies = Shuffle(AllMovies).Take(20);
            ViewBag.OnePageOfProducts = RandomMovies.ToPagedList(pageNumber, 4);
            AllMovies.RemoveRange(RandomMovies);
            return View();
        }

        //Fisher-Yates algorithm
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> items)
        {
            var result = items.ToArray();
            var r = new Random();
            for (int i = items.Count(); i > 1; i--)
            {
                int j = r.Next(i);
                var t = result[j];
                result[j] = result[i - 1];
                result[i - 1] = t;
            }
            return result;
        }
    }
}