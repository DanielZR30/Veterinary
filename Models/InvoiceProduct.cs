//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Veterinary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceProduct
    {
        public System.Guid ProductID { get; set; }
        public System.Guid InvoiceID { get; set; }
        public byte Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
