using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NBA_ManagementSystem.Models;

namespace NBA_ManagementSystem.Controllers
{
    public class TradesController : Controller
    {
        private NBAContext db = new NBAContext();

        public ActionResult Index()
        {
            return View(db.Trades.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Trade trade = db.Trades.Find(id);
            if (trade == null) return HttpNotFound();
            return View(trade);
        }
    }
}