using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFreak.DAL;
using MovieFreak.Models;
using Microsoft.AspNet.Identity;

namespace MovieFreak.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private MovieContext db = new MovieContext();

        [HttpPost]
        public ActionResult RateProduct(int id, int rate)
        {
            string userName = User.Identity.GetUserName();
            bool success = false;
            string error = "";
            
            try
            {
                success = RegisterProductVote(id, rate, userName);
            }
            catch (System.Exception ex)
            {
                // get last error
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }

            return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        }

        public UserMovieList UserListAdd(string username)
        {
            UserMovieList UserList = new UserMovieList();
            UserList.UserName = username;
            return UserList;
        }

        public bool RegisterProductVote(int id, int rate, string userName)
        {
            string error = "";
            try
            {
                UserRating UsersRating = new UserRating();
                int listID = 0;

                if (ModelState.IsValid)
                {
                    var UserToAdd = db.UserMovieLists.SingleOrDefault(u => u.UserName == userName);
                    if (UserToAdd == null)
                    {
                        UserMovieList MyList = db.UserMovieLists.Add(UserListAdd(userName));
                        db.SaveChanges();
                        listID = MyList.UserMovieListID;
                    }
                    else
                    {
                        listID = db.UserMovieLists.SingleOrDefault(i => i.UserName == userName).UserMovieListID;
                    }
                }

                if (ModelState.IsValid)
                {
                    var RatingToAdd = db.Ratings.Where(m => m.MyMovieID == id).Where(l => l.UserMovieLIstID == listID).FirstOrDefault();
                    if (RatingToAdd == null)
                    {
                        UsersRating.Rating = rate;
                        UsersRating.MyMovieID = id;
                        UsersRating.UserMovieLIstID = listID;
                        db.Ratings.Add(UsersRating);
                        db.SaveChanges();
                    }
                    else
                    {
                        UsersRating.Rating = rate;
                        db.SaveChanges();
                    }  
                }
            }
            catch (Exception ex)
            {
                // get last error
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }
            return true;
        }
    }
}
