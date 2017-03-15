using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace WebAppDoorTracker.Services
{
    public class JsonLoggingService : IJsonLoggingService
    {
        private List<LogData> jsonLogs { get; set; }

        public JsonLoggingService()
        {
            jsonLogs = new List<LogData>();
        }

        public void WriteLogs(HttpActionExecutedContext actionExecutedContext)
        {

            LogData logdata = new LogData
            {
                Location = actionExecutedContext.Response.RequestMessage.RequestUri,
                Method = actionExecutedContext.Response.RequestMessage.Method
            };

            jsonLogs.Add(logdata);
            string json = JsonConvert.SerializeObject(jsonLogs.ToArray());

            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 100000);
            string filename = "logadata" + randomNumber + ".json";
            string fullSavePath = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/"));

            foreach (var log in jsonLogs)
            {
                File.WriteAllText(fullSavePath + filename, json);
            }


        }
    }
}