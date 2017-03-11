using AutoMapper;
using DataLayer;
using DataLayer.Repository;
using Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppDoorTracker.ExtensionMethods;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.Controllers.API
{
    [EnableCorsAttribute("http://localhost:55736", "*", "* ")]
    [RoutePrefix("api/address")]
    public class AddressController : ApiBaseController
    {

        private readonly IEntityBaseRepository<Address> _AddressRepository;
        List<anemicCustomerDTO> address_cus = null;

        public AddressController()
        {

        }

        public AddressController(IEntityBaseRepository<Address> AddressRepository, IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _AddressRepository = AddressRepository;
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var Addresses = _AddressRepository.GetAll().OrderBy(o => o.PostalCode);
                IEnumerable<AddressDTO> AddressDTOS = Mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(Addresses);
                return response = request.CreateResponse<IEnumerable<AddressDTO>>(HttpStatusCode.OK, AddressDTOS);
            });
        }

        [HttpGet]
        [Route("get/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            if (filter == null) return null;

            else
            {
                string searchvalue = filter.ToLower().Trim();
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    var Addresses = _AddressRepository.GetAll().Where(p => p.PostalCode.Contains(searchvalue) || p.StreetName.Contains(searchvalue));
                    IEnumerable<AddressDTO> AddressDTOS = Mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(Addresses);
                    return response = request.CreateResponse<IEnumerable<AddressDTO>>(HttpStatusCode.OK, AddressDTOS);
                });
            }
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, AddressDTO address)
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
                    Address _address = _AddressRepository.GetSingle(address.Id);
                    _address.UpdateAddress(address);

                    _UnitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("showCustomer")]
        //Show associated customer with address
        public HttpResponseMessage ShowCustomer(HttpRequestMessage request, int id)
        {
            //TODO move to service layer???
            address_cus = new List<anemicCustomerDTO>();
            anemicCustomerDTO newCustomer = null;

            var CustomersAddress = _AddressRepository.GetAll()
                                .Include(c => c.Customer).Where(p => p.Id == id);

            //yuck
            foreach (Address a in CustomersAddress)
            {
                newCustomer = new anemicCustomerDTO();
                newCustomer.LastName = a.Customer.LastName;
                newCustomer.MobileNumber = a.Customer.MobileNumber;
                address_cus.Add(newCustomer);
            }
            //

            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    return response = request.CreateResponse(HttpStatusCode.OK, address_cus);
                });
        }
    }
}
