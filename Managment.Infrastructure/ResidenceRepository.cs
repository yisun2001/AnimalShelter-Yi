using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Infrastructure
{
    public class ResidenceRepository : IResidenceRepository
    {

        private readonly AnimalDbContext _context;

        public ResidenceRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddResidence(Residence newresidence)
        {
            await _context.Residences.AddAsync(newresidence);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Residence> GetAllResidences(string type = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                return _context.Residences.Where(residence =>
                                                 
                                                 residence.Type.Equals((AnimalType)Enum.Parse(typeof(AnimalType), type))

                );

            }
            else {
             return _context.Residences;
            }
                
        }

        public Residence GetById(int id)
        {
            return  _context.Residences.SingleOrDefault(Residence => Residence.Id == id);
        }
    }
}
