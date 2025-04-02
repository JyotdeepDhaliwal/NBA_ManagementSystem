using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBA_ManagementSystem.Models;

namespace NBA_ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private NBAContext db = new NBAContext();

        public ActionResult Index()
        {
            var news = new List<string>
            {
                "LeBron James announces retirement after 21 seasons!",
                "Golden State Warriors secure their playoff spot with a clutch win!",
                "Trade rumors: Damian Lillard to join the Miami Heat?",
                "NBA suspends Draymond Green for 3 games after altercation.",
                "Victor Wembanyama wins Rookie of the Year award!"
            };

            var upcomingMatches = new List<string>
            {
                "Lakers vs. Warriors - March 10, 2024",
                "Celtics vs. Heat - March 12, 2024",
                "Bucks vs. Nets - March 14, 2024",
                "Suns vs. Mavericks - March 15, 2024",
                "76ers vs. Nuggets - March 16, 2024"
            };

            var teamStandings = db.Teams
                .OrderByDescending(t => t.GamesWon)
                .Select(t => new
                {
                    t.Name,
                    t.GamesWon,
                    t.GamesLost
                }).ToList();

            ViewBag.News = news;
            ViewBag.UpcomingMatches = upcomingMatches;
            ViewBag.TeamStandings = teamStandings;

            return View();
        }
    }
}