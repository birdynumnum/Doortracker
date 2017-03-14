
using System;
using System.Net.Http;
namespace Domain
{
    public class LogData
    {
        public Uri Location { get; set; }
        public HttpMethod Method { get; set; }
    }
}
