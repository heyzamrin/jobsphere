using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCProjectOne.Models
{
    public class CompanyInsertClass
    {
        [Required(ErrorMessage = "Enter the Name")]
        public string cName { get; set; }

        [Required(ErrorMessage = "Enter the address")]
        public string cAddress { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string cEmail { get; set; }

        [Required(ErrorMessage = "Enter the Phone")]
        [RegularExpression(@"(\d{10})$", ErrorMessage = "Enter valid number")]
        public string cPhone { get; set; }
        public string cPassword { get; set; }

        [Compare("cPassword", ErrorMessage = "Password mismatch")]
        public string cConfirmPassword { get; set; }

        public string cMsg { get; set; }
    }
}
