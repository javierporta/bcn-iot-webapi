using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Models;

namespace Services
{
    public class SensorS1Service : ISensorS1Service
    {
        private readonly ICosmosDbService<SensorS1Data> _cosmosDbServiceSensorS1;
        private readonly IClientService _clientService;

        public SensorS1Service(ICosmosDbService<SensorS1Data> cosmosDbServiceSensorS1, IClientService clientService)
        {
            _cosmosDbServiceSensorS1 = cosmosDbServiceSensorS1;
            _clientService = clientService;

        }
        public async Task<SensorS1Data> GetS1CurrentValues(string mac)

        {
            var queryDefinition = new QueryDefinition("SELECT TOP 1 * FROM c WHERE c.mac = @mac ORDER BY c.timestamp DESC")
                   .WithParameter("@mac", mac); //Todo Add registered clients check
            var serviceResult = await _cosmosDbServiceSensorS1.GetItemsAsync(queryDefinition);
            var lastRecord = serviceResult.FirstOrDefault();
            return lastRecord;

        }

        public async Task<IEnumerable<SensorS1Data>> GetAll()
        {
            return await _cosmosDbServiceSensorS1.GetItemsAsync("SELECT TOP 100 * FROM c ORDER BY c.timestamp DESC");

        }

        public async Task<IEnumerable<SensorS1Data>> GetAllByClient(string clientId)
        {
            var client = await _clientService.GetClientById(clientId);
            if (client == null)
            {
                return null;
            }

            var queryDefinition =
                new QueryDefinition($"SELECT TOP 100 * FROM c WHERE ARRAY_CONTAINS(@registeredDevices, c.mac) ORDER BY c.timestamp DESC")
                    .WithParameter("@registeredDevices", client.RegisteredDevices);
            return await _cosmosDbServiceSensorS1.GetItemsAsync(queryDefinition);
        }

    }
}
