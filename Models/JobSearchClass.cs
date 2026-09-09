using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjectOne.Models
{
    public class JobSearchClass
    {
        public string JobTitle { get; set; }
        public string JobSkills { get; set; }
        public string JobExperience { get; set; }
        public string JobLocation { get; set; }

        public List<CompanyJobDetailsTab> SelectJob { get; set; }

        public JobSearchClass()
        {
            SelectJob = new List<CompanyJobDetailsTab>();
        }
    }
}