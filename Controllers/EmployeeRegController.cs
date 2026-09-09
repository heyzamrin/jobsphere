using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjectOne.Models;
using System.IO;

namespace MVCProjectOne.Controllers
{
    public class EmployeeRegController : Controller
    {
        // GET: EmployeeReg
        MVCProject1Entities dbobj = new MVCProject1Entities();
        public ActionResult InsertEmployeePageLoad()

        {
            return View();
        }
        public ActionResult InsertEmployeeclick(EmployeeInsertClass clsobj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/Photos");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~\\Photos", fname);
                    clsobj.ePhoto = fullpath; // set
                }

                var getmaxid = dbobj.spMaxLogId().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;

                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }

                // get
                dbobj.spEmployeeReg(
                regid,
                clsobj.eName,
                clsobj.eAge,
                clsobj.eAddress,
                clsobj.ePhone,
                clsobj.eEmail,
                clsobj.eQualification,
                clsobj.eSkill,
                clsobj.eExperience,
                clsobj.ePhoto,
                "active",
                clsobj.eGender
                );

                dbobj.spLoginInsert(
                    regid,
                    clsobj.eEmail,
                    clsobj.ePassword,
                    "Employee"
                );

                clsobj.eMsg = "successfully inserted";

                return View("InsertEmployeePageLoad", clsobj);
            }

            return View("InsertEmployeePageLoad", clsobj);
        }
    }
}
