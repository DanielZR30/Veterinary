using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinary.ViewModels
{
    public class PetViewModel
    {
        public string PetName { get; set; }
        public decimal PetWeight { get; set; }
        public byte PetAge { get; set; }

        public Guid OwnerId { get; set; }

        public Guid SpeciesId { get; set; }
    }
}