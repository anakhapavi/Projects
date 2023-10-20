using RecuirementManagement.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RecuirementManagement.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageRepository imageRepository;

        public ImageController()
        {
            imageRepository = new ImageRepository();
        }

        /// <summary>
        /// GET:Image
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }

                bool isUploaded = imageRepository.UploadImage(imageData);

                if (isUploaded)
                {
                    ViewBag.Message = "Image uploaded successfully.";
                    return RedirectToAction("UploadImage"); 
                }
                else
                {
                    ViewBag.ErrorMessage = "Image upload failed. Please try again.";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please select an image to upload.";
                return View();
            }
        }
        public ActionResult Display(int imgid)
        {
            byte[] imageData = imageRepository.GetImageDataById(imgid);

            if (imageData != null)
            {
                string contentType = "image/jpeg"; 
                if (imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8)
                {
                    contentType = "image/jpeg";
                }
                else if (imageData.Length >= 4 && imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
                {
                    contentType = "image/png";
                }
                return File(imageData, contentType);
            }
            else
            {
                return HttpNotFound();
            }
        }
        /// <summary>
        /// DisplayImage
        /// </summary>
        /// <returns></returns>
        public ActionResult DisplayAllImages()
        {
            List<byte[]> imageList = imageRepository.GetAllImagesData();

            if (imageList != null && imageList.Count > 0)
            {
                return View(imageList);
            }
            else
            {
                return HttpNotFound();
            }
        }


    }

}
