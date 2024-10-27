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
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,NickName,Email,Phone,BirthDate,City,ZipCode,ShirtSize,EmergencyContact,EmergencyPhone,IsActivePlayer,IsLeadershipTeamManager")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players.Find(id);

            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,FirstName,LastName,NickName,Email,Phone,BirthDate,City,ZipCode,ShirtSize,EmergencyContact,EmergencyPhone,IsActivePlayer,IsLeadershipTeamManager")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = db.Players.Find(id);

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
