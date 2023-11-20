using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    internal interface IDoctorService
    {
        Task<Doctor> CreateDoctor(Doctor doctor);

        Task<Doctor> GetDoctorById(Guid doctorId);

        Task<IEnumerable<Doctor>> GetDoctors();

        Task<Doctor> UpdateDoctor(Doctor doctor);

        Task<Doctor> DeleteDoctor(Guid doctorId);
    }
}
