﻿using System.Web.Mvc;

namespace Gite.WebSite.Controllers
{
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Activites()
        {
            return View();
        }

        public ActionResult Description()
        {
            return View();
        }

        public ActionResult Souvenirs()
        {
            return View();
        }
    }
}