using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RecuirementManagement.Models;
using Login = RecuirementManagement.Models.Login;
using System.Diagnostics;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace RecuirementManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository loginRepository;
        public LoginController()
        {
            loginRepository = new LoginRepository();
        }

        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Logindetails()
        {
            return View();
        }

        // POST: Login
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Logindetails(string username, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Call the repository to verify user credentials
        //        string userType = loginRepository.VerifyUserCredentials(username, password);

        //        if (userType != "invalid")
        //        {
        //            if (userType == "hr")
        //            {
        //                // Redirect to the HR dashboard
        //                return RedirectToAction("Index", "HR");
        //            }
        //            else if (userType == "candidate")
        //            {
        //                // Redirect to the candidate dashboard
        //                return RedirectToAction("Index", "Candidate");
        //            }
        //        }

        //        // Invalid credentials; show an error message
        //        ModelState.AddModelError("", "Invalid username or password");
        //    }

        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logindetails(Login login)
        {
            if (ModelState.IsValid)
            {
                string usertype = loginRepository.AuthenticateUser(login.username, login.password);
                if (usertype != null) 
                {
                   
                    Session["username"] = login.username;
                    Session["password"] = login.password;
                   
                    if (usertype == "hr")
                    {
                        return RedirectToAction("Index", "Hr");
                    }
                    else if (usertype == "candidate")
                    {
                        return RedirectToAction("Index","Candidate");
                    }
                }
                ModelState.AddModelError("", "Invalid username and password");
            }
            return View(login);
        }

       
       




    }
}

