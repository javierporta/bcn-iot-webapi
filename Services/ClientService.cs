using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly ICosmosDbService<ClientData> _cosmosDbServiceClient;


        public ClientService(ICosmosDbService<ClientData> cosmosDbServiceClient)
        {
            _cosmosDbServiceClient = cosmosDbServiceClient;
        }
        public async Task<IEnumerable<ClientData>> GetAll()
        {
            return await _cosmosDbServiceClient.GetItemsAsync("SELECT * FROM c");
        }

        public async Task<bool> UpdateClient(string id, ClientDataToUpdate clientDataToUpdate)
        {
            var clientData = await GetClientById(id);

            if (clientData is null)
            {
                return false;
            }

            clientData.Name = clientDataToUpdate.Name;
            clientData.TemperatureHighThreshold = clientDataToUpdate.TemperatureHighThreshold;
            clientData.TemperatureLowThreshold = clientDataToUpdate.TemperatureLowThreshold;

            await _cosmosDbServiceClient.UpdateItemAsync(id, clientData);
            return true;
        }

        public async Task<ClientData> GetClientById(string id)
        {
            return await _cosmosDbServiceClient.GetItemAsync(id);
        }
    }
}
