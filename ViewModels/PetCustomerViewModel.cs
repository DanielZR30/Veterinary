using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinary.ViewModels
{
    public class PetCustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }

        public string OwnerName { get; set; }
    }
}