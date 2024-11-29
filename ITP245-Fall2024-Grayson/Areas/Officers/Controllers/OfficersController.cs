using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Areas.Officers.Controllers
{
    public class OfficersController : Controller
    {
        // GET: Officers/Officers
        public ActionResult Index()
        {
            return View();
        }
    }
}