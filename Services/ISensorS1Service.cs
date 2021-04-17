using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services
{
    public interface ISensorS1Service
    {
        SensorS1Data GeS1CurrentValues();
    }
}
