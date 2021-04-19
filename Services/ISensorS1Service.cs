using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ISensorS1Service
    {
        Task<SensorS1Data> GetS1CurrentValues();
        Task<IEnumerable<SensorS1Data>> GetAll();
        Task<IEnumerable<SensorS1Data>> GetAllByClient(string clientId);
    }
}
