using DataLayer;
using StructureMap;
using StructureMap.Graph;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppDoorTracker.DependencyResolution;
using WebAppDoorTracker.Infrastructure;

namespace WebAppDoorTracker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer Container { get; private set; }

        protected void Application_Start()
        {
            Container = new Container();
            Container.Configure(x =>
            {
                x.Scan(y =>
                {
                    y.TheCallingAssembly();
                    y.AddAllTypesOf<ApiController>();
                });
            });

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.EnsureInitialized();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<RepositoryRegistry>();
            });

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Services
                  .Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

            Database.SetInitializer<DoorContext>(new DoorTrackerContextInitializer());

            using (var context = new DoorContext())
            {
                context.Database.Initialize(force: true);
            }

            AutoMapperMapping.Config();
        }

        protected void Application_End()
        {
            Container.Dispose();
        }

        protected void Application_BeginRequest()
        {
            //
        }

        protected void Application_EndRequest()
        {
            //
        }
    }
}
