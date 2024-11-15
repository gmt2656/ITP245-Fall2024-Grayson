using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class TeamController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Team
        public ActionResult Index()
        {
            var teams = db.Teams
                          .Include("Division")    // Include related Division by name
                          .Include("Player")      // Include Manager (Player) by name
                          .Include("Player1")     // Include Assistant Manager (Player1) by name
                          .ToList();

            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var team = db.Teams
                         .Include("Division")
                         .Include("Player")      // Include Manager (Player) by name
                         .Include("Player1")     // Include Assistant Manager (Player1) by name
                         .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return View("_Details", team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            // Populate divisions and players for dropdowns
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name");
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name");
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name");

            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,LogoImage")] Team team, HttpPostedFileBase LogoImage)
        {
            if (ModelState.IsValid)
            {
                // Handle File Storage (Logo Image)
                if (LogoImage != null)
                {
                    string fileName = Path.GetFileName(LogoImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    LogoImage.SaveAs(filePath); // Save to the Images folder
                    team.ImageLocation = filePath; // Store file path in ImageLocation
                }

                // Optionally, handle Database Storage (if using binary storage)
                if (LogoImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        LogoImage.InputStream.CopyTo(memoryStream);
                        team.LogoImage = memoryStream.ToArray(); // Store binary data in LogoImage
                    }
                }

                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Re-populate dropdowns in case of validation errors
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name", team.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name", team.AssistantManagerID);

            return View("_Edit", team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var team = db.Teams
                         .Include("Division")
                         .Include("Player")      // Include Manager (Player) by name
                         .Include("Player1")     // Include Assistant Manager (Player1) by name
                         .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return HttpNotFound();
            }

            // Pass the teamId to the view via ViewBag
            ViewBag.TeamId = id;

            // Populate dropdowns for the form
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name", team.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name", team.AssistantManagerID);

            return View("_Edit", team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,LogoImage")] Team team, HttpPostedFileBase LogoImage)
        {
            if (ModelState.IsValid)
            {
                // Handle File Storage (Logo Image)
                if (LogoImage != null && LogoImage.ContentLength > 0)
                {
                    // For file storage (saving to file system)
                    string fileName = Path.GetFileName(LogoImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    LogoImage.SaveAs(filePath); // Save to the Images folder
                    team.ImageLocation = filePath; // Store file path in ImageLocation

                    // For database storage (if binary storage is needed)
                    using (var memoryStream = new MemoryStream())
                    {
                        LogoImage.InputStream.CopyTo(memoryStream);
                        team.LogoImage = memoryStream.ToArray(); // Store binary data in LogoImage field
                    }
                }

                // Update the team in the database
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If validation fails, re-populate dropdowns and return the view
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "Name", team.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "Name", team.AssistantManagerID);

            return View("_Edit", team);
        }


        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            var team = db.Teams
                         .Include("Division")
                         .FirstOrDefault(t => t.TeamId == id);

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
            var team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

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
