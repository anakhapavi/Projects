using RecuirementManagement.Repository;
using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecuirementManagement.Controllers
{
    public class CandidateRegistrationController : Controller
    {
        /// <summary>
        /// GET: CandidateRegistration
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCandidateDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCandidateDetails(CandidateRegistration canidateRegistration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CanidateRepositorycs RegRepos = new CanidateRepositorycs();
                    if (RegRepos.AddCandidateDetails(canidateRegistration))
                    {
                        ViewBag.Message = "Deails added sucessfully";
                    }
                }
                return RedirectToAction("GetCandidateDetails");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        ///get details
        /// </summary>
        public ActionResult GetCandidateDetails()
        {
            CanidateRepositorycs RegRepos = new CanidateRepositorycs();
            ModelState.Clear();
            return View(RegRepos.GetCandidateDetails());
        }

    }
}
