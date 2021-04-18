using Newtonsoft.Json;

namespace Models
{
    public class ClientData : IGenericCosmosDbItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "registeredDevices")]
        public string[] RegisteredDevices { get; set; }

        [JsonProperty(PropertyName = "temperatureHighThreshold")]
        public double TemperatureHighThreshold { get; set; }

        [JsonProperty(PropertyName = "temperatureLowThreshold")]
        public double TemperatureLowThreshold { get; set; }
    }
}
