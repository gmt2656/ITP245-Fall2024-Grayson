using ITP245_Fall2024_GraysonModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class GameController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Game
        public ActionResult Index()
        {
            // Get the list of valid Game Status values
            var statuses = Enum.GetValues(typeof(Status))
                               .Cast<Status>()
                               .Select(s => new SelectListItem
                               {
                                   Text = s.ToString(),
                                   Value = ((int)s).ToString()
                               }).ToList();
            ViewBag.Statuses = statuses;

            // Get the list of all games and order by StatusId
            var games = db.Games
                          .Include("Team")    // Home Team
                          .Include("Team1")   // Visitor Team
                          .Include("Field")
                          .OrderBy(g => g.StatusId)
                          .ToList();

            return View(games);
        }

        // GET: Game/_Index
        public ActionResult _Index(int? id)
        {
            var gamesQuery = db.Games
                               .Include("Team")    // Home Team
                               .Include("Team1")   // Visitor Team
                               .Include("Field")
                               .AsQueryable();

            if (id.HasValue && id.Value != 0)
            {
                // Filter games by the selected Game Status ID
                gamesQuery = gamesQuery.Where(g => (int)g.StatusId == id.Value);
            }

            // Order the games by StatusId
            var games = gamesQuery.OrderBy(g => g.StatusId).ToList();

            return PartialView(games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            var game = db.Games
                         .Include("Team")    // Home Team
                         .Include("Team1")   // Visitor Team
                         .Include("Field")
                         .FirstOrDefault(g => g.GameId == id);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // Optional: Dispose method to release database resources
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
