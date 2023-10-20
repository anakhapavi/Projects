using RecuirementManagement.Models;
using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecuirementManagement.Controllers
{
    public class ScheduleController : Controller
    {
        public string connectionString;
        public ScheduleController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ConnectionString;
        }
        /// <summary>
        /// GET: Add Schedule
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSchedule()
        {
            ApplicationRepository repository = new ApplicationRepository();
            List<ApplicationDetails> application = repository.GetScheduledApplications();
            SelectList applicationList = new SelectList(application, "cid", "firstName");
            ViewBag.ApplicationList = applicationList;
            Schedule schedule = new Schedule();
            return View(schedule);
        }

        [HttpPost]
        public ActionResult AddSchedule(Schedule schedule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int selectedCid = schedule.cid;
                    ScheduleRepository SchduleRepos = new ScheduleRepository();
                    if (SchduleRepos.AddSchedule(schedule))
                    {
                        ViewBag.Message = "Scheduled Sucessfully";
                    }
                }
                return RedirectToAction("Index","Hr");
            }
            catch 
            {
                return View();
            }
        }
        /// <summary>
        /// GET:View  Schedules
        /// </summary>
        /// <returns></returns>
        //view by candidate
        //public ActionResult ScheduleDetails()
        //{
        //    string username = (string)Session["username"];
        //    RegistrationRepository RegRepos = new RegistrationRepository();
        //    ScheduleRepository ScheduleRepos = new ScheduleRepository();
        //    List<Schedule> scheduleDetails = new List<Schedule>();
        //    List<Registration> userDetails = RegRepos.GetCandidateDetailsByUsername(username);
        //    if (userDetails.Count > 0)
        //    {
        //        int cid = userDetails[0].cid; 
        //        scheduleDetails = ScheduleRepos.GetSchedules(cid);
        //        ViewBag.UserDetails = userDetails;
        //    }
        //    else
        //    {
        //        // Handle the case where user details were not found
        //    }

        //    return View(scheduleDetails);
        //}
        public ActionResult ScheduleDetailUser()
        {
            string username = (string)Session["username"];
            RegistrationRepository RegRepos = new RegistrationRepository();
            ScheduleRepository ScheduleRepos = new ScheduleRepository();
            List<ScheduleWithDetails> scheduleDetails = new List<ScheduleWithDetails>(); 
            List<Registration> userDetails = RegRepos.GetCandidateDetailsByUsername(username);
            if (userDetails.Count > 0)
            {
                int cid = userDetails[0].cid;
                scheduleDetails = ScheduleRepos.GetSchedulesWithDetails(cid); 
                ViewBag.UserDetails = userDetails;
            }
            else
            {
                //ViewBag.Message="error";

            }

            return View(scheduleDetails); 
        }




    }
}
