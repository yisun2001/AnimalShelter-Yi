using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IVolunteerRepository
    {
        IEnumerable<Volunteer> GetAllVolunteers();

        Volunteer CreateVolunteer(Volunteer volunteer);

        Volunteer GetVolunteerByEmail(string email);
    }
}
