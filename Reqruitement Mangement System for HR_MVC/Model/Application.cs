using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Application
    {
        [Key]
        [Display(Name = "Application Id")]
        public int appid { get; set; }

        [Display(Name = "Candidate Id")]
        public int cid { get; set; }

        [Display(Name ="Education Id")]
        public int eid { get; set; }

        [Display(Name = "Job Id")]
        public int vid { get; set; }

        [Display(Name ="Status")]
        public string status { get; set; }  
    }
}
