using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppDoorTracker.Startup))]
namespace WebAppDoorTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
            new IdentityServerBearerTokenAuthenticationOptions
            {
                DelayLoadMetadata = true,
                Authority = Constants.Constants.DoorTrackerSTS,
                RequiredScopes = new[] { "DoorTracker" },
            });

            ConfigureAuth(app);
        }
    }
}
