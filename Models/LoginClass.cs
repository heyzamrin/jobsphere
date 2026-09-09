using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVCProjectOne.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "***")]
        public string lEmail { get; set; }

        [Required(ErrorMessage = "***")]
        public string lPassword { get; set; }
        public string lMsg { get; set; }
    }
}