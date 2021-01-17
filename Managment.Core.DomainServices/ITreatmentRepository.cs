using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface ITreatmentRepository
    {
        IEnumerable<Treatment> GetAllTreatments();
        Treatment GetById(int id);
        Task AddTreatment(Treatment newtreament);
        IEnumerable<Treatment> GetByAnimalId(int id);
        Task DeleteTreatment(Treatment treatment);
        Task UpdateTreatment(Treatment treatment);

    }
}
