using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcnIoTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureHumiditySensorsController : ControllerBase
    {
        private readonly ISensorS1Service _sensorS1Service;
        public TemperatureHumiditySensorsController(ISensorS1Service sensorS1Service)
        {
            _sensorS1Service = sensorS1Service;
        }

        // GET: api/<TemperatureHumiditySensors>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorS1Data>>> Get(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return Unauthorized("No clientId provided");
            }

            var serviceResult = await _sensorS1Service.GetAllByClient(clientId);

            return serviceResult.ToList();
        }

        // GET api/<TemperatureHumiditySensors>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorS1Data>> Get(int id, string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return Unauthorized("No clientId provided");
            }

            var serviceResult = await _sensorS1Service.GetS1CurrentValues();

            return serviceResult;
        }
    }
}
