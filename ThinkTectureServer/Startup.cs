using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using ThinkTectureServer.Config;

[assembly: OwinStartup(typeof(ThinkTectureServer.Startup))]
namespace ThinkTectureServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
                {
                    var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                    var options = new IdentityServerOptions()
                    {

                        Factory = idServerServiceFactory,
                        SiteName = "DoorTracker Securirity Tracker Service (DTSTS)",
                        IssuerUri = Constants.Constants.DoorTrackerIssuerUri,
                        PublicOrigin = Constants.Constants.DoorTrackerSTSOrigin,
                        SigningCertificate = LoadCertificate()
                    };
                    idsrvApp.UseIdentityServer(options);
                });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}