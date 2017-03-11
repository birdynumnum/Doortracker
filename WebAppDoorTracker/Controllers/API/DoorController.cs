using AutoMapper;
using DataLayer;
using DataLayer.Repository;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppDoorTracker.ExtensionMethods;
using WebAppDoorTracker.Infrastructure.Core;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.Controllers.API
{
    [EnableCorsAttribute("http://localhost:55736", "*", "* ")]
    [RoutePrefix("api/door")]
    public class DoorController : ApiBaseController
    {
        private readonly IEntityBaseRepository<Door> _DoorRepository;

        public DoorController()
        {

        }

        public DoorController(IEntityBaseRepository<Door> DoorRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _DoorRepository = DoorRepository;
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var TheDoors = _DoorRepository.GetAll().OrderBy(o => o.DateOfInstallation); ;
                IEnumerable<DoorDTO> DoorsDTOS = Mapper.Map<IEnumerable<Door>, IEnumerable<DoorDTO>>(TheDoors);
                return response = request.CreateResponse<IEnumerable<DoorDTO>>(HttpStatusCode.OK, DoorsDTOS);

            });
        }

        public HttpResponseMessage GetID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var TheDoors = _DoorRepository.GetAll().OrderBy(o => o.DateOfInstallation); ;
                IEnumerable<DoorDTO> DoorsDTOS = Mapper.Map<IEnumerable<Door>, IEnumerable<DoorDTO>>(TheDoors);
                return response = request.CreateResponse<IEnumerable<DoorDTO>>(HttpStatusCode.OK, DoorsDTOS);

            });
        }

        [HttpPost]
        [Route("put")]
        public HttpResponseMessage Put(HttpRequestMessage request, DoorDTO doordto)
        {
            Door newDoor;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    newDoor = new Door(doordto.Id, doordto.Make, doordto.Model, doordto.YearOfManufacture, doordto.DateOfInstallation, doordto.NoOfWarrantyYears);
                    _DoorRepository.Add(newDoor);
                    _UnitOfWork.Commit();

                    doordto = Mapper.Map<Door, DoorDTO>(newDoor);

                    response = request.CreateResponse<DoorDTO>(HttpStatusCode.Created, doordto);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("get/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            HttpResponseMessage response = null;

            if (filter == null) return null;
            else
            {
                string searchfilter = filter.ToLower().Trim();

                return CreateHttpResponse(request, () =>
                     {
                         var result = _DoorRepository.GetAll().Where(e => e.Model.ToLower().Contains(searchfilter) || e.Make.ToLower().Contains(searchfilter));

                         var doorsDTOS = Mapper.Map<IEnumerable<Door>, IEnumerable<DoorDTO>>(result);

                         return response = request.CreateResponse<IEnumerable<DoorDTO>>(HttpStatusCode.Found, doorsDTOS);
                     });
            }
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, DoorDTO doordto)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    Door foundDoor = _DoorRepository.GetSingle(doordto.Id);
                    foundDoor.UpdateDoor(doordto);
                    _DoorRepository.Edit(foundDoor);
                    _UnitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Door> Doors = null;
                int totalDoors = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    Doors = _DoorRepository.FindBy(c => c.Make.ToLower().Contains(filter) ||
                            c.Model.ToLower().Contains(filter))
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalDoors = _DoorRepository.GetAll()
                        .Where(c => c.Make.ToLower().Contains(filter) ||
                            c.Model.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    Doors = _DoorRepository.GetAll()
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalDoors = _DoorRepository.GetAll().Count();
                }

                IEnumerable<DoorDTO> doorDTOs = Mapper.Map<IEnumerable<Door>, IEnumerable<DoorDTO>>(Doors);

                PaginationSet<DoorDTO> pagedSet = new PaginationSet<DoorDTO>()
                {
                    Page = currentPage,
                    TotalCount = totalDoors,
                    TotalPages = (int)Math.Ceiling((decimal)totalDoors / currentPageSize),
                    Items = doorDTOs
                };

                response = request.CreateResponse<PaginationSet<DoorDTO>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
    }
}
