﻿using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace TeamStatus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            config.Routes.MapHttpRoute(
                name: "Action",
                routeTemplate: "api/status/StatusByDate/{category,statusDate,teamId}"
                );

            config.Routes.MapHttpRoute(
                name: "UpdateProfile",
                routeTemplate: "api/profile/UpdateProfilePicture/{file}",
                defaults: new { Controller = "profile", Action = "UpdateProfilePicture", HttpVerbs=HttpVerbs.Post }
                );
        }
    }
}
