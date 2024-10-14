using ITP245_Fall2024_GraysonModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class TeamController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Team
        public ActionResult Index()
        {
            var teams = db.Teams.ToList();
            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var team = db.Teams
                .Include(t => t.Rosters)
                .Include(t => t.Games)
                .Include(t => t.Games1)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Details", team);
        }
    }
}
