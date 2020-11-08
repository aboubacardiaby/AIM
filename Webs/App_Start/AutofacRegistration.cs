using Autofac;
using Autofac.Integration.Mvc;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Webs;

namespace Web.App_Start
{
    public static class AutofacRegistration
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<BeverageSaleRepository>().As<IBeverageSaleRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();


            builder.RegisterFilterProvider();

            var container = builder.Build();

            // Setting Dependency Injection Parser
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}