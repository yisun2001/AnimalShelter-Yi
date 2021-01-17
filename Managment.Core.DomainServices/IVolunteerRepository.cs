using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IVolunteerRepository
    {

        Task AddVolunteer(Volunteer volunteer);

        Volunteer GetVolunteerByEmail(string email);
    }
}
