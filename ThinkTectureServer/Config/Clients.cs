using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace ThinkTectureServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                    new Client(){
                        ClientId = "doortrackerimplicit",
                        ClientName="Door Tracker Implicit",
                        Flow = Flows.Implicit,
                        AllowAccessToAllScopes = true, 
                        RedirectUris = new List<string>
                        {
                            Constants.Constants.DoorTrackerAngular + "callback.html"
                        }
                    }
            };
        }
    }
}