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
    public class StationsController : Controller
    {
        private IStationLogic _stationBLL;

        public StationsController()
        {
            _stationBLL = new StationLogic();
        }

        public StationsController(IStationLogic stub)
        {
            _stationBLL = stub;
        }

        // GET: Stations
        public ActionResult Index()
        {
            ViewBag.NewStationAdded = false;
            ViewBag.NewStationAdded = TempData["NewStationAdded"];
            ViewBag.LoggedIn = Session["LoggedIn"];
            TempData["NewStationAdded"] = false;
            var AllStations = _stationBLL.GetAllStations();
            return View(AllStations);
        }

        // GET: Stations/Details/5
        public ActionResult Details(int? StationID)
        {
            if (StationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station OneStation = _stationBLL.GetStationByID(StationID);
            if (OneStation == null)
            {
                return HttpNotFound();
            }
            return View(OneStation);
        }

        // GET: Stations/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Stations/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "StationID,StationName")] Station NewStation)
        {
            if (ModelState.IsValid)
            {
                var StationAdd = _stationBLL.AddStation(NewStation);
                if (StationAdd)
                {
                    TempData["NewStationAdded"] = true;
                    return RedirectToAction("Index");
                }
                return View(NewStation);
            }
            return View(NewStation);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(int? StationID)
        {
            if (StationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station OneStation = _stationBLL.GetStationByID(StationID);
            if (OneStation == null)
            {
                return HttpNotFound();
            }
            return View(OneStation);
        }

        // POST: Stations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationID,StationName")] Station OneStation)
        {
            if (ModelState.IsValid)
            {
                var StationAdded = _stationBLL.EditStation(OneStation);
                if (StationAdded)
                    return RedirectToAction("Index");
                return View(OneStation);
            }
            return View(OneStation);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? StationID)
        {
            if (StationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station OneStation = _stationBLL.GetStationByID(StationID);
            if (OneStation == null)
            {
                return HttpNotFound();
            }
            return View(OneStation);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int StationID)
        {
            Station OneStation = _stationBLL.GetStationByID(StationID);
            var IsDeleted = _stationBLL.DeleteStation(StationID);
            if (IsDeleted)
                return RedirectToAction("Index");
            else return View(OneStation);
        }
    }
}