using Blazored.LocalStorage;
using BlazorGame.Models;
using BlazorGame.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorGame
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<GameModel>();
            builder.Services.AddScoped<StageManager>();
            builder.Services.AddScoped<MedianStripManager>();
            builder.Services.AddScoped<SceneryManager>();
            builder.Services.AddScoped<AICarManager>();
            builder.Services.AddScoped<IBrowserService, BrowserService>();
            builder.Services.AddScoped<IGameDimensionService, GameDimensionService>();
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}
