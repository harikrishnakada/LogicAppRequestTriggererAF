using ApiClientLib;
using ApiClientLib.Auth;

using LogicAppRequestTriggererAF.Extensions;
using LogicAppRequestTriggererAF.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using Polly;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(LogicAppRequestTriggererAF.Startup))]
namespace LogicAppRequestTriggererAF
{
    internal class Startup : FunctionsStartup
    {
        public override async void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IApiClient, ApiClientLib.ApiClient>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5)).
                    AddRetryPolicy();

            builder.Services.AddScoped<IAuthenticationProvider, AzureAdAuthenticationProvider>();

            builder.Services.AddScoped<DocumentReviewService>();

        }
    }
}
