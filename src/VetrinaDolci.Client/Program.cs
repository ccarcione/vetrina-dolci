using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VetrinaDolci.WebAPI;
using VetrinaDolci.WebAPI.Models;

namespace VetrinaDolci.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("VetrinaDolci.Client", LogLevel.Debug)
                    .AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Hello World!");
            logger.LogInformation("");

            logger.LogInformation("###############  TEST Auth  ###############");
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                logger.LogInformation(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "vetrinadolci.webapi"
            });

            if (tokenResponse.IsError)
            {
                logger.LogInformation(tokenResponse.Error);
                return;
            }

            ConsoleWriteJson(tokenResponse.Json.ToString(), logger);
            logger.LogInformation("");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            string url = "https://localhost:6001/identity";
            logger.LogInformation($"call api: {url}");
            var response = await apiClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogInformation("StatusCode", response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                logger.LogInformation("response.Content", JArray.Parse(content));
            }
            logger.LogInformation("");

            string weatherForecasturl = "https://localhost:6001/WeatherForecast";
            logger.LogInformation($"call api: {weatherForecasturl}");
            response = await apiClient.GetAsync(weatherForecasturl);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogInformation("response.Content", response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                logger.LogInformation("response.Content", JArray.Parse(content));
            }
            logger.LogInformation("");

            logger.LogInformation("###############  TEST Seed data da CSV a Sql Lite  ###############");
            using (var db = new ApplicationContext())
                await SeedHelper.SeedFromCsv(logger, db);

            // Keep the console window open in debug mode.
            logger.LogInformation("Press any key to exit.");
            Console.ReadKey();
        }

        static void ConsoleWriteJson(string json, ILogger logger)
        {
            JObject parsed = JObject.Parse(json);

            foreach (var pair in parsed)
            {
                logger.LogInformation("{0}: {1}", pair.Key, pair.Value);
            }
        }
    }
}
