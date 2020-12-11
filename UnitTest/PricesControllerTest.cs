using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vy2.Controllers;
using VyBLL;
using VyDAL;
using VyModels;

namespace UnitTest
{
    [TestClass]
    public class PricesControllerTest
    {
        [TestMethod]
        public void TestGetIndex()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            var PriceList = new List<Price>
            {
                new Price { PriceID = 1, RouteID = 1, PassengerType = "Adult", TicketPrice = 50 },
                new Price { PriceID = 2, RouteID = 1, PassengerType = "Student", TicketPrice = 30 },
                new Price { PriceID = 3, RouteID = 1, PassengerType = "Child", TicketPrice = 15 },
                new Price { PriceID = 4, RouteID = 2, PassengerType = "Adult", TicketPrice = 300 },
                new Price { PriceID = 5, RouteID = 2, PassengerType = "Student", TicketPrice = 150 },
                new Price { PriceID = 6, RouteID = 2, PassengerType = "Child", TicketPrice = 75 }
            };

            // ACt
            var result = (ViewResult)controller.Index();
            var resultList = (List<Price>)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            for (int i = 0; i < resultList.Count(); i++)
            {
                Assert.AreEqual(PriceList[i].PriceID, resultList[i].PriceID);
                Assert.AreEqual(PriceList[i].PassengerType, resultList[i].PassengerType);
                Assert.AreEqual(PriceList[i].RouteID, resultList[i].RouteID);
                Assert.AreEqual(PriceList[i].TicketPrice, resultList[i].TicketPrice);
            }
        }

        [TestMethod]
        public void TestGetDetails()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OnePrice = new Price { PriceID = 6, RouteID = 2, PassengerType = "Child", TicketPrice = 75 };
            var PriceID = 6;

            // ACt
            var result = (ViewResult)controller.Details(PriceID);
            var resultPrice = (Price)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(OnePrice.PriceID, resultPrice.PriceID);
            Assert.AreEqual(OnePrice.RouteID, resultPrice.RouteID);
            Assert.AreEqual(OnePrice.PassengerType, resultPrice.PassengerType);
            Assert.AreEqual(OnePrice.TicketPrice, resultPrice.TicketPrice);
        }

        [TestMethod]
        public void TestGetAdd()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Add();

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAdd()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var NewPrice = new Price { PriceID = 7, RouteID = 3, PassengerType = "Adult", TicketPrice = 200 };

            // ACt
            var result = (RedirectToRouteResult)controller.Add(NewPrice);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostAddDBError()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var NewPrice = new Price { PriceID = 0, TicketPrice = 0.0f };

            // ACt
            var result = (ViewResult)controller.Add(NewPrice);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddValidationError()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("PriceID", "Price ID should be provided!");
            var NewPrice = new Price();

            // ACt
            var result = (ViewResult)controller.Add(NewPrice);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetEdit()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Edit(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEdit()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OnePrice = new Price { PriceID = 6, RouteID = 2, PassengerType = "Child", TicketPrice = 75 };

            // ACt
            var result = (RedirectToRouteResult)controller.Edit(OnePrice);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostEditValidationError()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("PriceID", "Price ID should string!");
            var OnePrice = new Price();

            // ACt
            var result = (ViewResult)controller.Edit(OnePrice);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditDBError()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OnePrice = new Price { PriceID = 0, TicketPrice = 0.0f };

            // ACt
            var result = (ViewResult)controller.Edit(OnePrice);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetDelete()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDelete()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDeleteConfirmed()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostDeletePriceNotExsist()
        {
            // Arrange
            var controller = new PricesController(new PriceLogic(new PriceRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));

            // ACt
            var result = controller.Delete(100) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}