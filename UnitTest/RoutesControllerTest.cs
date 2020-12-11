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
    public class RoutesControllerTest
    {
        [TestMethod]
        public void TestGetIndex()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteList = new List<Route>
            {
                new Route {RouteID = 1, RouteName = "R1"},
                new Route {RouteID = 2, RouteName = "R2"},
            };

            // ACt
            var result = (ViewResult)controller.Index();
            var resultList = (List<Route>)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            for (int i = 0; i < resultList.Count(); i++)
            {
                Assert.AreEqual(RouteList[i].RouteID, resultList[i].RouteID);
                Assert.AreEqual(RouteList[i].RouteName, resultList[i].RouteName);
            }
        }

        [TestMethod]
        public void TestGetDetails()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteStations = new List<Station>
            {
                new Station {StationID = 1, StationName = "Oslo S", StationNumber = 1},
                new Station {StationID = 2, StationName = "Nationaltheatret", StationNumber = 2},
                new Station{StationID = 3, StationName = "Lysaker", StationNumber = 3}
            };
            var OneRoute = new Route { RouteID = 1, RouteName = "R1", RouteStations = RouteStations };

            // ACt
            var result = (ViewResult)controller.Details(1);
            var resultRoute = (Route)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(OneRoute.RouteID, resultRoute.RouteID);
            Assert.AreEqual(OneRoute.RouteName, resultRoute.RouteName);
            for (int i = 0; i < OneRoute.RouteStations.Count(); i++)
            {
                Assert.AreEqual(OneRoute.RouteStations[i].StationID, resultRoute.RouteStations[i].StationID);
                Assert.AreEqual(OneRoute.RouteStations[i].StationName, resultRoute.RouteStations[i].StationName);
                Assert.AreEqual(OneRoute.RouteStations[i].StationNumber, resultRoute.RouteStations[i].StationNumber);
            }
        }

        [TestMethod]
        public void TestGetAdd()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Add();

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAdd()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var NewRoute = new Route { RouteID = 4, RouteName = "R4" };

            // ACt
            var result = (RedirectToRouteResult)controller.Add(NewRoute);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostAddDBError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var NewRoute = new Route { RouteID = 0, RouteName = null };

            // ACt
            var result = (ViewResult)controller.Add(NewRoute);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddValidationError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("RouteName", "Route name should be provided!");
            var NewRoute = new Route();

            // ACt
            var result = (ViewResult)controller.Add(NewRoute);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetEdit()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Edit(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEdit()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var OneRoute = new Route { RouteID = 1, RouteName = "R1" };

            // ACt
            var result = (RedirectToRouteResult)controller.Edit(OneRoute);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostEditValidationError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("RouteName", "Route name should string!");
            var OneRoute = new Route();

            // ACt
            var result = (ViewResult)controller.Edit(OneRoute);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditDBError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var OneRoute = new Route { RouteID = 0, RouteName = null };

            // ACt
            var result = (ViewResult)controller.Edit(OneRoute);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetDelete()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDelete()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (ViewResult)controller.Delete(1);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDeleteConfirmed()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Index", result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestPostDeleteRouteNotExsist()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = controller.Delete(100) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        //[TestMethod]
        //public void TestGetDetails()
        //{
        //    // Arrange
        //    var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()));
        //    var OneStation = new Station { StationID = 3, StationName = "Lysaker" };

        //    // ACt
        //    var result = (ViewResult)controller.Details(3);
        //    var resultStation = (Station)result.Model;

        //    // Assert
        //    Assert.AreEqual("", result.ViewName);
        //    Assert.AreEqual(OneStation.StationID, resultStation.StationID);
        //    Assert.AreEqual(OneStation.StationName, resultStation.StationName);
        //}

        [TestMethod]
        public void TestGetAddStation()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteID = 1;

            // ACt
            var result = (ViewResult)controller.AddStation(RouteID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddStation()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var NewStation = new Station { StationID = 4, StationName = "Sandvika" };
            var RouteID = 1;

            // ACt
            var result = (RedirectToRouteResult)controller.AddStation(RouteID, NewStation);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual(RouteID, result.RouteValues["RouteID"]);
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestPostAddStationDBError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var NewStation = new Station { StationID = 0, StationName = "" };
            var RouteID = 1;

            // ACt
            var result = (ViewResult)controller.AddStation(RouteID, NewStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostAddStationValidationError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("StationName", "Station name should be provided!");
            var NewStation = new Station();
            var RouteID = 1;

            // ACt
            var result = (ViewResult)controller.AddStation(RouteID, NewStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetEditStation()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteID = 1;
            var StationID = 1;

            // ACt
            var result = (ViewResult)controller.EditStation(RouteID, StationID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditStation()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var OneStation = new Station { StationID = 1, StationName = "Bergen", StationNumber = 2 };
            var RouteID = 1;

            // ACt
            var result = (RedirectToRouteResult)controller.EditStation(RouteID, OneStation);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual(RouteID, result.RouteValues["RouteID"]);
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestPostEditStationValidationError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            controller.ViewData.ModelState.AddModelError("StationName", "Station name should string!");
            var OneStation = new Station();
            var RouteID = 1;

            // ACt
            var result = (ViewResult)controller.EditStation(RouteID, OneStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostEditStationDBError()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var OneStation = new Station { StationID = 0, StationName = null };
            var RouteID = 1;

            // ACt
            var result = (ViewResult)controller.EditStation(RouteID, OneStation);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestGetDeleteStation()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteID = 1;
            var StationID = 1;

            // ACt
            var result = (ViewResult)controller.DeleteStation(RouteID, StationID);

            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void TestPostDeleteStationConfirmed()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));
            var RouteID = 1;
            var StationID = 1;

            // ACt
            var result = (RedirectToRouteResult)controller.DeleteStationConfirmed(RouteID, StationID);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual(RouteID, result.RouteValues["RouteID"]);
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestPostDeleteStationNotExsist()
        {
            // Arrange
            var controller = new RoutesController(new RouteLogic(new RouteRepositoryStub()), new StationLogic(new StationRepositoryStub()));

            // ACt
            var result = controller.DeleteStation(1, 100) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}