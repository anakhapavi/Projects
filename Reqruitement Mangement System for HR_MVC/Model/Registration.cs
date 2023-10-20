using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Registration
    {
        [Key]
        public int cid { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "First letter in caps")]
        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "First letter in caps")]
        [Display(Name = "Last name")]
        public string lastName { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DefaultValue("1900-01-01")] 
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should contain exactly 10 digits")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address.")]
        public string email { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public string address { get; set; }


        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }

        public string usertype { get; set; }

        

    }


}
