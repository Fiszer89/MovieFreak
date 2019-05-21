using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFreak.DAL;
using System.Net.Mail;
using System.Net;
using MovieFreak.Models;
using System.Text;

namespace MovieFreak.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Test Form";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.Email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.To.Add("adrianflejszer@gmail.com");
                    msg.Subject = "Contact Us";
                    msg.IsBodyHtml = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("adrianflejszer@gmail.com", "fiszer89");
                    sb.Append("First name: " + c.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.Comment);
                    msg.Body=sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
        return View();
        }
    }
}