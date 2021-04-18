using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Core;
using Models;
using Services;

namespace BcnIoTWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BcnIoTWebApi", Version = "v1" });
            });

            services.AddSingleton<ICosmosDbService<SensorS1Data>>(InitializeCosmosClientInstanceSensorDataAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosDbService<ClientData>>(InitializeCosmosClientInstanceClientDataAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            services.AddScoped<ISensorS1Service, SensorS1Service>();
            services.AddScoped<IClientService, ClientService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BcnIoTWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        private static async Task<CosmosDbService<SensorS1Data>> InitializeCosmosClientInstanceSensorDataAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("SensorsContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService<SensorS1Data> cosmosDbService = new CosmosDbService<SensorS1Data>(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/_partitionKey");

            return cosmosDbService;
        }

        private static async Task<CosmosDbService<ClientData>> InitializeCosmosClientInstanceClientDataAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ClientsContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService<ClientData> cosmosDbService = new CosmosDbService<ClientData>(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            //Upsert client seeding data
            if (await cosmosDbService.GetItemAsync(CreateBcnOfficeClient().Id) == null)
            {
                await cosmosDbService.AddItemAsync(CreateBcnOfficeClient());
            }
            else
            {
                await cosmosDbService.UpdateItemAsync(CreateBcnOfficeClient().Id, CreateBcnOfficeClient());
            }

            return cosmosDbService;
        }

        private static ClientData CreateBcnOfficeClient()
        {
            return new()
            {
                Id = "oifjweweo$ineogsef27r3893r_273y2huiwfeg",
                Name = "Javier - Barcelona Office",
                RegisteredDevices = new[] { "AC233FA2572E", "AC233FA25791", "AC233FA256A7" },
                TemperatureHighThreshold = 25,
                TemperatureLowThreshold = 5
            };
        }
    }
}
