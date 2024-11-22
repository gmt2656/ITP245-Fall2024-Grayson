using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ITP245_Fall2024_GraysonModel;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.UI.WebControls;

namespace ITP245_Fall2024_Grayson.Controllers
{
    public class TeamController : Controller
    {
        private readonly SportsEntities db = new SportsEntities();

        // GET: Team
        public ActionResult Index()
        {
            ViewBag.ImagePath = ConfigurationManager.AppSettings["LogoImageLocation"];

            var teams = db.Teams
                          .Include(t => t.Division)    // Use lambda expressions for clarity
                          .Include(t => t.Player)      // Include Manager (Player)
                          .Include(t => t.Player1)     // Include Assistant Manager (Player1)
                          .ToList();

            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var team = db.Teams
                         .Include(t => t.Division)
                         .Include(t => t.Player)      // Include Manager (Player)
                         .Include(t => t.Player1)     // Include Assistant Manager (Player1)
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
            ViewBag.DivisionId = new SelectList(db.Divisions.ToList(), "DivisionId", "Name");
            ViewBag.ManagerID = new SelectList(db.Players.ToList(), "PlayerId", "Name");
            ViewBag.AssistantManagerID = new SelectList(db.Players.ToList(), "PlayerId", "Name");

            // Return the view with an empty team model
            var team = new Team(); // Assuming Team is the model class
            return View(team);
        }


        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,FileName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);

                if (team.FileName != null)
                {
                    team.ImageLocation = UploadImage(team.FileName); // Upload the image and set the path
                    team.LogoImage = ConvertToBytes(team.FileName);  // Convert the file to binary data
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Repopulate ViewBag in case of validation failure
            var divisions = db.Divisions.ToList();
            var players = db.Players.ToList();

            ViewBag.DivisionId = new SelectList(divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.ManagerID = new SelectList(players, "PlayerId", "Name", team.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(players, "PlayerId", "Name", team.AssistantManagerID);

            return View(team);  // Return the team with validation messages
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var team = db.Teams
                .Include(t => t.Division)  // Including the related Division
                .FirstOrDefault(t => t.TeamId == id);  // We only need the Division, no need for Player or Player1

            if (team == null)
            {
                return HttpNotFound();
            }

            // Get the list of Divisions and Players for the dropdowns
            var divisions = db.Divisions.ToList();
            var players = db.Players.ToList();  // This gets the full list of players for the Manager and Assistant Manager dropdowns

            // Populate the ViewBag with the appropriate SelectList for the dropdowns
            ViewBag.DivisionId = new SelectList(divisions, "DivisionId", "Name", team.DivisionId); // Set the selected value based on the team's current DivisionId
            ViewBag.ManagerID = new SelectList(players, "PlayerId", "Name", team.ManagerID); // Set the selected value based on the team's current ManagerID
            ViewBag.AssistantManagerID = new SelectList(players, "PlayerId", "Name", team.AssistantManagerID); // Set the selected value based on the team's current AssistantManagerID

            ViewBag.ImagePath = ConfigurationManager.AppSettings["LogoImageLocation"];

            return View(team);  // Pass the team to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,FileName")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;

                if (team.FileName != null)
                {
                    team.ImageLocation = UploadImage(team.FileName); // Upload the image and set the path
                    team.LogoImage = ConvertToBytes(team.FileName);  // Convert the file to binary data
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Repopulate ViewBag in case of validation failure
            var divisions = db.Divisions.ToList();
            var players = db.Players.ToList();

            ViewBag.DivisionId = new SelectList(divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.ManagerID = new SelectList(players, "PlayerId", "Name", team.ManagerID);
            ViewBag.AssistantManagerID = new SelectList(players, "PlayerId", "Name", team.AssistantManagerID);

            return View(team);  // Return the team with validation messages
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            var team = db.Teams
                         .Include(t => t.Division)
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

        private string UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Get the file name and create a path to save it
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Images/"), fileName);

                // Save the file to the server
                file.SaveAs(filePath);

                // Return the relative path for storing in the database
                return "~/Images/" + fileName;
            }

            return null; // Return null if no file is uploaded
        }

        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.InputStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }

            return null; // Return null if no file is uploaded
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
