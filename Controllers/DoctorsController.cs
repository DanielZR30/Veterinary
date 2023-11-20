using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.Services;
using Veterinary.ViewModels;

namespace Veterinary.Controllers
{
    [EnableCors(origins: "http://localhost:54641", headers: "*", methods: "*")]
    public class DoctorsController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly IDoctorService _doctorService = new DoctorService(_context);

        #region Create

        // CREATE
        [HttpPost]
        [Route("api/doctors/create")]
        public async Task<object> CreateDoctor([FromBody] DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Doctor doctor = new Doctor
                    {
                        DoctorName = doctorViewModel.DoctorName,
                        DoctorLastname = doctorViewModel.DoctorLastname,
                        DoctorPhone = doctorViewModel.DoctorPhone
                    };
                    await _doctorService.CreateDoctor(doctor);
                    return Ok(doctor);
                }
                catch (DbUpdateException dbEx)
                {
                    // handle error
                }
                catch (Exception ex)
                {
                    // handle error
                }
            }

            return BadRequest(ModelState);
        }

        // READ
        [HttpGet]
        [Route("api/doctors")]
        public async Task<object> GetDoctors()
        {
            IEnumerable<Doctor> doctors = await _doctorService.GetDoctors();
            return Ok(doctors);
        }

        [HttpGet]
        [Route("api/doctors/{doctorId}")]
        public async Task<object> GetDoctor(Guid doctorId)
        {
            Doctor doctor = await _doctorService.GetDoctorById(doctorId);
            return Ok(doctor);
        }

        // UPDATE  
        [HttpPut]
        [Route("api/doctors/{doctorId}")]
        public async Task<object> UpdateDoctor(Guid doctorId, [FromBody] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _doctorService.UpdateDoctor(doctor);
                    return Ok(doctor);
                }
                catch (Exception ex)
                {
                    // handle error 
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE
        [HttpDelete]
        [Route("api/doctors/{doctorId}")]
        public async Task<object> DeleteDoctor(Guid doctorId)
        {
            Doctor doctor = await _doctorService.GetDoctorById(doctorId);
            await _doctorService.DeleteDoctor(doctorId);
            return Ok(doctor);
        }
    }
    #endregion
}
