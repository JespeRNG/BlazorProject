using Autofac;
using System.Net.Http;
using BlazorApp.Services;
using BlazorApp.Interfaces;

namespace BlazorApp.AppStart
{
    public class DIConfig
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(x => 
                new AccountService(new HttpClient())
            ).As<IAccountService>().InstancePerLifetimeScope();

            var container = containerBuilder.Build();
            return container;
        }
    }
}
