using RecuirementManagement.Models;
using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace RecuirementManagement.Controllers
{
    public class EducationController : Controller
    {
        /// <summary>
        /// GET: Education
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEducation(int? cid)
        {
                ViewBag.QualificationOptions = GetQualificationOptions();
                ViewBag.WorkExperienceOptions = GetWorkExperienceOptions();
                return View();        
        }

        [HttpPost]
        public ActionResult AddEducation(Education education,int? cid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    education.status = "pending";
                    EducationRepositorycs EduRepos = new EducationRepositorycs();
                    if (EduRepos.AddEducation(education))
                    {
                        ViewBag.Message = "Education details added";
                    }
                    else
                    {
                        ViewBag.Message = "Education details not added";
                    }
                }
                return RedirectToAction("Logindetails", "Login");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        ///view education details
        ////// </summary>
        public ActionResult GetEducation()
        {
            EducationRepositorycs EduRepos = new EducationRepositorycs();
            ModelState.Clear();
            return View(EduRepos.GetEducation());
        }

        /// <summary>
        ///add dropdown
        /// </summary>
        private IEnumerable<SelectListItem> GetQualificationOptions()
        {
           return new List<SelectListItem>
           {
                 new SelectListItem { Text = "--Select--", Value = "--Select--", Selected = true },
                 new SelectListItem { Text = "MCA", Value = "MCA" },
                 new SelectListItem { Text = "MSc", Value = "MSc" },
                 new SelectListItem { Text = "MTech", Value = "MTech" },
                 new SelectListItem { Text = "BTech", Value = "BTech" },
                 new SelectListItem { Text = "BCA", Value = "BCA" },
                 new SelectListItem { Text = "BSc", Value = "BSc" },
                 new SelectListItem { Text = "Diploma", Value = "Diploma" },
                 new SelectListItem { Text = "ITI", Value = "ITI" },
                 new SelectListItem { Text = "Others", Value = "Others" }
           };
        }
        /// <summary>
        ///dropdown for workexperience
        /// </summary>
        private IEnumerable<SelectListItem> GetWorkExperienceOptions()
        {
           return new List<SelectListItem>
           {
                 new SelectListItem { Text = "--Select--", Value = "--Select--", Selected = true },
                 new SelectListItem { Text = "Yes", Value = "Yes" },
                 new SelectListItem { Text = "No", Value = "No" }
           };
        }

        public ActionResult EditEducation(int? eid)
        {
            if (eid.HasValue)
            {
                EducationRepositorycs EduRepos = new EducationRepositorycs();
                var education = EduRepos.GetEducation().FirstOrDefault(e => e.eid == eid);

                if (education != null)
                {
                    return View(education);
                }
                else
                {
                    return HttpNotFound(); 
                }
            }
            else
            {
                return RedirectToAction("GetEducation"); 
            }
        }

        [HttpPost]
        public ActionResult EditEducation(int? eid, Education education)
        {
            try
            {
                EducationRepositorycs EduRepos = new EducationRepositorycs();
                EduRepos.EditEducation(education);
                return RedirectToAction("GetEducation");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// view by candidate
        /// </summary>
        /// <returns></returns>
        public ActionResult EducationDetails()
        {
            string username = (string)Session["username"];
            RegistrationRepository RegRepos = new RegistrationRepository();
            EducationRepositorycs EduRepos = new EducationRepositorycs();
            List<Education> educationDetails = new List<Education>();
            List<Registration> userDetails = RegRepos.GetCandidateDetailsByUsername(username);
            if (userDetails.Count > 0)
            {
                int cid = userDetails[0].cid; 
                educationDetails = EduRepos.GetEducationDetailsByCid(cid);
                ViewBag.UserDetails = userDetails;
            }
            else
            {
                //ViewBag.Message ="Error";
            }

            return View(educationDetails);
        }

    }
}
