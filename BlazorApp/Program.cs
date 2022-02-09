using System;
using System.Net.Http;
using BlazorApp.AppStart;
using BlazorApp.Services;
using BlazorApp.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });
                builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddCors();
            DIConfig.Configure();

            await builder.Build().RunAsync();
        }
    }
}
