using Autofac;
using BlazorApplication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorApplication.Services;
using BlazorApplication.Interfaces;
using BlazorApplication.Providers;

namespace BlazorApplication.AppStart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            DIConfig.Configure(builder);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddAuthenticationCore();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddOptions();
            services.AddScoped<AuthStateProvider>();
            //services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddSingleton<WeatherForecastService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
