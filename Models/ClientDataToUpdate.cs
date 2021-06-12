using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ClientDataToUpdate
    {
        public string Name { get; set; } = string.Empty;
        public double TemperatureHighThreshold { get; set; } = 0;
        public double TemperatureLowThreshold { get; set; } = 0;
    }
}
