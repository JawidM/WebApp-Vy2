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
    public class DepartureRepositoryTest
    {
        [TestMethod]
        public void TestGetIndex()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var DepartureListRoute1 = new List<Departure>
            {
                new Departure {DepartureID = 1, RouteID = 1, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 11, 30, 0) },
                new Departure {DepartureID = 2, RouteID = 1, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 14, 00, 0) },
                new Departure {DepartureID = 5, RouteID = 1, StationID = 2, DepartureTime = new DateTime(2019, 10, 20, 11, 35, 0) },
                new Departure {DepartureID = 6, RouteID = 1, StationID = 2, DepartureTime = new DateTime(2019, 10, 20, 14, 05, 0) },
                new Departure {DepartureID = 7, RouteID = 1, StationID = 3, DepartureTime = new DateTime(2019, 10, 20, 11, 45, 0) },
                new Departure {DepartureID = 8, RouteID = 1, StationID = 3, DepartureTime = new DateTime(2019, 10, 20, 14, 15, 0) },
            };

            // ACt
            var result = (ViewResult)controller.Index(1);
            var resultList = (List<Departure>)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            for (int i = 0; i < resultList.Count(); i++)
            {
                Assert.AreEqual(DepartureListRoute1[i].DepartureID, resultList[i].DepartureID);
                Assert.AreEqual(DepartureListRoute1[i].RouteID, resultList[i].RouteID);
                Assert.AreEqual(DepartureListRoute1[i].StationID, resultList[i].StationID);
                Assert.AreEqual(DepartureListRoute1[i].DepartureTime, resultList[i].DepartureTime);
            }
        }

        [TestMethod]
        public void TestGetDetails()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OneDeparture = new Departure { DepartureID = 3, RouteID = 2, StationID = 1, DepartureTime = new DateTime(2019, 10, 20, 21, 30, 0) };
            var RouteID = 2;
            var DepartureID = 3;
            // ACt
            var result = (ViewResult)controller.Details(RouteID, DepartureID);
            var resultDeparture = (Departure)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(OneDeparture.DepartureID, resultDeparture.DepartureID);
            Assert.AreEqual(OneDeparture.StationID, resultDeparture.StationID);
            Assert.AreEqual(OneDeparture.DepartureTime, resultDeparture.DepartureTime);
        }

        [TestMethod]
        public void TestGetAdd()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var RouteID = 1;
            // ACt
            var result = (ViewResult)controller.Add(RouteID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAdd()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var NewDeparture = new Departure { DepartureID = 13, RouteID = 2, StationID = 5, DepartureTime = new DateTime(2019, 11, 20, 16, 20, 0) };
            var RouteID = 2;

            // ACt
            var result = (RedirectToRouteResult)controller.Add(RouteID, NewDeparture);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostAddDBError()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var NewDeparture = new Departure { DepartureID = 0, DepartureTime = new DateTime() };
            var RouteID = 2;

            // ACt
            var result = (ViewResult)controller.Add(RouteID, NewDeparture);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddValidationError()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("DepartureID", "Departure ID should be provided!");
            var NewDeparture = new Departure();
            var RouteID = 2;

            // ACt
            var result = (ViewResult)controller.Add(RouteID, NewDeparture);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetEdit()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var RouteID = 1;
            var OneDeparture = new Departure { DepartureID = 2, RouteID = 1, StationID = 1, DepartureTime = new DateTime(2019, 11, 20, 15, 30, 0) };

            // ACt
            var result = (ViewResult)controller.Edit(RouteID, OneDeparture.DepartureID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEdit()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OneDeparture = new Departure { DepartureID = 3, RouteID = 2, StationID = 1, DepartureTime = new DateTime(2019, 12, 01, 10, 30, 0) };
            var RouteID = 2;
            // ACt
            var result = (RedirectToRouteResult)controller.Edit(RouteID, OneDeparture);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostEditValidationError()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("DepartureName", "Departure name should string!");
            var OneDeparture = new Departure();
            var RouteID = 1;
            // ACt
            var result = (ViewResult)controller.Edit(RouteID, OneDeparture);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditDBError()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var OneDeparture = new Departure { DepartureID = 0, DepartureTime = new DateTime(2019, 12, 01, 10, 30, 0) };
            var RouteID = 2;

            // ACt
            var result = (ViewResult)controller.Edit(RouteID, OneDeparture);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetDelete()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var RouteID = 1;
            var DepartureID = 1;

            // ACt
            var result = (ViewResult)controller.Delete(RouteID, DepartureID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDelete()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var RouteID = 1;
            var DepartureID = 1;
            // ACt
            var result = (ViewResult)controller.Delete(RouteID, DepartureID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDeleteConfirmed()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var DepartureID = 1;
            var RouteID = 1;

            // ACt
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(RouteID, DepartureID);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostDeleteDepartureNotExsist()
        {
            // Arrange
            var controller = new DeparturesController(new DepartureLogic(new DepartureRepositoryStub()), new RouteLogic(new RouteRepositoryStub()));
            var RouteID = 1;
            var DepartureID = 100;
            // ACt
            var result = controller.Delete(RouteID, DepartureID) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}