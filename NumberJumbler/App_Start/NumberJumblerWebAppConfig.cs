using Microsoft.Practices.Unity;
using NumberJumbler.Models;
using NumberJumbler.Resolver;
using NumberJumbler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace NumberJumbler
{
    public static class NumberJumblerWebAppConfig
    {
        public static IDependencyResolver RegisterServices(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<INumberJumblerWebService, NumberJumblerWebService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return config.DependencyResolver;
        }
    }
}