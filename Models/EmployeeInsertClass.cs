using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVCProjectOne.Models
{
    public class EmployeeInsertClass
    {
        public int eId { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        public string eName { get; set; }

        [Range(18, 50, ErrorMessage = "Enter the age")]
        public int eAge { get; set; }

        [Required(ErrorMessage = "Enter the address")]
        public string eAddress { get; set; }
        [Required(ErrorMessage = "Enter the Phone")]
        [RegularExpression(@"(\d{10})$", ErrorMessage = "Enter valid number")]
        public string ePhone { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string eEmail { get; set; }
        public string eQualification { get; set; }
        public string eSkill { get; set; }
        public string eExperience { get; set; }
        
        public string ePhoto { get; set; }
        public string eStatus { get; set; }
        public string eGender { get; set; }

        public string eUsername { get; set; }

        public string ePassword { get; set; }

        [Compare("ePassword", ErrorMessage = "Password mismatch")]
        public string eConfirmPassword { get; set; }

        public string eMsg { get; set; }
    }
}
