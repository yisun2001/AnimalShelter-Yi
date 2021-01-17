using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IResidenceRepository
    {
        IEnumerable<Residence> GetAllResidences(string type = null);
        Residence GetById(int id);
        Task AddResidence(Residence newresidence);
    }
}
