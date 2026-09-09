using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjectOne.Models;
using System.IO;

namespace MVCProjectOne.Controllers
{
    public class JobInsertController : Controller
    {
        // GET: JobInsert
        MVCProject1Entities dbobj = new MVCProject1Entities();
        public ActionResult JobInsertPageLoad()
        {
            return View();
        }
        public ActionResult JobInsertClick(JobInsertClass clsobj)
        {
            if (ModelState.IsValid)
            {
                int companyid = Convert.ToInt32(Session["cid"]);

                dbobj.spCompanyJobInsert(
                    companyid,
                    clsobj.jTitle,
                    clsobj.jExperience,
                    clsobj.jSkill,
                    clsobj.jLocation,
                    clsobj.jQualification,
                    clsobj.jSalary,
                    clsobj.jPostDate,
                    clsobj.jEndDate,
                    "Active"
                );

                clsobj.jMsg = "Job details inserted successfully";

                return View("JobInsertPageLoad", clsobj);
            }

            return View("JobInsertPageLoad", clsobj);
        }
    }
}