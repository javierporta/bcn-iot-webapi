using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IClientService
    {
        Task<ClientData> GetClientById(string id);
        Task<IEnumerable<ClientData>> GetAll();
        Task UpdateClient(string id, ClientDataToUpdate clientDataToUpdate);
    }
}
