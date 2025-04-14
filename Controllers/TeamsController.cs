using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NBA_ManagementSystem.Models;

namespace NBA_ManagementSystem.Controllers
{
    public class TeamsController : Controller
    {
        private NBAContext db = new NBAContext();

        public ActionResult Index()
        {
            var teams = db.Teams.ToList();
            return View(teams);
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Team team = db.Teams.Find(id);
            if (team == null) return HttpNotFound();
            return View(team);
        }


        public ActionResult Create()
        {
            //if (!IsAdmin()) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,City,Telephone,Owner,HeadCoach,GamesWon,GamesLost")] Team team, HttpPostedFileBase logoFile)
        {
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");



            if (ModelState.IsValid)
            {
                if (logoFile != null && logoFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(logoFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    logoFile.SaveAs(path);
                    team.LogoFileName = fileName;
                }

                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        public ActionResult Edit(int? id)
        {
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Team team = db.Teams.Find(id);
            if (team == null)
                return HttpNotFound();

            return View(team);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City,Telephone,Owner,HeadCoach,GamesWon,GamesLost")] Team team, HttpPostedFileBase logoFile)
        {
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");


            if (ModelState.IsValid)
            {
                if (logoFile != null && logoFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(logoFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    logoFile.SaveAs(path);
                    team.LogoFileName = fileName;
                }

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        public ActionResult Delete(int? id)
        {
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Team team = db.Teams.Find(id);
            if (team == null)
                return HttpNotFound();

            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //if (Session["Role"]?.ToString() != "Admin")
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("AccessDenied", "Account");


            Team team = db.Teams.Find(id);

            // Delete the logo file from disk (if exists)
            if (!string.IsNullOrEmpty(team.LogoFileName))
            {
                string logoPath = Path.Combine(Server.MapPath("~/Images/"), team.LogoFileName);
                if (System.IO.File.Exists(logoPath))
                {
                    System.IO.File.Delete(logoPath);
                }
            }

            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }








        // This below is for the teams and players users can make.

        // GET FOR Edit a custom team which adds more players
        public ActionResult EditTeam(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var team = db.UserTeams.Include("Players").FirstOrDefault(t => t.Id == id);

            if (team == null || team.UserId != (int)Session["UserId"])
                return HttpNotFound();

            ViewBag.TeamId = team.Id;
            ViewBag.TeamName = team.TeamName;

            var players = db.UserTeamPlayers.Where(p => p.UserTeamId == team.Id).ToList();

            return View("AddPlayers", players); // tells it to reuse AddPlayers.cshtml
        }


        [HttpPost]
        public ActionResult DeleteTeam(int id)
        {
            var team = db.UserTeams.Find(id);
            if (team == null || team.UserId != (int)Session["UserId"])
                return HttpNotFound();

            db.UserTeams.Remove(team);
            db.SaveChanges();

            return RedirectToAction("MyTeams");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayer(int id)
        {
            var player = db.UserTeamPlayers.Find(id);
            if (player == null)
                return HttpNotFound();

            int teamId = player.UserTeamId;

            db.UserTeamPlayers.Remove(player);
            db.SaveChanges();

            return RedirectToAction("EditTeam", new { id = teamId });
        }


        // GET FOR CreateOwnTeam
        public ActionResult CreateOwnTeam()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOwnTeam(string teamName)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(teamName))
            {
                ViewBag.Error = "Team name is required.";
                return View();
            }

            var userId = (int)Session["UserId"];

            var newTeam = new UserTeam
            {
                TeamName = teamName,
                UserId = userId
            };

            db.UserTeams.Add(newTeam);
            db.SaveChanges();

            return RedirectToAction("AddPlayers", new { id = newTeam.Id });
        }



        // GET FOR AddPlayers
        public ActionResult AddPlayers(int id)
        {
            var team = db.UserTeams.Find(id);
            if (team == null || team.UserId != (int)Session["UserId"])
                return HttpNotFound();

            ViewBag.TeamId = id;
            ViewBag.TeamName = team.TeamName;
            return View();
        }

        [HttpPost]
        public ActionResult AddPlayers(int teamId, string playerName, string position, string height)
        {
            var team = db.UserTeams.Find(teamId);
            if (team == null || team.UserId != (int)Session["UserId"])
                return HttpNotFound();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                ViewBag.TeamId = teamId;
                ViewBag.TeamName = team.TeamName;
                ViewBag.Error = "Player name is required.";
                return View();
            }

            var player = new UserTeamPlayer
            {
                UserTeamId = teamId,
                PlayerName = playerName,
                Position = position,
                Height = height
            };

            db.UserTeamPlayers.Add(player);
            db.SaveChanges();

            return RedirectToAction("AddPlayers", new { id = teamId });
        }

        public ActionResult MyTeams()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = (int)Session["UserId"];
            var teams = db.UserTeams.Where(t => t.UserId == userId).ToList();
            return View(teams);
        }

    }
}