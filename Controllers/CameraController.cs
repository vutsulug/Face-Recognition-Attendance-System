using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CameraController : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();
       // private IHostingEnvironment hostingEnvironment = new IHostingEnvironment();
        //private readonly AttendanceDbContext _context;
        //private readonly IHostingEnvironment _environment;
        //public CameraController(AttendanceDbContext context)
        //{
        //    //_environment = hostingEnvironment;
        //    _context = context;
        //}

        [HttpGet]
        public ActionResult Capture()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Capture(string name)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // Getting Filename  
                        var fileName = file.FileName;
                        // Unique filename "Guid"  
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        // Getting Extension  
                        var fileExtension = Path.GetExtension(fileName);
                        // Concating filename + fileExtension (unique filename)  
                        var newFileName = string.Concat(myUniqueFileName, fileExtension);
                        //  Generating Path to store photo   
                        // var filepath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{newFileName}";

                        //if (!string.IsNullOrEmpty(filepath))
                        //{
                            // Storing Image in Folder  
                        //    StoreInFolder(file, filepath);
                      //  }

                        var imageBytes = System.IO.File.ReadAllBytes(fileName);
                        if (imageBytes != null)
                        {
                            // Storing Image in Folder  
                            StoreInDatabase(imageBytes);
                        }

                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        [HttpPost]
        public ContentResult SaveCapture(string data)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

            //Convert Base64 Encoded string to Byte Array.
            byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

            //Save the Byte Array as Image File.
            string filePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
            //  Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
            System.IO.File.WriteAllBytes(filePath, imageBytes);

            return Content("true");
        }

        /// <summary>  
        /// Saving captured image into Folder.  
        /// </summary>  
        /// <param name="file"></param>  
        /// <param name="fileName"></param>  
        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        /// <summary>  
        /// Saving captured image into database.  
        /// </summary>  
        /// <param name="imageBytes"></param>  
        private void StoreInDatabase(byte[] imageBytes)
        {
            try
            {
                if (imageBytes != null)
                {
                    string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                    ImageStore imageStore = new ImageStore()
                    {
                        CreateDate = DateTime.Now,
                        ImageBase64String = imageUrl,
                        ImageId = 0
                    };

                    db.ImageStore.Add(imageStore);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}