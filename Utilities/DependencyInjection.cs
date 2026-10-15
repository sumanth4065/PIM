using AventStack.ExtentReports;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PIMQAAutomation.Modules.Common.Constants;
using PPT.Integration.DataHub.Clients.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMQAAutomation.Utilities
{
    public class DependencyInjection
    {
        private static IHost _host;
        public static void SetupDI()
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var builtConfig = config.Build();
                var keyVaultName = builtConfig[AppSettingConstants.Keyvault];
                var kvUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
                var credential = new DefaultAzureCredential();

                var secretClient = new SecretClient(kvUri, credential);
                //var secretName = builtConfig["MySecretName"];
                var secret = secretClient.GetSecret(AppSettingConstants.InternalApplicationServiceOktaSecret);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add(AppSettingConstants.OktaApplicationClientSecret, secret.Value.Value);
                config.AddInMemoryCollection(dictionary);
                builtConfig = config.Build();
            
            })
            .ConfigureServices((context, services) =>
            {
                services.AddIntegrationDataHubClient(options => options.Configuration = context.Configuration);
            })
            .Build();
        }

        public static T GetService<T>()
        {
            if (_host == null)
            {
                throw new InvalidOperationException("Dependency Injection host is not initialized. Call SetupDI() first.");
            }
            return _host.Services.GetRequiredService<T>();
        }
    }
}
