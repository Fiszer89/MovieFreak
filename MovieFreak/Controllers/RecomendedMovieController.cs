using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFreak.Logic;
using MovieFreak.DAL;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using MovieFreak.Models;
using PagedList;
using System.Text;
using MovieFreak.ViewModels;
using System.Net;

namespace MovieFreak.Controllers
{
    [Authorize]
    public class RecomendedMovieController : Controller
    {
        MovieContext db = new MovieContext();

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            
            List<int> moviesToWatch = new List<int>();
            string userName = User.Identity.GetUserName();
            var currentUserId = db.UserMovieLists.Where(u => u.UserName == userName).First().UserMovieListID;
            var currentList = db.Ratings.Where(r => r.UserMovieLIstID == currentUserId).Select(r => r.MyMovieID).ToList();

            var result = from rules in db.Rules
                         join ruleX in db.RulesX on rules.RuleXID equals ruleX.RuleXID
                         join ruleY in db.RulesY on rules.RuleYID equals ruleY.RuleYID
                         join xMovie in db.RuleXMovie on ruleX.RuleXID equals xMovie.RuleX_RuleXID
                         join yMovie in db.RuleYMovie on ruleY.RuleYID equals yMovie.RuleY_RuleYID
                         select new { id1 = ruleX.RuleXID, id2 = ruleY.RuleYID };

            foreach(var r in result)
            {
                if (ContainsAllItems(currentList, db.RuleXMovie.Where(x => x.RuleX_RuleXID == r.id1).Select(x => x.MyMovie_MyMovieID).ToList()))
                {
                    foreach(var m in db.RuleYMovie.Where(x => x.RuleY_RuleYID == r.id2).Select(x => x.MyMovie_MyMovieID))
                    {
                        moviesToWatch.Add(m);
                    }
                }
            }
            /*
            foreach(var r in allRules)
            {
                if(ContainsAllItems(currentList, r.X))
                {
                    foreach (var m in r.Y)
                    {
                        moviesToWatch.Add(m);
                    }
                }
            }
            */
            moviesToWatch.Distinct();
      
            List<MyMovie> myMoviesToWatch = new List<MyMovie>();
            var dbMovies = db.Movies.Select(m => m).Distinct();
            foreach(var m in moviesToWatch)
            {
                var movie = db.Movies.Find(m);
                myMoviesToWatch.Add(movie);
            }

            var viewModel = new RecommendedView();
            try
            {
                viewModel.Movies = db.Movies;
                    //.Include(a => a.MoviesActors)
                    //.Include(g => g.MoviesGeneres);
                    //.Include(w => w.MovieWriters);
            }
            catch (Exception e)
            {
                //throw e;
            }

            var listOfRecomendedMovies = myMoviesToWatch.Distinct().ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfProducts = listOfRecomendedMovies;
            ViewBag.Movies = myMoviesToWatch;
            return View(viewModel);
        }

        public static bool ContainsAllItems(List<int> a, List<int> b)
        {
            return !b.Except(a).Any();
        }
    }
}