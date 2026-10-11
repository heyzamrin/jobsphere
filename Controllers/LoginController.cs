
using System;
using System.Linq;
using System.Web.Mvc;
using MVCProjectOne.Models;

namespace MVCProjectOne.Controllers
{
    public class LoginController : Controller
    {
        MVCProject1Entities dbobj = new MVCProject1Entities();

        public ActionResult LoginPageLoad()
        {
            return View();
        }

        public ActionResult CompanyHomeClick()
        {
            if (Session["cid"] == null)
            {
                return RedirectToAction("LoginPageLoad", "Login");
            }

            if (Session["LoginType"] == null ||
                Session["LoginType"].ToString() != "Company")
            {
                return RedirectToAction("HomeRedirect", "Login");
            }

            int companyId = Convert.ToInt32(Session["cid"]);

            var applications = dbobj.JobApplicationTabs
                .Where(x => x.CompanyJobDetailsTab.CompanyId == companyId)
                .ToList();

            return View(applications);
        }

        public ActionResult HomeRedirect()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("LoginPageLoad", "Login");
            }

            if (Session["LoginType"].ToString() == "Company")
            {
                return RedirectToAction("CompanyHomeClick", "Login");
            }
            else if (Session["LoginType"].ToString() == "Employee")
            {
                return RedirectToAction("EmployeePageLoad", "JobSearch");
            }

            return RedirectToAction("LoginPageLoad", "Login");
        }

        public ActionResult LoginClick(LoginClass objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.spLoginCountId(
                    objcls.lEmail,
                    objcls.lPassword
                ).FirstOrDefault();

                if (val == 1)
                {
                    var cid = dbobj.spLoginId(
                        objcls.lEmail,
                        objcls.lPassword
                    ).FirstOrDefault();

                    Session["cid"] = cid;

                    var lt = dbobj.spLoginType(
                        objcls.lEmail,
                        objcls.lPassword
                    ).FirstOrDefault();

                    Session["LoginType"] = lt;

                    if (lt == "Employee")
                    {
                        return RedirectToAction(
                            "EmployeePageLoad",
                            "JobSearch"
                        );
                    }
                    else if (lt == "Company")
                    {
                        return RedirectToAction(
                            "CompanyHomeClick",
                            "Login"
                        );
                    }
                    else
                    {
                        ModelState.Clear();
                        objcls.lMsg = "Invalid Email and Password";
                        return View("LoginPageLoad");
                    }
                }
                else
                {
                    ModelState.Clear();
                    objcls.lMsg = "Invalid Email and Password";
                    return View("LoginPageLoad");
                }
            }

            return View("LoginPageLoad");
        }
    }
}
