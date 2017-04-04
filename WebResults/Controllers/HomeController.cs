using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebResults.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.Title = "Categories";
            return PartialView();
        }

        public ActionResult Course()
        {
            ViewBag.Title = "Courses";
            return PartialView();
        }

        public ActionResult Top()
        {
            ViewBag.Title = "Top";
            return PartialView();
        }
    }
}
