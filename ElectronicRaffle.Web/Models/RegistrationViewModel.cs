using ElectronicRaffle.Data;
using ElectronicRaffle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicRaffle.Web.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "School")]
        public School School { get; set; }

        [Required]
        [Display(Name = "Contact No.")]
        public string ContactNumber { get; set;}

        public List<School> Schools
        {
            get
            {
                return SchoolRepository.GetArray().ToList();
            }
        }
    }
}