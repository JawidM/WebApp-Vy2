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
    public class RoutesController : Controller
    {
        private IRouteLogic _routeBLL;
        private IStationLogic _stationBLL;

        public RoutesController()
        {
            _routeBLL = new RouteLogic();
            _stationBLL = new StationLogic();
        }

        public RoutesController(IRouteLogic routeStub, IStationLogic stationStub)
        {
            _routeBLL = routeStub;
            _stationBLL = stationStub;
        }

        // GET: Routes
        public ActionResult Index()
        {
            var AllRoutes = _routeBLL.GetAllRoutes();
            return View(AllRoutes);
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? RouteID)
        {
            Route OneRoute = _routeBLL.GetRouteByID(RouteID);

            return View(OneRoute);
        }

        // GET: Routes/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Routes/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "RouteID,RouteName")] Route NewRoute)
        {
            if (ModelState.IsValid)
            {
                var RouteAdded = _routeBLL.AddRoute(NewRoute);
                if (RouteAdded == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(NewRoute);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? RouteID)
        {
            if (RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = _routeBLL.GetRouteByID(RouteID);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouteID,RouteName")] Route OneRoute)
        {
            if (ModelState.IsValid)
            {
                var RouteEdited = _routeBLL.EditRoute(OneRoute);
                if (RouteEdited)
                {
                    return RedirectToAction("Index");
                }
                return View(OneRoute);
            }
            return View(OneRoute);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? RouteID)
        {
            if (RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = _routeBLL.GetRouteByID(RouteID);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int RouteID)
        {
            Route OneRoute = _routeBLL.GetRouteByID(RouteID);

            var IsDeleted = _routeBLL.DeleteRoute(RouteID);
            if (IsDeleted)
                return RedirectToAction("Index");
            else
                return View(OneRoute);
        }

        // GET: Routes/1/Stations/Add
        [Route("Routes/{RouteID}/Stations/Add")]
        public ActionResult AddStation(int? RouteID)
        {
            ViewBag.RouteID = RouteID;

            var AllStations = _stationBLL.GetAllStations();
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");

            return View();
        }

        // POST: Routes/1/Stations/Add
        [Route("Routes/{RouteID}/Stations/Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStation(int? RouteID, [Bind(Include = "StationID,StationName,StationNumber")] Station NewStation)
        {
            ViewBag.RouteID = RouteID;

            var AllStations = _stationBLL.GetAllStations();
            ViewBag.StationID = new SelectList(AllStations, "StationID", "StationName");
            System.Diagnostics.Debug.WriteLine("NewStation");
            System.Diagnostics.Debug.WriteLine(NewStation.StationID);
            System.Diagnostics.Debug.WriteLine(NewStation.StationNumber);
            System.Diagnostics.Debug.WriteLine(NewStation.StationName);

            System.Diagnostics.Debug.WriteLine("RouteID");
            System.Diagnostics.Debug.WriteLine(RouteID);

            if (RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var StationAdded = _routeBLL.AddRouteStation((int)RouteID, NewStation);
                if (StationAdded)
                {
                    return RedirectToAction("Details", "Routes", new { RouteID });
                }
                return View(NewStation);
            }

            return View(NewStation);
        }

        // GET: Routes/1/stations/5
        [Route("Routes/{RouteID}/Stations/Edit/{StationNumber}")]
        public ActionResult EditStation(int? RouteID, int? StationNumber)
        {
            var AllStations = _stationBLL.GetAllStations();

            if (StationNumber == null && RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Station RouteStation = _routeBLL.GetRouteStationByNumber((int)RouteID, (int)StationNumber);
            if (RouteStation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteID = RouteID;
            return View(RouteStation);
        }

        // POST: Routes/1/Stations/Edit/3
        [Route("Routes/{RouteID}/Stations/Edit/{StationID}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStation(int? RouteID, [Bind(Include = "StationID,StationName,StationNumber")] Station OneStation)
        {
            ViewBag.RouteID = RouteID;

            if (RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var StationEdited = _routeBLL.EditRouteStation((int)RouteID, OneStation);
                if (StationEdited)
                {
                    return RedirectToAction("Details", "Routes", new { RouteID });
                }
                return View(OneStation);
            }

            return View(OneStation);
        }

        // GET: Routes/1/stations/5
        [Route("Routes/{RouteID}/Stations/Delete/{StationNumber}")]
        public ActionResult DeleteStation(int? RouteID, int? StationNumber)
        {
            if (StationNumber == null && RouteID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Station RouteStation = _routeBLL.GetRouteStationByNumber((int)RouteID, (int)StationNumber);
            if (RouteStation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteID = RouteID;
            return View(RouteStation);
        }

        // POST: Routes/1/stations/Delete/3
        [Route("Routes/{RouteID}/Stations/Delete/{StationNumber}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStationConfirmed(int? RouteID, int? StationNumber)
        {
            Station OneStation = _routeBLL.GetRouteStationByNumber((int)RouteID, (int)StationNumber);

            var IsDeleted = _routeBLL.DeleteRouteStation((int)RouteID, OneStation.StationID);
            if (IsDeleted)
                return RedirectToAction("Details", "Routes", new { RouteID });
            else
                return View(OneStation);
        }
    }
}