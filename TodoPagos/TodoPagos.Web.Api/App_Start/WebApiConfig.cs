﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TodoPagos.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "TodoPagosApi",
                routeTemplate: "api/{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
