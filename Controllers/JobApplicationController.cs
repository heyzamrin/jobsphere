
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProjectOne.Models;

namespace MVCProjectOne.Controllers
{
    public class JobApplicationController : Controller
    {
        MVCProject1Entities dbobj = new MVCProject1Entities();

        // Display job details and resume upload page
        public ActionResult ApplyJobPageLoad(int id)
        {
            if (Session["cid"] == null)
            {
                return RedirectToAction("LoginPageLoad", "Login");
            }

            var job = dbobj.CompanyJobDetailsTabs
                .FirstOrDefault(x => x.JobId == id);

            if (job == null)
            {
                return RedirectToAction("EmployeePageLoad", "JobSearch");
            }

            ViewBag.Job = job;

            return View();
        }

        // Upload resume and save application
        [HttpPost]
        public ActionResult ApplyJobClick(int id, HttpPostedFileBase resume)
        {
            if (Session["cid"] == null)
            {
                return RedirectToAction("LoginPageLoad", "Login");
            }

            int empid = Convert.ToInt32(Session["cid"]);

            var job = dbobj.CompanyJobDetailsTabs
                .FirstOrDefault(x => x.JobId == id);

            if (job == null)
            {
                return RedirectToAction("EmployeePageLoad", "JobSearch");
            }

            ViewBag.Job = job;

            if (resume == null || resume.ContentLength == 0)
            {
                ViewBag.Message = "Please upload your resume.";
                return View("ApplyJobPageLoad");
            }

            string extension = Path.GetExtension(resume.FileName);

            if (extension == null ||
                extension.ToLower() != ".pdf")
            {
                ViewBag.Message = "Please upload a PDF file only.";
                return View("ApplyJobPageLoad");
            }

            var existing = dbobj.JobApplicationTabs.FirstOrDefault(
                x => x.EmpId == empid && x.JobId == id);

            if (existing != null)
            {
                ViewBag.Message = "You have already applied for this job.";
                return View("ApplyJobPageLoad");
            }

            string folderPath = Server.MapPath("~/Resumes");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                resume.SaveAs(filePath);

                JobApplicationTab app = new JobApplicationTab();

                app.EmpId = empid;
                app.JobId = id;
                app.ApplicationDate = DateTime.Today;
                app.Resume = "~/Resumes/" + fileName;
                app.ApplicationStatus = "Pending";

                dbobj.JobApplicationTabs.Add(app);
                dbobj.SaveChanges();

                // Show success message on the application page
                TempData["SuccessMessage"] =
                    "Applied Successfully! Your application has been submitted.";

                return RedirectToAction(
                    "ApplyJobPageLoad",
                    new { id = id });
            }
            catch
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                ViewBag.Message =
                    "Unable to submit your application. Please try again.";

                return View("ApplyJobPageLoad");
            }
        }
    }
}
