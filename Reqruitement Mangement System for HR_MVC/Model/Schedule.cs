using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Schedule
    {
        [Key]
        [Display(Name = "Schedule Id")]
        public int sid { get; set; }

        [Display(Name = "Candiadte Id")]
        public int cid { get; set; }

        [Display(Name = "Vaccancy Id")]
        public int vid { get; set; }

        [Display(Name = "Application Id")]
        public int appid { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime scheduleDate { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public TimeSpan scheduleTime { get; set; }

        [Display(Name = "Organizer Name")]
        public string organizer { get; set; }

        [Display(Name = "Zoom Link")]
        public string link { get; set; }
    }
}
