using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VyBLL;
using VyModels;

namespace Vy2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["NewTicket"] = null;
            Session["LoggedIn"] = false;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(Ticket OneTicket)
        {
            return View(OneTicket);
        }

        public ActionResult Route()
        {
            Ticket NewTicket = (Ticket)Session["NewTicket"];
            return View(NewTicket);
        }

        [HttpPost]
        public ActionResult Route(Ticket Ticket)
        {
            var RouteBLL = new RouteLogic();
            var OneTicket = RouteBLL.GetTicketWithRoute(Ticket);
            Session["NewTicket"] = OneTicket;
            Ticket ticket = (Ticket)Session["NewTicket"];
            return RedirectToAction("Route", "Home");
        }

        // Make the ticket object available to JavaScript
        public string RouteJSON()
        {
            Ticket ticket = (Ticket)Session["NewTicket"];
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(ticket);
            return json;
        }

        public string StationsJson()
        {
            var stationBLL = new StationLogic();
            var AllStations = stationBLL.GetAllStationNames();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(AllStations);
            return json;
        }

        [HttpGet]
        public ActionResult Receipt(int id)
        {
            var TicketBLL = new TicketLogic();
            var OneTicket = TicketBLL.GetTicketByID(id);
            return View(OneTicket);
        }

        [HttpPost]
        public ActionResult Receipt(Ticket t)
        {
            var TicketBLL = new TicketLogic();

            int newTikcetID = TicketBLL.SaveTicket(t);
            if (newTikcetID != 0)
            {
                return Json(new { id = newTikcetID, newurl = Url.Action("Receipt") });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Routes()
        {
            var RouteBLL = new RouteLogic();
            var AllRoutes = RouteBLL.GetAllRoutes();
            return View(AllRoutes);
        }
    }
}