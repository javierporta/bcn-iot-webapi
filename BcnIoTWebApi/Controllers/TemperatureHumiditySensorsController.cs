﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcnIoTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureHumiditySensorsController : ControllerBase
    {
        // GET: api/<TemperatureHumiditySensors>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TemperatureHumiditySensors>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
