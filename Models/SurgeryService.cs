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
    
    public partial class SurgeryService
    {
        public string IDMedicalService { get; set; }
        public Nullable<System.DateTime> MedicalServiceDate { get; set; }
        public string MedicalServiceDescription { get; set; }
        public string MedicalServicEstatus { get; set; }
        public string IDPetRecord { get; set; }
        public string IDDoctor { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Estatus Estatus { get; set; }
        public virtual PetRecord PetRecord { get; set; }
    }
}
