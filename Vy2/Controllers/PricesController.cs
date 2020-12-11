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
    public class PricesController : Controller
    {
        private IPriceLogic _priceBLL;
        private IRouteLogic _routeBLL;

        public PricesController()
        {
            _priceBLL = new PriceLogic();
            _routeBLL = new RouteLogic();
        }

        public PricesController(IPriceLogic priceStub, IRouteLogic routeStub)
        {
            _priceBLL = priceStub;
            _routeBLL = routeStub;
        }

        // GET: Prices
        public ActionResult Index()
        {
            var AllPrices = _priceBLL.GetAllPrices();
            return View(AllPrices);
        }

        // GET: Prices/Details/5
        public ActionResult Details(int? PriceID)
        {
            if (PriceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price OnePrice = _priceBLL.GetPriceByID(PriceID);
            if (OnePrice == null)
            {
                return HttpNotFound();
            }
            return View(OnePrice);
        }

        // GET: Prices/Add
        public ActionResult Add()
        {
            var AllRoutes = _routeBLL.GetAllRoutes();
            var AllPassengerTypes = _priceBLL.GetAllPassengerTypes();
            ViewBag.RouteID = new SelectList(AllRoutes, "RouteID", "RouteName");
            ViewBag.PassengerType = new SelectList(AllPassengerTypes, "PassengerType");

            return View();
        }

        // POST: Prices/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "PriceID,RouteID,RouteName,PassengerType,TicketPrice")] Price price)
        {
            var AllRoutes = _routeBLL.GetAllRoutes();
            var AllPassengerTypes = _priceBLL.GetAllPassengerTypes();
            ViewBag.RouteID = new SelectList(AllRoutes, "RouteID", "RouteName");
            ViewBag.PassengerType = new SelectList(AllPassengerTypes, "PassengerType");

            if (ModelState.IsValid)
            {
                var IsCreated = _priceBLL.AddPrice(price);
                if (IsCreated)
                    return RedirectToAction("Index");
                else
                    return View(price);
            }

            return View(price);
        }

        // GET: Prices/Edit/5
        public ActionResult Edit(int? PriceID)
        {
            if (PriceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price OnePrice = _priceBLL.GetPriceByID(PriceID);
            if (OnePrice == null)
            {
                return HttpNotFound();
            }
            return View(OnePrice);
        }

        // POST: Prices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceID,RouteID,RouteName,PassengerType,TicketPrice")] Price OnePrice)
        {
            if (ModelState.IsValid)
            {
                var PriceEdited = _priceBLL.EditPrice(OnePrice);
                if (PriceEdited)
                    return RedirectToAction("Index");
                return View(OnePrice);
            }
            return View(OnePrice);
        }

        // GET: Prices/Delete/5
        public ActionResult Delete(int? PriceID)
        {
            if (PriceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price OnePrice = _priceBLL.GetPriceByID(PriceID);
            if (OnePrice == null)
            {
                return HttpNotFound();
            }
            return View(OnePrice);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int PriceID)
        {
            var PriceDeleted = _priceBLL.DeletePrice(PriceID);

            return RedirectToAction("Index");
        }
    }
}