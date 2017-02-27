using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using NumberJumbler.Services;

namespace NumberJumbler
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<INumberJumblerWebService, NumberJumblerWebService>(new HierarchicalLifetimeManager());
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}