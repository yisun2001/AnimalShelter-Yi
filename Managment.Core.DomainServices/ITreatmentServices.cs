using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface ITreatmentServices
    {
        Task AddTreatment(Treatment treatment);
        Task UpdateTreatment(Treatment treatment);

    }
}
