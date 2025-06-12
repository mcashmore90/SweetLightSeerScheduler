using Microsoft.Extensions.Options;
using Radzen;
using SweetLightSeerScheduler.Components;
using SweetLightSeerScheduler.Interfaces;
using SweetLightSeerScheduler.Services;

namespace SweetLightSeerScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddRadzenComponents();
            builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Configure Environment Variables and ConfigurationAdd commentMore actions
            builder.Services.AddSingleton<IGoogleAuthSettings>(new GoogleAuthSettings
            {
                PrivateKey = Environment.GetEnvironmentVariable("GoogleAuth__PrivateKey") ?? builder.Configuration["GoogleAuth:PrivateKey"],
                ClientEmail = Environment.GetEnvironmentVariable("GoogleAuth__ClientEmail") ?? builder.Configuration["GoogleAuth:ClientEmail"],
                ApplicationName = Environment.GetEnvironmentVariable("GoogleAuth__ApplicationName") ?? builder.Configuration["GoogleAuth:ApplicationName"],
                ReadingId = Environment.GetEnvironmentVariable("GoogleAuth__ReadingId") ?? builder.Configuration["GoogleAuth:ReadingId"],
                Scope = builder.Configuration.GetSection("GoogleAuth:Scope").Get<string[]>()
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
