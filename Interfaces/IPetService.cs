using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;
using Veterinary.ViewModels;

namespace Veterinary.Interfaces
{
    internal interface IPetService
    {
        Task<Pet> CreatePet(Pet pet);

        Task<Pet> GetPetById(Guid petId);

        Task<IEnumerable<Pet>> GetPetBySpecies(Guid speciesId);

        Task<IEnumerable<Pet>> GetPets();

        Task<IEnumerable<PetDataViewModel>> GetPetData();

        Task<Pet> UpdatePet(Pet pet);

        Task<Pet> DeletePet(Guid petId);
        Task<IEnumerable<Pet>> GetPetByCustomer(Guid customerId);
    }
}
