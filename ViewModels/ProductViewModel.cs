using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veterinary.Models;

namespace Veterinary.ViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public Guid IDCategoria { get; set; }
        
    }
}