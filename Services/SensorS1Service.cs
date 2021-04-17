using Models;

namespace Services
{
    public class SensorS1Service : ISensorS1Service
    {
        public SensorS1Data GeS1CurrentValues()
        {
            return new SensorS1Data { Humidity = 57, Temperature = 24 };
        }
    }
}
