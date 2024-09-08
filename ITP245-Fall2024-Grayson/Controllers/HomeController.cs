using ITP245_Fall2024_Grayson.Models;
using ITP245_Fall2024_Grayson.Models.Sports.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Grayson's About Page";
            var about = new About();
            return View(about);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Grayson's Contact Page";
            var contact = new Contact();
            return View(contact);
        }
    }
}