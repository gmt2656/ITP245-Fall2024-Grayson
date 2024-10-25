using ITP245_Fall2024_GraysonModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class TeamController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Team
        public ActionResult Index()
        {
            var teams = db.Teams
                .Include(t => t.Division)
                .Include(t => t.Player)
                .Include(t => t.Player1)
                .ToList();

            return View(teams);
        }


        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var team = db.Teams
                .Include(t => t.Division)
                .Include(t => t.Player)    // Manager
                .Include(t => t.Player1)   // Assistant Manager
                .Include(t => t.Rosters.Select(r => r.Player)) // Players in the team
                .Include(t => t.Games.Select(g => g.Team1))    // Home games, include Visitor Team
                .Include(t => t.Games1.Select(g => g.Team))    // Away games, include Home Team
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Details", team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            PopulateViewBags();
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DivisionId,ManagerID,AssistantManagerID,ShortName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateViewBags(team);
            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            ViewBag.NameList = new SelectList(db.Teams, "Name", "Name", team.Name);
            ViewBag.ShortNameList = new SelectList(db.Teams, "ShortName", "ShortName", team.ShortName);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId); // Set current Division
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name", team.ManagerID); // Set current Manager
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name", team.AssistantManagerID); // Set current Assistant Manager

            return View(team);
        }


        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. The team was modified or deleted by another user.");
                }
            }

            PopulateViewBags(team);
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Team team = db.Teams
                .Include(t => t.Division)
                .FirstOrDefault(t => t.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams
                .Include(t => t.Games)
                .Include(t => t.Games1)
                .Include(t => t.Rosters)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return HttpNotFound();
            }

            // Remove related Rosters
            foreach (var roster in team.Rosters.ToList())
            {
                db.Rosters.Remove(roster);
            }

            // Remove related Games where team is home
            foreach (var game in team.Games.ToList())
            {
                db.Games.Remove(game);
            }

            // Remove related Games where team is visitor
            foreach (var game in team.Games1.ToList())
            {
                db.Games.Remove(game);
            }

            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Helper method to populate ViewBag for dropdown lists
        private void PopulateViewBags(Team team = null)
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team?.DivisionId);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name", team?.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name", team?.AssistantManagerID);
        }

        // Dispose method to release database resources
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
