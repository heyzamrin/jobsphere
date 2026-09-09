using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjectOne.Models;
using System.IO;



namespace MVCProjectOne.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        MVCProject1Entities dbobj = new MVCProject1Entities();

        public ActionResult LoginPageLoad()
        {
            return View();
        }
       
       
        public ActionResult CompanyHomeClick()
        {
            return View();
        }
        public ActionResult LoginClick(LoginClass objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.spLoginCountId(objcls.lEmail, objcls.lPassword).FirstOrDefault();
                if (val == 1)
                {
                    var cid = dbobj.spLoginId(objcls.lEmail, objcls.lPassword).FirstOrDefault();
                    Session["cid"] = cid;

                    var lt = dbobj.spLoginType(objcls.lEmail, objcls.lPassword).FirstOrDefault();
                    if (lt == "Employee")
                    {
                        return RedirectToAction("EmployeePageLoad", "JobSearch");
                    }
                    else if (lt == "Company")
                    {
                        return RedirectToAction("CompanyHomeClick");
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
   