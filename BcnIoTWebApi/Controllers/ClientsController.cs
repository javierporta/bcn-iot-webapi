using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcnIoTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<ClientData>> Get()
        {
            var result = await _clientService.GetAll();
            return result;
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientData>> Get(string id)
        {
            var result = await _clientService.GetClientById(id);
            if (result == null)
            {
                return NotFound();
            }

            return result;

        }

        // POST api/<ClientsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ClientDataToUpdate value)
        {
            var result = await _clientService.UpdateClient(id, value);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
