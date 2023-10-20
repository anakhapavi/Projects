using RecuirementManagement.Models;
using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace RecuirementManagement.Controllers
{
    public class ApplicationController : Controller
    {
        public string connectionString;
        public ApplicationController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ConnectionString;
        }
        /// <summary>
        /// ADD: Application
        /// </summary>
        /// <returns></returns>
        public ActionResult AddApplications()
        {
            VaccancyRepository repository = new VaccancyRepository(connectionString);

            List<Vaccancy> vacancies = repository.GetVaccancies();
            SelectList vacancyList = new SelectList(vacancies, "Vid", "JobRole");

            ViewBag.VacancyList = vacancyList;

            Application application = new Application();

            return View(application); 
        }
        [HttpPost]
        public ActionResult AddApplications(Application application)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    application.status = "Applied";

                    int selectedVid = application.vid;
                    ApplicationRepository APPRegPos = new ApplicationRepository();
                    if (APPRegPos.AddApplications(application))
                    {
                        ViewBag.Message = "Details added";
                    }
                }
                return RedirectToAction("Index","Candidate");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// VIEW: Application
        /// </summary>
        /// <returns></returns>
        //public ActionResult GetApplications()
        //{ 
        //    ApplicationRepository AppRegPos = new ApplicationRepository();
        //    ModelState.Clear();
        //    return View(AppRegPos.GetApplications());
        //}
        public ActionResult GetAllApplicationsDetails()
        {
            ApplicationRepository AppRegPos = new ApplicationRepository();
            var allApplicationsDetails = AppRegPos.GetAllApplicationsDetails();

            return View(allApplicationsDetails);
        }

        /// <summary>
        /// EDIT: Application
        /// </summary>
        /// <returns></returns>
        // GET: EditApplicationDetails
        public ActionResult EditApplicationDetails(int? appid)
        {
            if (appid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationRepository APPRegPos = new ApplicationRepository();
            Application applicationModel = APPRegPos.GetApplicationById(appid);

            if (applicationModel == null)
            {
                return HttpNotFound();
            }
            applicationModel.status = "-Select-";


            return View(applicationModel);
        }

        /// <summary>
        /// POST: EditApplicationDetails
        /// </summary>
        /// <param name="updatedApplication"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditApplicationDetails(Application updatedApplication)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationRepository AppRegPos = new ApplicationRepository();
                    AppRegPos.EditApplications(updatedApplication);
                    return RedirectToAction("GetAllApplicationsDetails");
                }
            }
            catch
            {
                // ViewBag.ErrorMessage = "An error occurred: ";
            }

            return View(updatedApplication);
        }



    }
}
