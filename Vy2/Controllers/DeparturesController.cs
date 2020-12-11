using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VyBLL;
using VyModels;

namespace Vy2.Controllers
{
    public class DeparturesController : Controller
    {
        private IDepartureLogic _departureBLL;
        private IRouteLogic _routeBLL;

        public DeparturesController()
        {
            _departureBLL = new DepartureLogic();
            _routeBLL = new RouteLogic();
        }

        public DeparturesController(IDepartureLogic departureStub, IRouteLogic routeStub)
        {
            _departureBLL = departureStub;
            _routeBLL = routeStub;
        }

        // GET: Routes/1/Departures
        [Route("Routes/{RouteID}/Departures")]
        public ActionResult Index(int? RouteID)
        {
            ViewBag.RouteID = RouteID;

            var AllDepartures = _departureBLL.GetRouteDepartures((int)RouteID);
            return View(AllDepartures);
        }

        // GET: Routes/1/Departures/2
        [Route("Routes/{RouteID}/Departures/{DepartureID}")]
        public ActionResult Details(int? RouteID, int? DepartureID)
        {
            ViewBag.RouteID = RouteID;

            if (DepartureID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure OneDeparture = _departureBLL.GetDepartureByID((int)DepartureID);
            if (OneDeparture == null)
            {
                return HttpNotFound();
            }
            return View(OneDeparture);
        }

        // GET: Routes/1/Departures/Add
        [Route("Routes/{RouteID}/Departures/Add")]
        public ActionResult Add(int? RouteID)
        {
            var AllStations = _routeBLL.GetAllRouteStations((int)RouteID);
            ViewBag.RouteID = RouteID;
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");

            return View();
        }

        // POST: Routes/1/Departures/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Routes/{RouteID}/Departures/Add")]
        public ActionResult Add(int? RouteID, [Bind(Include = "DepartureID,RouteID,StationID,DepartureTime")] Departure NewDeparture)
        {
            var AllStations = _routeBLL.GetAllRouteStations((int)RouteID);
            ViewBag.RouteID = RouteID;
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");

            if (ModelState.IsValid)
            {
                var DepartureAdded = _departureBLL.AddDeparture((int)RouteID, NewDeparture);
                if (DepartureAdded)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.RouteID = RouteID;
                    return View(NewDeparture);
                }
            }

            ViewBag.RouteID = RouteID;
            return View(NewDeparture);
        }

        // GET: Routes/1/Departures/Edit/2
        [Route("Routes/{RouteID}/Departures/Edit/{DepartureID}")]
        public ActionResult Edit(int? RouteID, int? DepartureID)
        {
            var AllStations = _routeBLL.GetAllRouteStations((int)RouteID);
            ViewBag.RouteID = RouteID;
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");

            if (DepartureID == null && DepartureID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = _departureBLL.GetDepartureByID((int)DepartureID);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // POST: Routes/1/Departures/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Routes/{RouteID}/Departures/Edit/{DepartureID}")]
        public ActionResult Edit(int? RouteID, [Bind(Include = "DepartureID,RouteID,StationID,DepartureTime")] Departure OneDeparture)
        {
            ViewBag.RouteID = RouteID;

            var AllStations = _routeBLL.GetAllRouteStations((int)RouteID);
            ViewBag.RouteID = RouteID;
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");

            if (ModelState.IsValid)
            {
                var DepartureEdited = _departureBLL.EditDeparture((int)RouteID, OneDeparture);
                if (DepartureEdited)
                    return RedirectToAction("Index");
                return View(OneDeparture);
            }
            return View(OneDeparture);
        }

        // GET: Routes/1/Departures/Delete/2
        [Route("Routes/{RouteID}/Departures/Delete/{DepartureID}")]
        public ActionResult Delete(int? RouteID, int? DepartureID)
        {
            ViewBag.RouteID = RouteID;

            if (DepartureID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var OneDeparture = _departureBLL.GetDepartureByID((int)DepartureID);
            if (OneDeparture == null)
            {
                return HttpNotFound();
            }
            return View(OneDeparture);
        }

        // POST: Routes/1/Departures/Delete/2
        [Route("Routes/{RouteID}/Departures/Delete/{DepartureID}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? RouteID, int? DepartureID)
        {
            ViewBag.RouteID = RouteID;

            var DepartureDeleted = _departureBLL.DeleteDeparture((int)RouteID, (int)DepartureID);
            return RedirectToAction("Index");
        }
    }
}