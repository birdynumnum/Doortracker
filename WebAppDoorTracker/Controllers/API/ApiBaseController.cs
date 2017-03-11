using DataLayer;
using DataLayer.Repository;
using Domain;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAppDoorTracker.Controllers.API
{
    [EnableCorsAttribute("http://localhost:55736", "*", "* ")]
    public class ApiBaseController : ApiController
    {
        protected readonly IEntityBaseRepository<Error> _errorepository;
        protected readonly IUnitOfWork _UnitOfWork;

        public ApiBaseController()
        {

        }

        public ApiBaseController(IEntityBaseRepository<Error> _eeror)
        {
            _errorepository = _eeror;
        }

        public ApiBaseController(IEntityBaseRepository<Error> errorepository, IUnitOfWork unitofwork)
        {
            _errorepository = errorepository;
            _UnitOfWork = unitofwork;
        }

        public ApiBaseController(IDataRepository dataRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            _errorepository = errorsRepository;
            _UnitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();

            }
            catch (SecurityException ex)
            {

                LogError(ex);

                response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized operation");
            }

            //catch (FaultException<AuthorizationValidationException> ex)
            //{

            //    LogError(ex);

            //    response = request.CreateResponse(HttpStatusCode.BadRequest, "Unauthorized operation");
            //}

            catch (FaultException ex)
            {

                LogError(ex);

                response = request.CreateResponse(HttpStatusCode.BadRequest, "Yikes - something went seriously wrong");
            }

            catch (DbUpdateException ex)
            {
                LogError(ex);

                response = request.CreateResponse(HttpStatusCode.BadRequest, "Update on database failed");
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, "Yikes - something went seriously wrong");
            }

            return response;
        }

        private void LogError(Exception ex)
        {
            Error logError = new Error()
            {
                Message = ex.Message,
                CreationDate = DateTime.Now,
                StackTrace = ex.StackTrace
            };

            _errorepository.Add(logError);
            _UnitOfWork.Commit();
        }

    }
}
