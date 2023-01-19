using BlazorGame.Models;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorGame.Services
{
    public interface IBrowserService
    {
        Task<ViewDimensions> GetDimensions();
    }

    public class BrowserService : IBrowserService
    {
        IJSRuntime jsRuntime;
        IJSObjectReference module;

        public BrowserService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<ViewDimensions> GetDimensions()
        {
            module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/helpers.js");
            return await module.InvokeAsync<ViewDimensions>("getDimensions");
        }
    }
}
