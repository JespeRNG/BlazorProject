using Autofac;
using System.Net.Http;
using BlazorApplication.Services;
using BlazorApplication.Providers;
using BlazorApplication.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApplication.AppStart
{
    public class DIConfig
    {
        public static ContainerBuilder Configure(ContainerBuilder containerBuilder)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = 
                (sender, cert, chain, sslPolicyErrors) => { return true; };

            containerBuilder.RegisterType<LocalStorageService>().As<ILocalStorageService>();
            //containerBuilder.RegisterType<AuthStateProvider>();

            containerBuilder.RegisterType<AuthStateProvider>()
                .As<AuthenticationStateProvider>().InstancePerLifetimeScope();

            containerBuilder.Register(x =>
                new AccountService(new HttpClient(clientHandler),
                    x.Resolve<ILocalStorageService>())
            ).As<IAccountService>().InstancePerLifetimeScope();
            containerBuilder.Register(x =>
                new UserService(new HttpClient(clientHandler),
                    x.Resolve<ILocalStorageService>())
            ).As<IUserService>().InstancePerLifetimeScope();

            return containerBuilder;
        }
    }
}
