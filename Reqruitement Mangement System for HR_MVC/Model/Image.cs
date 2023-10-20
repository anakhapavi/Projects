using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Models
{
    public class Image
    {
       public int imgid { get; set; }

        [Display(Name = "Image")]
        [FileExtensions(Extensions = ".png,.jpg,.jpeg", ErrorMessage = "Only PNG and JPG (JPEG) files are allowed.")]
        public byte[] image { get; set; }
    }
}
