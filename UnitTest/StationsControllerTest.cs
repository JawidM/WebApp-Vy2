using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
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
    public class StationsControllerTest
    {
        [TestMethod]
        public void TestGetIndex()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            var StationList = new List<Station>
            {
                new Station {StationID = 1, StationName = "Oslo S"},
                new Station {StationID = 2, StationName = "Nationaltheatret"},
                new Station{StationID = 3, StationName = "Lysaker"}
            };

            // ACt
            var result = (ViewResult)controller.Index();
            var resultList = (List<Station>)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            for (int i = 0; i < resultList.Count(); i++)
            {
                Assert.AreEqual(StationList[i].StationID, resultList[i].StationID);
                Assert.AreEqual(StationList[i].StationName, resultList[i].StationName);
            }
        }

        [TestMethod]
        public void TestGetDetails()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            var OneStation = new Station { StationID = 3, StationName = "Lysaker" };

            // ACt
            var result = (ViewResult)controller.Details(3);
            var resultStation = (Station)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(OneStation.StationID, resultStation.StationID);
            Assert.AreEqual(OneStation.StationName, resultStation.StationName);
        }

        [TestMethod]
        public void TestGetAdd()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Add();

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAdd()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            var NewStation = new Station { StationID = 4, StationName = "Sandvika" };

            // ACt
            var result = (RedirectToRouteResult)controller.Add(NewStation);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostAddDBError()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            var NewStation = new Station { StationID = 0, StationName = "" };

            // ACt
            var result = (ViewResult)controller.Add(NewStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddValidationError()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("StationName", "Station name should be provided!");
            var NewStation = new Station();

            // ACt
            var result = (ViewResult)controller.Add(NewStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetEdit()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Edit(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEdit()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            var OneStation = new Station { StationID = 1, StationName = "Bergen" };

            // ACt
            var result = (RedirectToRouteResult)controller.Edit(OneStation);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostEditValidationError()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("StationName", "Station name should string!");
            var OneStation = new Station();

            // ACt
            var result = (ViewResult)controller.Edit(OneStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditDBError()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));
            var OneStation = new Station { StationID = 1, StationName = null };

            // ACt
            var result = (ViewResult)controller.Edit(OneStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetDelete()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDelete()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDeleteConfirmed()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostDeleteStationNotExsist()
        {
            // Arrange
            var controller = new StationsController(new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = controller.Delete(100) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}