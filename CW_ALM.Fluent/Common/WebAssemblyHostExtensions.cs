using CW_ALM.Fluent.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

namespace CW_ALM.Fluent.Common
{
    public static class WebAssemblyHostExtensions
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost host)
        {
            var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
            var cultureString = await localStorage.GetItem<string>("culture");

            CultureInfo cultureInfo;

            if (!string.IsNullOrWhiteSpace(cultureString))
            {
                cultureInfo = new CultureInfo(cultureString);
            }
            else
            {
                cultureInfo = new CultureInfo(LocalizerSettings.NeutralCulture);
            }

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
