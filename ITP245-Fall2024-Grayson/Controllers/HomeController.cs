using ITP245_Fall2024_Grayson.Models;
using ITP245_Fall2024_Grayson.Models.Sports.cs;
using ITP245_Fall2024_GraysonModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new SportsEntities())
            {
                // Create a dictionary to hold Teams and Active Players
                var homePage = new Dictionary<string, List<ISports>>();

                // Get all Teams from the database
                var teams = db.Teams.OrderBy(t => t.Name).ToList<ISports>();

                // Get only active Players (where IsActive is true)
                var activePlayers = db.Players
                    .Where(p => p.IsActivePlayer)  // Filter to include only active players
                    .OrderBy(p => p.LastName) // Optionally order by LastName
                    .ToList<ISports>();

                // Add Teams and Active Players to the dictionary
                homePage.Add("Teams", teams);
                homePage.Add("Active Players", activePlayers);

                // Pass the dictionary to the View
                return View(homePage);
            }
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
