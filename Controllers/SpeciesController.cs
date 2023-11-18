using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.Services;

namespace Veterinary.Controllers
{
    [EnableCors(origins: "http://localhost:54641", headers: "*", methods: "*")]
    public class SpeciesController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();

        private readonly ISpeciesService _speciesService = new SpeciesService(_context);

        #region Read
        [HttpGet]
        [Route("api/species")]
        public async Task<IHttpActionResult> GetSpecies()
        {
            try
            {
                IEnumerable<Species> species = await _speciesService.GetSpecies();
                return Ok(species);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        #endregion
    }
}
