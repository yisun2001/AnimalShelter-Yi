using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface ITreatmentRepository
    {
        IEnumerable<Treatment> GetAllTreatments();

        Treatment GetTreatment(int Id);

        Treatment DeleteTreatment(int Id);

        Treatment CreateTreatment(Treatment treatment, int Id);

        Treatment UpdateTreatment(Treatment treatment, int Id);
    }
}
