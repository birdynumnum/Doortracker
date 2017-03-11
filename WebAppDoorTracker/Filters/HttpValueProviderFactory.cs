using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace WebAppDoorTracker.Filters
{
    public class HttpValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            HttpRequestHeaders httprequestheaders = actionContext.ControllerContext.Request.Headers;
            return new HttpValueProvider(new HeadersMap(httprequestheaders));
        }
    }
}