using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Configuration;
using ThenAndNow.Repositories;
using ThenAndNow.Services;

namespace ThenAndNow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            #region configuration

            var appConfiguration = new AppConfiguration();
            builder.Configuration.Bind("AppConfiguration", appConfiguration);
            builder.Services.AddSingleton(appConfiguration);

            #endregion

            #region services

            builder.Services.TryAddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.TryAddSingleton<INavigationService, NavigationService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.TryAddScoped<IHttpDataCacheService, HttpDataCacheCacheService>();
            builder.Services.TryAddScoped<IEntryRepository, EntryRepository>();

            builder.Services.TryAddScoped<IFirebaseService, FirebaseService>();
            builder.Services.TryAddScoped<IRatingService, RatingService>();
            builder.Services.TryAddScoped<IReplyService, ReplyService>();
            builder.Services.TryAddScoped<IUserService, UserService>();

            #endregion

            await builder.Build().RunAsync();
        }
    }
}
