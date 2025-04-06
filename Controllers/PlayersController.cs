using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NBA_ManagementSystem.Models;

namespace NBA_ManagementSystem.Controllers
{
    public class PlayersController : Controller
    {
        private NBAContext db = new NBAContext();

        public ActionResult Index()
        {
            var players = db.Players.Include("Team").ToList();
            return View(players);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Player player = db.Players.Find(id);
            if (player == null) return HttpNotFound();
            return View(player);
        }


        // This is the GET for Create 
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // The POST for Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JerseyNumber,Name,Position,Height,DateOfBirth,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // This is the GET for Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Player player = db.Players.Find(id);
            if (player == null)
                return HttpNotFound();

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // This is the POST for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JerseyNumber,Name,Position,Height,DateOfBirth,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // This is the GET for Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Player player = db.Players.Find(id);
            if (player == null)
                return HttpNotFound();

            return View(player);
        }

        // This is the POST for Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // This loads the search page
        public ActionResult Search()
        {
            var all = db.Players.ToList();
            return View(all);
        }

        
        // This is called by AJAX to return filtered results
        [HttpGet]
        public ActionResult Filter(string query)
        {
            var results = db.Players
                .Where(p => p.Name.Contains(query))
                .ToList();

            return PartialView("_PlayerList", results);
        }


        public JsonResult SearchSuggestions(string query)
        {
            var suggestions = db.Players
                .Where(p => p.Name.Contains(query))
                .Select(p => p.Name)
                .Distinct()
                .Take(5)
                .ToList();

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }
    }
}