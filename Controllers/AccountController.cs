using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NBA_ManagementSystem.Models;

namespace NBA_ManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private NBAContext db = new NBAContext();

        // GET For Register
        public ActionResult Register()
        {
            return View();
        }

        // POST For  Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // I have it so that it will check for duplicates
                if (db.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
                {
                    ModelState.AddModelError("", "Username or Email already taken.");
                    return View(user);
                }

                //user.PasswordHash = HashPassword(user.PasswordHash);
                user.PasswordHash = HashPassword(user.Password);
                db.Users.Add(user);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var e in ex.EntityValidationErrors)
                    {
                        foreach (var ve in e.ValidationErrors)
                        {
                            ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            return View(user);
        }

        // GET For Login
        public ActionResult Login()
        {
            return View();
        }

        // POST For Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                return RedirectToAction("CreateOwnTeam", "Teams");
            }

            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View();
        }

        // GET For Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult CreateOwnTeam()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }


        // Hash password function
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}