using ITP245_Fall2024_GraysonModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class PlayerController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Player
        public ActionResult Index()
        {
            // Fetch all players
            var players = db.Players
                .Include(p => p.Rosters.Select(r => r.Team.Division))
                .ToList();

            return View(players);
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players
                .Include(p => p.Rosters.Select(r => r.Team.Division))
                .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
            {
                return HttpNotFound();
            }

            // Return the _Details view
            return View("_Details", player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            ViewBag.TeamList = new SelectList(db.Teams, "TeamId", "Name");
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player, int[] SelectedTeamIds)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();

                // If teams were selected, create roster entries
                if (SelectedTeamIds != null)
                {
                    foreach (var teamId in SelectedTeamIds)
                    {
                        var roster = new Roster
                        {
                            PlayerId = player.PlayerId,
                            TeamId = teamId
                        };
                        db.Rosters.Add(roster);
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.TeamList = new SelectList(db.Teams, "TeamId", "Name");
            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players
                           .Include(p => p.Rosters)
                           .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
            {
                return HttpNotFound();
            }

            ViewBag.TeamList = new SelectList(db.Teams, "TeamId", "Name");
            ViewBag.SelectedTeamIds = player.Rosters.Select(r => r.TeamId).ToArray();
            return View(player);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Player player, int[] SelectedTeamIds)
        {
            if (ModelState.IsValid)
            {
                // Update player details
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();

                // Update rosters
                // Remove existing rosters for this player
                var existingRosters = db.Rosters.Where(r => r.PlayerId == player.PlayerId).ToList();
                foreach (var roster in existingRosters)
                {
                    db.Rosters.Remove(roster);
                }

                // Add new rosters based on selected teams
                if (SelectedTeamIds != null)
                {
                    foreach (var teamId in SelectedTeamIds)
                    {
                        var roster = new Roster
                        {
                            PlayerId = player.PlayerId,
                            TeamId = teamId
                        };
                        db.Rosters.Add(roster);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamList = new SelectList(db.Teams, "TeamId", "Name");
            ViewBag.SelectedTeamIds = player.Rosters.Select(r => r.TeamId).ToArray();
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players
                           .Include(p => p.Rosters.Select(r => r.Team)) // Include team information
                           .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }


        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var player = db.Players.Find(id);

            if (player == null)
            {
                return HttpNotFound();
            }

            // Remove related Rosters
            var rosters = db.Rosters.Where(r => r.PlayerId == id).ToList();
            foreach (var roster in rosters)
            {
                db.Rosters.Remove(roster);
            }

            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
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
