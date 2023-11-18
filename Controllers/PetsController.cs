using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PetsController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly IPetService _petService = new PetService(_context);
        private readonly ICustomerService _customerService = new CustomerService(_context);
        private readonly ISpeciesService _speciesService = new SpeciesService(_context);

        #region Create
        [HttpPost]
        [Route("api/pets/create")]
        public async Task<object> CreatePet([FromBody] PetViewModel pet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Pet p = new Pet
                    {
                        IDPet = Guid.NewGuid(),
                        IDCustomer = pet.OwnerId,
                        IDSpecies = pet.SpeciesId,
                        PetName = pet.PetName,
                        PetAge = pet.PetAge,
                        PetWeight = pet.PetWeight,
                        Species = await _speciesService.GetSpeciesById(pet.SpeciesId),
                        Customer = await _customerService.GetCustomerById(pet.OwnerId)
                    };
                    await _petService.CreatePet(p);
                    return Ok<Pet>(p);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("api/pets")]
        public async Task<IHttpActionResult> GetPets()
        {
            try
            {
                IEnumerable<Pet> pets = await _petService.GetPets();
                return Ok(pets);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/pets/{petId}")]
        public async Task<object> GetPetById(Guid petId)
        {
            try
            {
                Pet pet = await _petService.GetPetById(petId);
                return Ok(pet);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/pets/species/{speciesId}")]
        public async Task<object> GetPetBySpecies(Guid speciesId)
        {
            try
            {
                IEnumerable<Pet> pets = await _petService.GetPetBySpecies(speciesId);
                return Ok(pets);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("api/pets/{petId}")]
        public async Task<object> UpdatePet(Guid petId, [FromBody] Pet pet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Pet p = await _petService.UpdatePet(pet);
                    return Ok<Pet>(p);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("api/pets/{petId}")]
        public async Task<object> DeletePet(Guid petId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Pet p = await _petService.DeletePet(petId);
                    return Ok(p);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion
    }
}