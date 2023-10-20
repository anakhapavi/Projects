using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class ApplicationDetails
    {
        [Display(Name ="Application Id")]
        public int appid { get; set; }

        [Display(Name = "Candidate Id")]
        public int cid { get; set; }

        [Display(Name = "Education Id")]
        public int eid { get; set; }

        [Display(Name = "Job Id")]
        public int vid { get; set; }

        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Display(Name = "Highest Qualification")]
        public string highestQualification { get; set; }

        [Display(Name = "Work Experience")]
        public string workExperience { get; set; }

        [Display(Name = "No.of Years")]
        public int noofYears { get; set; }

        [Display(Name = "Skills")]
        public string skills { get; set; }

        [Display(Name = "Job Role")]
        public string jobRole { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
        }   
}
