using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class TeamController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Team
        public ActionResult Index()
        {
            ViewBag.ImagePath = ConfigurationManager.AppSettings["LogoImageLocation"];
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
            return View(new Team());
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation")] Team team, HttpPostedFileBase TeamLogo)
        {
            if (ModelState.IsValid)
            {
                if (TeamLogo != null && TeamLogo.ContentLength > 0)
                {
                    // Save the file to the file system
                    var filePath = Path.Combine(Server.MapPath("~/Images/TeamLogos"), Path.GetFileName(TeamLogo.FileName));
                    TeamLogo.SaveAs(filePath);

                    // Save the file path to the database
                    team.ImageLocation = Path.GetFileName(TeamLogo.FileName);
                }

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
            ViewBag.ImagePath = ConfigurationManager.AppSettings["LogoImageLocation"];
            PopulateViewBags(team);
            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation")] Team team, HttpPostedFileBase TeamLogo)
        {
            if (ModelState.IsValid)
            {
                if (TeamLogo != null && TeamLogo.ContentLength > 0)
                {
                    var filePath = Path.Combine(Server.MapPath("~/Images/TeamLogos"), Path.GetFileName(TeamLogo.FileName));
                    TeamLogo.SaveAs(filePath);

                    team.ImageLocation = Path.GetFileName(TeamLogo.FileName);
                }

                // Reload the entity from the database
                var dbTeam = db.Teams.Find(team.TeamId);
                if (dbTeam != null)
                {
                    db.Entry(dbTeam).CurrentValues.SetValues(team);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the entity was not found
                    return HttpNotFound();
                }
            }

            PopulateViewBags(team);
            return View(team);
        }

        private string UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                    var ext = Path.GetExtension(file.FileName).ToLower();
                    var imagePath = ConfigurationManager.AppSettings["LogoImageLocation"];
                    var path = Path.Combine(Server.MapPath(imagePath), Path.GetFileName(file.FileName));

                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        return "File uploaded successfully";
                    }
                    else
                    {
                        return "Please use file extensions JPG and PNG.";
                    }
                }
                catch (Exception ex)
                {
                    return "ERROR: " + ex.Message;
                }
            }
            return "No file selected.";
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = db.Teams
                .Include(t => t.Division)
                .FirstOrDefault(t => t.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }

            ViewBag.ImagePath = ConfigurationManager.AppSettings["LogoImageLocation"];
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var team = db.Teams
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

        // Action to display team image
        public ActionResult DisplayImage(int id)
        {
            var team = db.Teams.Find(id);
            if (team != null && !string.IsNullOrEmpty(team.ImageLocation))
            {
                // Get the file path
                var filePath = Path.Combine(Server.MapPath("~/Images/TeamLogos"), team.ImageLocation);

                // Check if the file exists
                if (System.IO.File.Exists(filePath))
                {
                    // Return the image file
                    return File(filePath, "image/jpeg");
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}
