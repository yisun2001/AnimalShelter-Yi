using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainServices
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
    }
}
