using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class SensorS1Service : ISensorS1Service
    {
        private readonly ICosmosDbService<SensorS1Data> _cosmosDbServiceSensorS1;

        public SensorS1Service(ICosmosDbService<SensorS1Data> cosmosDbServiceSensorS1)
        {
            _cosmosDbServiceSensorS1 = cosmosDbServiceSensorS1;
        }
        public async Task<SensorS1Data> GetS1CurrentValues()

        {
            var serviceResult = await _cosmosDbServiceSensorS1.GetItemsAsync("SELECT TOP 1 * FROM c order by c.timestamp DESC");
            var lastRecord = serviceResult.FirstOrDefault();
            return lastRecord;

        }

        public async Task<IEnumerable<SensorS1Data>> GetAll()
        {
            return await _cosmosDbServiceSensorS1.GetItemsAsync("SELECT * FROM c");

        }
    }
}
