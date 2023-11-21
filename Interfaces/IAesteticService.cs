using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    public interface IAestheticService
    {
        Task<AestheticService> CreateAestheticService(AestheticService aestheticService);
        Task<AestheticService> GetAestheticServiceById(Guid aestheticServiceId);
        Task<IEnumerable<AestheticService>> GetAestheticServices();
        Task<AestheticService> UpdateAestheticService(AestheticService aestheticService);
        Task<AestheticService> DeleteAestheticService(Guid aestheticServiceId);
        Task<IEnumerable<AestheticService>> GetAestheticServicesByPet(Guid petId);
    }
}
