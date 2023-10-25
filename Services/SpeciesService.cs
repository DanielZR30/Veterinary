using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veterinary.Interfaces;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly VeterinaryEntities _context;

        public SpeciesService(VeterinaryEntities context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Species>> GetSpecies()
        {
            return await _context.Species.OrderBy(s => s.SpeciesName).ToListAsync();
        }

        public async Task<Species> GetSpeciesById(Guid speciesId)
        {
            return await _context.Species.FirstOrDefaultAsync(s => s.IDSpecies.Equals(speciesId));
        }
    }
}