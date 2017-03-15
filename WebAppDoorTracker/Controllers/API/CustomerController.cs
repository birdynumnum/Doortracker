using AutoMapper;
using DataLayer;
using DataLayer.Repository;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    [AllowAnonymous]
    [RoutePrefix(("api/customer"))]
    public class CustomerController : ApiBaseController
    {
        private readonly IEntityBaseRepository<Customer> _CustomerRepository;
        private List<anemicDoorDTO> cus_door { get; set; }

        public CustomerController()
        {

        }

        public CustomerController(IEntityBaseRepository<Customer> CustomerRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _CustomerRepository = CustomerRepository;
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var Customers = _CustomerRepository.GetAll();
                IEnumerable<CustomerDTO> CustomersDTOS = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(Customers);
                return response = request.CreateResponse<IEnumerable<CustomerDTO>>(HttpStatusCode.OK, CustomersDTOS);
            });
        }

        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var Customers = _CustomerRepository.GetAll();
                IEnumerable<CustomerDTO> CustomersDTOS = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(Customers);
                return response = request.CreateResponse<IEnumerable<CustomerDTO>>(HttpStatusCode.OK, CustomersDTOS);
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
                    var result = _CustomerRepository.GetAll().Where(e => e.EMail.ToLower().Contains(searchfilter) || e.LastName.ToLower().Contains(searchfilter));
                    var customerdto = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(result);
                    return response = request.CreateResponse<IEnumerable<CustomerDTO>>(HttpStatusCode.Found, customerdto);
                });
            }
        }

        [HttpGet]
        [Route("showdoors")]
        //get doors installed at this customer
        public HttpResponseMessage ShowDoors(HttpRequestMessage request, int id)
        {
            //TODO move to service layer???
            cus_door = new List<anemicDoorDTO>();
            anemicDoorDTO newDoor = null;

            var CustomersAddressDoors = _CustomerRepository.GetAll()
                                .Include(a => a.Address.Select(d => d.Doors)).Where(p => p.Id == id);

            //yuck
            foreach (Customer c in CustomersAddressDoors)
            {
                foreach (Address a in c.Address)
                {
                    foreach (Door d in a.Doors)
                    {
                        newDoor = new anemicDoorDTO();
                        newDoor.Make = d.Make;
                        newDoor.Model = d.Model;
                        cus_door.Add(newDoor);
                    }
                }
            }
            //

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                return response = request.CreateResponse<IEnumerable<anemicDoorDTO>>(HttpStatusCode.OK, cus_door);
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CustomerDTO customer)
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
                    Customer _customer = _CustomerRepository.GetSingle(customer.Id);
                    _customer.UpdateCustomer(customer);

                    _UnitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("put")]
        public HttpResponseMessage Put(HttpRequestMessage request, CustomerDTO customerdto)
        {
            Customer newCustomer;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    DateTime registrationDate = DateTime.Now;

                    newCustomer = new Customer(customerdto.Id, customerdto.FirstName, customerdto.LastName, customerdto.EMail, customerdto.MobileNumber, registrationDate);
                    _CustomerRepository.Add(newCustomer);
                    _UnitOfWork.Commit();

                    customerdto = Mapper.Map<Customer, CustomerDTO>(newCustomer);
                }

                return response = request.CreateResponse<CustomerDTO>(HttpStatusCode.Created, customerdto);
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
                List<Customer> customers = null;
                int totalCustomers = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    customers = _CustomerRepository.FindBy(c => c.LastName.ToLower().Contains(filter) ||
                            c.EMail.ToLower().Contains(filter) ||
                            c.FirstName.ToLower().Contains(filter))
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalCustomers = _CustomerRepository.GetAll()
                        .Where(c => c.LastName.ToLower().Contains(filter) ||
                            c.EMail.ToLower().Contains(filter) ||
                            c.FirstName.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    customers = _CustomerRepository.GetAll()
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalCustomers = _CustomerRepository.GetAll().Count();
                }

                IEnumerable<CustomerDTO> customersDTOs = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);

                PaginationSet<CustomerDTO> pagedSet = new PaginationSet<CustomerDTO>()
                {
                    Page = currentPage,
                    TotalCount = totalCustomers,
                    TotalPages = (int)Math.Ceiling((decimal)totalCustomers / currentPageSize),
                    Items = customersDTOs
                };

                response = request.CreateResponse<PaginationSet<CustomerDTO>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
    }
}