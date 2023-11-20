using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinary.ViewModels
{
    public class DoctorViewModel
    {
        public Guid IDDoctor { get; set; }

        [Required]
        [StringLength(50)]
        public string DoctorName { get; set; }

        [Required]
        [StringLength(50)]
        public string DoctorLastname { get; set; }

        [Required]
        [StringLength(10)]
        public string ProfessionalCard { get; set; }

        [Required]
        [Phone]
        public string DoctorPhone { get; set; }
    }
}