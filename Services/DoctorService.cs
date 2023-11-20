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
    public class DoctorService : IDoctorService
    {
        private readonly VeterinaryEntities _context;

        public DoctorService(VeterinaryEntities context)
        {
                _context = context;
        }

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            Doctor d = await _context.Doctor.FirstOrDefaultAsync(doc => doc.ProfessionalCard.Equals(doctor.ProfessionalCard));
            if (d == null)
            {
                _context.Doctor.Add(doctor);
                _context.SaveChanges();
                return doctor;

            }

            return d;
        }

        public async Task<Doctor> DeleteDoctor(Guid doctorId)
        {
            Doctor d = _context.Doctor.FirstOrDefault(doc => doc.IDDoctor == doctorId);
            try
            {
                if (d != null)
                {
                    _context.Doctor.Remove(d);
                    await _context.SaveChangesAsync();
                }
                return d;
            }
            catch (Exception ex)
            {
                return d;
            }
        }

        public Task<Doctor> GetDoctorById(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetDoctors()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> UpdateDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}