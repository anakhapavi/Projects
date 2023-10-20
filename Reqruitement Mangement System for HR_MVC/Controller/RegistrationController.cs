using RecuirementManagement.Models;
using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RecuirementManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly RegistrationRepository RegRepos;

        public RegistrationController()
        {
            RegRepos = new RegistrationRepository();
        }

        /// <summary>
        /// GET: CandidateRegistration
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInformation()
        {
            ModelState.Clear();
            return View(RegRepos.GetInformation());
        }

        public ActionResult AddInformation()
        {
            Registration registration = new Registration();
            PopulateStates();
            PopulateCities();
            return View(registration);
        }

        [HttpPost]
        public ActionResult AddInformation(Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    registration.usertype = "candidate";


                    if (RegRepos.AddInformation(registration))
                    {
                        int cid = GetGeneratedCid();
                        ViewBag.Message = "Details Added Successfully";

                        if (registration.usertype == "candidate")
                        {
                            Login login = new Login
                            {
                                username = registration.username,
                                password = registration.password,
                                usertype = registration.usertype
                            };
                            LoginRepository loginRepos = new LoginRepository();
                            if (loginRepos.AddLogin(login))
                            {
                                ViewBag.Message = "Success";
                            }
                            else
                            {
                                ViewBag.Message = "Failed";
                            }
                        }

                        string redirectUrl = Url.Action("AddEducation", "Education", new { cid = cid });
                        return Redirect(redirectUrl);
                    }
                    else
                    {
                        ViewBag.Message = "Failed to add details to the database.";
                    }
                }

                PopulateStates();
                PopulateCities();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View();
            }
        }

        private void PopulateStates()
        {
            var states = new List<string>
            {
                "AndhraPradesh", "Goa", "Gujarat", "Kerala", "Tamil Nadu", "Karnataka", "Others"
            };
            ViewBag.States = new SelectList(states);
        }

        private void PopulateCities()
        {
            var cities = new List<string>
            {
                "Vijaywada", "Vishakapattanam", "Tirupati", "Panaji", "Ponda", "Vasco",
                "Ahmedabad", "Junagadh", "Porbandar", "Thiruvananthapuram", "Kochi",
                "Kottayam", "Thrissur", "Kozhikode", "Chennai", "Madurai", "Bangalore",
                "Tumkur", "Udupi", "Others"
            };
            ViewBag.Cities = new SelectList(cities);
        }

        private int GetGeneratedCid()
        {
            int generateCid = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT SCOPE_IDENTITY() AS GeneratedCid", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0)) 
                        {
                            generateCid = Convert.ToInt32(reader["GeneratedCid"]);
                        }
                    }
                }
            }
            return generateCid;
        }
        //public ActionResult EditInformation(int? cid)
        //{
        //    RegistrationRepository RegRepos = new RegistrationRepository();
        //    return View(RegRepos.GetInformation().Find(registration => registration.cid == cid)); ;
        //}
        //[HttpPost]
        //public ActionResult EditInformation(int? cid, Registration registration)
        //{
        //    try
        //    {
        //        RegistrationRepository RegRepos = new RegistrationRepository();
        //        RegRepos.EditInformation(registration);
        //        return RedirectToAction("GetInformation");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public ActionResult EditInfoByCandidate(int? cid)
        {
            RegistrationRepository RegRepos = new RegistrationRepository();

            if (cid.HasValue)
            {
                Registration candidateDetails = RegRepos.GetCandidateDetailsForUpdate(cid.Value);

                if (candidateDetails != null)
                {
                    return View(candidateDetails);
                }
            }
            return RedirectToAction("CandidateDetails");
        }

        [HttpPost]
        public ActionResult EditInfoByCandidate(int? cid, Registration registration)
        {
            if (cid == null)
            {
                return RedirectToAction("CandidateDetails");
            }

                try
                {
                    RegistrationRepository RegRepos = new RegistrationRepository();
                    RegRepos.EditInformation(registration);
                    return RedirectToAction("CandidateDetails", new { cid = cid });
                }
                catch 
                {
                    return View();
                }
        }
        //public ActionResult Delete(int? cid)
        //{
        //    if (!cid.HasValue)
        //    {
        //        return HttpNotFound();
        //    }

        //    RegistrationRepository RegRepos = new RegistrationRepository();
        //    var registration = RegRepos.GetInformation().FirstOrDefault(r => r.cid == cid);
        //    if (registration == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(registration);
        //}

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int? cid)
        //{
        //    if (!cid.HasValue)
        //    {
        //        return HttpNotFound();
        //    }

        //    RegistrationRepository RegRepos = new RegistrationRepository();

        //    if (RegRepos.DeleteInformation(cid.Value))
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return HttpNotFound();
        //}
        public ActionResult DeleteInformation(int cid, Registration registration)
        {
            try
            {
                RegistrationRepository RegRepos = new RegistrationRepository();
                if (RegRepos.DeleteInformation(cid))
                {
                    ViewBag.AlertMessage("Deleted");
                }
                return RedirectToAction("GetInformation");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CandidateDetails()
        {
            RegistrationRepository RegRepos = new RegistrationRepository();
            string username = (string)Session["Username"];
            List<Registration> candidateDetails = RegRepos.GetCandidateDetailsByUsername(username);
            return View(candidateDetails);
        }
    }
}
