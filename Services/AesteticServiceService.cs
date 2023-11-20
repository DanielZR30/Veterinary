using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class AestheticServiceService : IAestheticService
    {
        private readonly VeterinaryEntities _context;

        public AestheticServiceService(VeterinaryEntities context)
        {
            _context = context;
        }

        public async Task<AestheticService> CreateAestheticService(AestheticService aestheticService)
        {
            _context.AestheticService.Add(aestheticService);
            await _context.SaveChangesAsync();
            return aestheticService;
        }

        public async Task<AestheticService> DeleteAestheticService(Guid aestheticServiceId)
        {
            AestheticService aestheticService = await GetAestheticServiceById(aestheticServiceId);
            if (aestheticService != null)
            {
                _context.AestheticService.Remove(aestheticService);
                await _context.SaveChangesAsync();
            }

            return aestheticService;
        }

        public async Task<AestheticService> GetAestheticServiceById(Guid aestheticServiceId)
        {
            return await _context.AestheticService
                                .FirstOrDefaultAsync(s => s.IDAestheticService == aestheticServiceId);
        }

        public async Task<IEnumerable<AestheticService>> GetAestheticServices()
        {
            return await _context.AestheticService.ToListAsync();
        }

        public async Task<IEnumerable<AestheticService>> GetAestheticServicesByPet(Guid petId)
        {
            return await _context.AestheticService
                                .Where(s => s.Pet.IDPet == petId)
                                .ToListAsync();
        }

        public async Task<AestheticService> UpdateAestheticService(AestheticService aestheticService)
        {
            _context.Entry(aestheticService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return aestheticService;
        }
    }
}