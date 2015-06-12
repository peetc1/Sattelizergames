using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sattelizer.Business;
using Sattelizer.Business.Twitter;
using Sattelizer.Models;

namespace Sattelizer.Controllers
{
    public partial class HomeController : Controller
    {


        public virtual ActionResult Index()
        {
            // testing twitter call
            var twitter = new TwitterUtility();

            var tweets = twitter.GetTweets();
            var model = new HomeModel
            {
                TweetInfo = tweets
            };

            return View(model);
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}