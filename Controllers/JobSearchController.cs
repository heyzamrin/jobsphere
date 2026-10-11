using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using MVCProjectOne.Models;
using System.Linq;

namespace MVCProjectOne.Controllers
{
    public class JobSearchController : Controller
    {
        MVCProject1Entities dbobj = new MVCProject1Entities();

        public ActionResult EmployeePageLoad()
        {
            return View("JobSearchPageLoad", GetJobList());
        }

        private JobSearchClass GetJobList()
        {
            var joblists = new JobSearchClass();

            var jobs = dbobj.CompanyJobDetailsTabs.ToList();

            foreach (var j in jobs)
            {
                var jobobj = new CompanyJobDetailsTab();

                jobobj.JobId = j.JobId;
                jobobj.CompanyId = j.CompanyId;
                jobobj.JobTitle = j.JobTitle;
                jobobj.Experience = j.Experience;
                jobobj.Skill = j.Skill;
                jobobj.Location = j.Location;
                jobobj.Qualification = j.Qualification;
                jobobj.Salary = j.Salary;
                jobobj.PostDate = j.PostDate;
                jobobj.EndDate = j.EndDate;
                jobobj.JobStatus = j.JobStatus;

                joblists.SelectJob.Add(jobobj);
            }

            return joblists;
        }

        public ActionResult JobSearchClick(JobSearchClass clsobj)
        {
            string qry = "";

            if (!string.IsNullOrWhiteSpace(clsobj.JobTitle))
            {
                qry += " and JobTitle like '%" + clsobj.JobTitle + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.JobExperience))
            {
                qry += " and Experience like '%" + clsobj.JobExperience + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.JobSkills))
            {
                qry += " and Skill like '%" + clsobj.JobSkills + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.JobLocation))
            {
                qry += " and Location like '%" + clsobj.JobLocation + "%'";
            }

            return View("JobSearchPageLoad", getdata(qry));
        }

        private JobSearchClass getdata(string qry)
        {
            var joblists = new JobSearchClass();

            string con = ConfigurationManager
                .ConnectionStrings["importdataconnection"].ConnectionString;

            using (SqlConnection conobj = new SqlConnection(con))
            {
                SqlCommand cmdobj = new SqlCommand("spJobSearch", conobj);

                cmdobj.CommandType =
                    System.Data.CommandType.StoredProcedure;

                cmdobj.Parameters.AddWithValue("@qry", qry);

                conobj.Open();

                SqlDataReader dr = cmdobj.ExecuteReader();

                while (dr.Read())
                {
                    CompanyJobDetailsTab job =
                        new CompanyJobDetailsTab();

                    job.JobId = Convert.ToInt32(dr["JobId"]);
                    job.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                    job.JobTitle = dr["JobTitle"].ToString();
                    job.Experience = dr["Experience"].ToString();
                    job.Skill = dr["Skill"].ToString();
                    job.Location = dr["Location"].ToString();
                    job.Qualification = dr["Qualification"].ToString();
                    job.Salary = dr["Salary"].ToString();
                    job.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    job.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    job.JobStatus = dr["JobStatus"].ToString();

                    joblists.SelectJob.Add(job);
                }

                dr.Close();
            }

            return joblists;
        }
    }
}




//public ActionResult JobSearchPageLoad()
//{
//    var data = dbobj.spJobDisplay().ToList();

//    ViewBag.JobDetails = data;

//    return View();
//}

//public ActionResult EmployeeHomeClick()
//{
//    var data = dbobj.spJobDisplay().ToList();

//    ViewBag.JobDetails = data;

//    return View();
//}
