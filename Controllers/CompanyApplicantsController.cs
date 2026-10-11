
using System;
using System.Linq;
using System.Web.Mvc;
using MVCProjectOne.Models;

namespace MVCProjectOne.Controllers
{
    public class CompanyApplicantsController : Controller
    {
        MVCProject1Entities dbobj = new MVCProject1Entities();

        public ActionResult CompanyHomePageLoad()
        {
            if (Session["cid"] == null)
            {
                return RedirectToAction("LoginPageLoad", "Login");
            }

            int companyId = Convert.ToInt32(Session["cid"]);

            var applications = dbobj.JobApplicationTabs
                .Where(x => x.CompanyJobDetailsTab.CompanyId == companyId)
                .ToList();

            return View("~/Views/CompanyReg/CompanyHomePageLoad.cshtml",
                        applications);
        }
    }
}
