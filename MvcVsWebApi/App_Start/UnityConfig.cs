using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcVsWebApi.Models;
using Unity.Mvc5;

namespace MvcVsWebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //container.RegisterType<IMvcContext, MvcContext>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}