using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Education
    {
        [Display(Name = "EducationId")]
        public int eid { get; set; }

        [Display(Name = "CandidateId")]
        public int cid { get; set; }

        [Required]
        [Display(Name = "Highest Qualification")]
        public string highestQualification { get; set; }

        [Required]
        [Display(Name = "Pass Year")]
        public int passYear { get; set; }

        [Required]
        [Display(Name = "CGPA")]
        public decimal cgpa { get; set; }

        [Required]
        [Display(Name = "Work Experience")]
        public string workExperience { get; set; }

        [Required]
        [Display(Name = "No.of Years")]
        public int noofYears{ get; set; }

        [Required]
        [Display(Name = "Job Role")]
        public string jobRole { get; set; }

        [Required]
        [Display(Name = "Last Salary")]
        public decimal lastSalary { get; set; }

        [Required]
        [Display(Name = "Skills")]
        public string skills { get; set; }       
        public string status { get; set; }
    }
}
