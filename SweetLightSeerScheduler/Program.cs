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

            //Configure Environment Variables and Configuration
            builder.Services.AddSingleton<IGoogleAuthSettings>(new GoogleAuthSettings
            {
                PrivateKey = Environment.GetEnvironmentVariable("GoogleAuth__PrivateKey") ,
                ClientEmail = Environment.GetEnvironmentVariable("GoogleAuth__ClientEmail"),
                ApplicationName = Environment.GetEnvironmentVariable("GoogleAuth__ApplicationName") ,
                ReadingId = Environment.GetEnvironmentVariable("GoogleAuth__ReadingId") ,
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

            app.MapGet("/debug/env", () =>
            {
                var vars = new Dictionary<string, string?>
    {
        { "GoogleAuth__PrivateKey", Environment.GetEnvironmentVariable("GoogleAuth__PrivateKey") },
        { "GoogleAuth__ClientEmail", Environment.GetEnvironmentVariable("GoogleAuth__ClientEmail") },
        { "ASPNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") }
    };

                return Results.Json(vars);
            });

            app.Run();
        }
    }
}
