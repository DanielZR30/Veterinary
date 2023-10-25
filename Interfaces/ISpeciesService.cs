using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    internal interface ISpeciesService
    {
        Task<IEnumerable<Species>> GetSpecies();
        Task<Species> GetSpeciesById(Guid speciesId);
    }
}
