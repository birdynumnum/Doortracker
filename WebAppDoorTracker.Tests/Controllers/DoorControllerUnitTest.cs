using AutoMapper;
using DataLayer;
using DataLayer.Repository;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using WebAppDoorTracker.Controllers.API;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.Tests.Controllers
{
    [TestClass]
    public class DoorControllerUnitTest
    {
        private IMapper mapper { get; set; }
        private List<DoorDTO> doorsDTO { get; set; }
        private List<Door> doors { get; set; }
        private string filter { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Door, DoorDTO>();

            Door d1 = new Door { Id = 1, Make = "Test1", Model = "door" };
            Door d2 = new Door { Id = 2, Make = "findme", Model = "door" };

            doors = new List<Door>();

            doors.Add(d1);
            doors.Add(d2);

            DoorDTO dto1 = new DoorDTO { Id = 1, Make = "findme", Model = "door" };
            DoorDTO dto2 = new DoorDTO { Id = 2, Make = "Test2", Model = "DTO" };

            doorsDTO = new List<DoorDTO>();

            doorsDTO.Add(dto1);
            doorsDTO.Add(dto2);

            filter = "FindMe";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mapper = null;
            doorsDTO = null;
            doors = null;
        }

        [TestMethod]
        public void GetShouldReturnAllDoors()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            //Act
            var _response = controller.Get(request);

            // Assert
            var responseResult = JsonConvert.DeserializeObject<List<DoorDTO>>(_response.Content.ReadAsStringAsync().Result).ToArray();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
            Assert.AreEqual(responseResult.Length, 2);
        }

        [TestMethod]
        public void GetByFilterShoudReturnCorrectItem()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            //Act
            var _response = controller.Get(request, filter.ToLower().Trim());

            // Assert
            var responseResult = JsonConvert.DeserializeObject<List<DoorDTO>>(_response.Content.ReadAsStringAsync().Result).ToArray();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.Found);
            Assert.AreEqual(responseResult.Any(), true);
            Assert.AreEqual(responseResult.Length, 1);

            Assert.AreEqual(responseResult[0].Make, doorsDTO[0].Make);

        }

        [TestMethod]
        public void GetByFilterShoudReturnInCorrectItemAndNotFoundStatusCode()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            //Act
            filter = "Dontfindme";
            var _response = controller.Get(request, filter);

            // Assert
            var responseResult = JsonConvert.DeserializeObject<List<DoorDTO>>(_response.Content.ReadAsStringAsync().Result).ToArray();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.Found);
            Assert.AreEqual(responseResult.Any(), false);
            Assert.AreEqual(responseResult.Length, 0);
        }

        [TestMethod]
        public void PutShouldInsertItemAndReturnCorrectStatus()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            DoorDTO newDoor = new DoorDTO { Id = 2, Make = "NewDoor" };

            //Act
            var _response = controller.Put(request, newDoor);

            // Assert
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void PutInvalidModelShoudReturnBadRequest()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            DoorDTO newDoor = new DoorDTO { Id = 2, Make = "NewDoor" };

            //Act
            controller.ModelState.AddModelError("Invalid property", "Property invalid");
            var _response = controller.Put(request, newDoor);

            // Assert
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void UpdateShouldFunctionAndReturnStatusCodeOK()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetSingle(1)).Returns(new Door { });

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            DoorDTO newDoor = new DoorDTO { Id = 2, Make = "NewDoor" };
            DoorDTO updatedoor = doorsDTO.First();
            updatedoor.Make = "Updated";

            //Act
            var _response = controller.Update(request, updatedoor);

            // Assert
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);

        }

        [TestMethod]
        public void UpdateInvalidModelShouldReturnBadRequest()
        {
            //Arrange
            var _doorrepo = new Mock<IEntityBaseRepository<Door>>();
            _doorrepo.Setup(x => x.GetAll()).Returns(doors.AsQueryable());

            var _errorrepo = new Mock<IEntityBaseRepository<Error>>();
            var _uow = new Mock<IUnitOfWork>();

            var AutoMapperMock = new Mock<IMapper>();

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/door/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "door" } });

            var controller = new DoorController(_doorrepo.Object, _errorrepo.Object, _uow.Object);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            DoorDTO newDoor = new DoorDTO { Id = 2, Make = "NewDoor" };

            //Act
            controller.ModelState.AddModelError("Invalid property", "Property invalid");
            var _response = controller.Update(request, newDoor);

            // Assert
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
