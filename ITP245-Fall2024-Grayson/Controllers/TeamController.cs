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

        // GET: Team/Create
        public ActionResult Create()
        {
            // Prepare data needed for the view (e.g., divisions list)
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name");
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
            // If model state is invalid, redisplay form with validation errors
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
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
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
