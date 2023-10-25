using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinary.ViewModels
{
    public class PetDataViewModel
    {
        public string PetName { get; set; }
        public byte PetAge { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;

        public Guid SpeciesId { get; set; }
        public string SpeciesName { get; set;} = string.Empty;
    }
}