using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace ThinkTectureServer.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "DoorTracker",
                    DisplayName="DoorTrackerDisplay",
                    Description= "The doortracker app to mange doors and custoners",
                    Type= ScopeType.Resource
                }
            };
        }
    }
}