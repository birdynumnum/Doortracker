using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;

namespace ThinkTectureServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {

                new InMemoryUser
                {
                    Username = "Frank",
                    Password = "secret",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705ddf"
                },

                new InMemoryUser
                {
                    Username = "Bert",
                    Password = "secret",
                    Subject = "14960b9a-f738-46ec-8ad6-3fc572b451ad"
                }
            };
        }
    }
}