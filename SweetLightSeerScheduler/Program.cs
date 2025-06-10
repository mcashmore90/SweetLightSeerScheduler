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

            builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection("GoogleAuth"));
            builder.Services.AddSingleton<IGoogleAuthSettings>(s => s.GetRequiredService<IOptions<GoogleAuthSettings>>().Value);

            builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();

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
