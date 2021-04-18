using System;

namespace Models
{

    using Newtonsoft.Json;
    public class SensorS1Data : IGenericCosmosDbItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "temperature")]
        public double Temperature { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public double Humidity { get; set; }

        [JsonProperty(PropertyName = "mac")]
        public string Mac { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
