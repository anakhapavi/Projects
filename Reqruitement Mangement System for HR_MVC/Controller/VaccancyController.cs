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
    public class VaccancyController : Controller
    {
        public string connectionString;

        public VaccancyController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ConnectionString;
        }

        /// <summary>
        /// Add: Jobs
        /// </summary>
        /// <returns></returns>
        public ActionResult AddVaccancy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVaccancy(Vaccancy vaccancy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VaccancyRepository repository = new VaccancyRepository(connectionString);
                    if (repository.InsertVaccancy(vaccancy))
                    {
                        ViewBag.Message = "New job added successfully";
                    }
                }
                return RedirectToAction("GetVaccancies");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetVaccancies()
        {
            VaccancyRepository repository = new VaccancyRepository(connectionString);

            ModelState.Clear();
            if (User.Identity.IsAuthenticated)
            {
                var firstItem = repository.GetVaccancies().FirstOrDefault();
                if (firstItem != null)
                {
                    Session["vid"] = firstItem.vid;
                }
            }

            return View(repository.GetVaccancies());
        }    
    }

}

