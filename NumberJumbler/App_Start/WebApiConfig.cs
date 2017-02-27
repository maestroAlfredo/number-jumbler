using Microsoft.Practices.Unity;
using NumberJumbler.Models;
using NumberJumbler.Resolver;
using NumberJumbler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NumberJumbler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<INumberJumblerService, NumberJumblerServices>(new HierarchicalLifetimeManager());
            container.RegisterType<INumberJumblerWebService, NumberJumblerWebService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes(); //enable attribute routing

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //conventional routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
