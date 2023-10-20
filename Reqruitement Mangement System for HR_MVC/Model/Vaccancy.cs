using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Vaccancy
    {
        [Key]
        [Display(Name = "Vaccancy Id")]
        public int vid { get; set; }

        [Display(Name ="Job Role")]
        public string jobRole { get; set; }

        [Display(Name = "Skills")]
        public string skills { get; set; }

        [Display(Name = "Availability")]
        public string available { get; set; }

        [Display(Name = "Experience")]
        public string experience { get; set; }

        [Display(Name = "Status")]
        public string currentStatus { get; set; }
    }
}
