using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface IResidenceRepository
    {
        IEnumerable<Residence> GetAllResidences();

        Residence GetResidence(int Id);

        Residence DeleteResidence(int Id);

        Residence CreateResidence(Residence residence);

        Residence UpdateResidence(Residence residence);
    }
}
