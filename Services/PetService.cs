﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.ViewModels;

namespace Veterinary.Services
{
    public class PetService : IPetService
    {
        private readonly VeterinaryEntities _context;

        public PetService(VeterinaryEntities context)
        {
            _context = context;
        }

        public async Task<Pet> CreatePet(Pet pet)
        {
            Pet p = await _context.Pet.FirstOrDefaultAsync(pe => pe.PetName == pet.PetName);
            if (p == null)
            {
                if (pet.Customer == null) throw new Exception("No existe el usuario");
                if (pet.Species == null) throw new Exception("No existe la especies");
                p = _context.Pet.Add(pet);
                await _context.SaveChangesAsync();
            }

            return p;
        }

        public async Task<Pet> DeletePet(Guid petId)
        {
            Pet p = _context.Pet.FirstOrDefault(pr => pr.IDPet == petId);
            try
            {
                if (p != null)
                {
                    _context.Pet.Remove(p);
                    await _context.SaveChangesAsync();
                }
                return p;
            }
            catch (Exception ex)
            {
                return p;
            }
        }

        public async Task<IEnumerable<PetDataViewModel>> GetPetData()
        {
            throw new NotImplementedException();
        }
        public async Task<Pet> GetPetById(Guid petId)
        {
            return await _context.Pet.FirstOrDefaultAsync(p => p.IDPet.Equals(petId));
        }

        public async Task<IEnumerable<Pet>> GetPetBySpecies(Guid speciesId)
        {
            IEnumerable<Pet> Pets = await _context.Pet.Where(p => p.IDSpecies == speciesId).ToListAsync();
            return Pets;
        }

        public async Task<IEnumerable<Pet>> GetPets()
        {
            return await _context.Pet.ToListAsync();
        }

        public async Task<Pet> UpdatePet(Pet pet)
        {
            Pet p = _context.Pet.FirstOrDefault(pr => pr.IDPet == pet.IDPet);
            try
            {
                p.PetName = pet.PetName;
                p.PetWeight = pet.PetWeight;
                p.PetAge = pet.PetAge;
                await _context.SaveChangesAsync();
                return p;
            }
            catch (Exception ex)
            {
                return p;
            }
        }
    }
}