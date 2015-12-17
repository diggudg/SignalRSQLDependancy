using SQLDependancySignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLDependancySignalR.Controllers
{
    public class HomeController : Controller
    {
        Votenotifications vn = null;
        JobInfoRepository objRepo = new JobInfoRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           var a= objRepo.GetData();
            // ViewBag.Message = "Your application description page.";

            return View(a);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Votes()
        {
            vn = new Votenotifications();
            var a = vn.getVoteCount();
            return View(a);
        }
    }
}