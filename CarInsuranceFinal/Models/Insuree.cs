//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarInsuranceFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Insuree
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "DOB is required")]

        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Car Year is required")]
        public int CarYear { get; set; }
        [Required(ErrorMessage = "Car Make is required")]
        public string CarMake { get; set; }
        [Required(ErrorMessage = "Car Model is required")]
        public string CarModel { get; set; }
        public bool DUI { get; set; }
        [Required(ErrorMessage = "Speeding Tickets is required")]
        public int SpeedingTickets { get; set; }
        public bool CoverageType { get; set; }
        public decimal Quote { get; set; }
    }
}
