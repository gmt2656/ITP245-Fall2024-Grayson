using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson.Areas.Officers.Controllers
{
    public class TeamsController : Controller
    {
        private SportsEntities db = new SportsEntities();

        // GET: Officers/Teams
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.Division).Include(t => t.Player).Include(t => t.Player1);
            return View(teams.ToList());
        }

        // GET: Officers/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Officers/Teams/Create
        public ActionResult Create()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name");
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "LastName");
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "LastName");
            return View();
        }

        // POST: Officers/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,LogoImage")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.AssistantManagerID);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.ManagerID);
            return View(team);
        }

        // GET: Officers/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.AssistantManagerID);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.ManagerID);
            return View(team);
        }

        // POST: Officers/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Name,DivisionId,ManagerID,AssistantManagerID,ShortName,ImageLocation,LogoImage")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", team.DivisionId);
            ViewBag.AssistantManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.AssistantManagerID);
            ViewBag.ManagerID = new SelectList(db.Players, "PlayerId", "LastName", team.ManagerID);
            return View(team);
        }

        // GET: Officers/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Officers/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
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
