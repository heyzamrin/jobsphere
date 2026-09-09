using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjectOne.Models;

namespace MVCProjectOne.Controllers
{
    public class CompanyRegController : Controller
    {
        // GET: CompanyReg
        MVCProject1Entities dbobj = new MVCProject1Entities();
        public ActionResult InsertCompanyPageLoad()
        {
            return View();
        }
        public ActionResult InsertCompanyclick(CompanyInsertClass clsobj)
        {
            if (true)
            {
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

                dbobj.spCompanyReg(
     regid,
     clsobj.cName,
     clsobj.cAddress,
     clsobj.cPhone,
     clsobj.cEmail,
    clsobj.cPassword

 );

                dbobj.spLoginInsert(
                    regid,
                    clsobj.cEmail,
                    clsobj.cPassword,
                    "Company"
                );
                clsobj.cMsg = "successfully inserted";

                return View("InsertCompanyPageLoad", clsobj);
            }

            return View("InsertCompanyPageLoad", clsobj);
        }
        public ActionResult AddCompanyJobDetailsPageLoad()
        {
            return View();
        }
    }


}