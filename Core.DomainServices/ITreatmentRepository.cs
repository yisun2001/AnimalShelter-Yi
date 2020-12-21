using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        Treatment CreateTreatment(Treatment treatment);

        Treatment DeleteTreatment(int Id);

        Treatment GetTreatment(int Id);

        Treatment UpdateTreatment(Treatment treatment);
    }
}
