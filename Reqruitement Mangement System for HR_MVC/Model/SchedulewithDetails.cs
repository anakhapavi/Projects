using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class ScheduleWithDetails
    {
        [Display(Name = "ScheduleId")]
        public int sid { get; set; }

        [Display(Name = "CandidateId")]
        public int cid { get; set; }

        [Display(Name = "VaccationId")]
        public int vid { get; set; }

        [Display(Name = "ApplicationId")]
        public int appid { get; set; }

        [Display(Name = "Schedule Date")]
        public DateTime scheduleDate { get; set; }

        [Display(Name = "Schedule Time")]
        public TimeSpan scheduleTime { get; set; }

        [Display(Name = "Organizer")]
        public string organizer { get; set; }

        [Display(Name = "Link")]
        public string link { get; set; }

        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Display(Name = "JobRole")]
        public string jobRole { get; set; }
    }
}
