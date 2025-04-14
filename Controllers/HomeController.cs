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
                "Lakers vs. Warriors - April 19, 2025",
                "Celtics vs. Heat - April 19, 2025",
                "Bucks vs. Nets - April 20, 2025",
                "Suns vs. Mavericks - April 20, 2025",
                "76ers vs. Nuggets - April 21, 2025"
            };
            var funFacts = new List<string>
            {
                "The first NBA game was played on November 1, 1946, between the Toronto Huskies and the New York Knicks.",
                "Michael Jordan won 6 NBA championships with the Chicago Bulls during the 1990s.",
                "LeBron James was the youngest player in NBA history to score 30,000 career points.",
                "The Boston Celtics and Lakers are tied for most championships with 17 each.",
                "Wilt Chamberlain once scored 100 points in a single game.",
                "The NBA logo is based on a silhouette of Jerry West.",
                "Stephen Curry made 402 3-pointers in a single season.",
                "The shot clock was introduced in the 1954-55 season.",
                "The NBA’s first female referee was Violet Palmer in 1997.",
                "The NBA All-Star Game’s MVP trophy is named after Kobe Bryant."
            };

            var random = new Random();
            string selectedFact = funFacts[random.Next(funFacts.Count)];

            ViewBag.News = news;
            ViewBag.UpcomingMatches = upcomingMatches;
            ViewBag.FunFact = selectedFact;

            return View();
        }
    }
}