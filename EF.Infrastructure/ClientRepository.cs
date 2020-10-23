using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class ClientRepository : IClientRepository
    {
        private readonly AnimalShelterDbContext _context;

        public ClientRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients;
        }
    }
}
