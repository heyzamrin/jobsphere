using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProjectOne.Models
{
    public class JobInsertClass
    {
        public int jId { get; set; }

        [Required(ErrorMessage = "Enter the Job Title")]
        public string jTitle { get; set; }

        [Required(ErrorMessage = "Enter the Experience")]
        public string jExperience { get; set; }

        [Required(ErrorMessage = "Enter the Skill")]
        public string jSkill { get; set; }

        [Required(ErrorMessage = "Enter the Location")]
        public string jLocation { get; set; }

        [Required(ErrorMessage = "Enter the Qualification")]
        public string jQualification { get; set; }

        [Required(ErrorMessage = "Enter the Salary")]
        public decimal jSalary { get; set; }

        [Required(ErrorMessage = "Enter the Post Date")]
        public DateTime jPostDate { get; set; }

        [Required(ErrorMessage = "Enter the End Date")]
        public DateTime jEndDate { get; set; }

        public string jStatus { get; set; }

        public string jMsg { get; set; }
    }
}