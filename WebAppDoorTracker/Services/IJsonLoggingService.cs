﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebAppDoorTracker.Services
{
    public interface IJsonLoggingService
    {
        void WriteLogs(HttpActionExecutedContext ActionExecutedContext);
    }
}
