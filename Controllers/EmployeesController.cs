using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNet.Http;
using System.IO;
using VisioForge.Shared.Helpers;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();
      

        //public EmployeesController(IHostingEnvironment hostingEnvironment, AttendanceDbContext context)
        //{
        //    _environment = hostingEnvironment;
        //    _context = context;
        //}

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            return View(await db.Employees.Where(x => x.IsDeleted != true).ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create(string returnUrl = "")
        {
            var model = new EmployeeViewModel { ReturnUrl = returnUrl };

            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(employee.Image.Split(',')[1]);

                //Save the Byte Array as Image File.
                string filePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));

                //  Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
                System.IO.File.WriteAllBytes(filePath, imageBytes);

                try
                {
                    var item = new Employee();
                    item.EmailAddress = employee.EmailAddress;
                    item.EmployeeCode = employee.EmployeeCode;
                    item.FirstName = employee.FirstName;
                    item.Surname = employee.Surname;
                    item.DisplayName = employee.FirstName + " " + employee.Surname;
                    item.CellNumber = employee.CellNumber;
                    item.Password = employee.Password;
                    item.ConfirmPassword = employee.ConfirmPassword;
                    item.UserName = employee.EmailAddress;
                    if (imageBytes != null)
                    {
                        string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                        string imageUrl = string.Concat("data:image/jpg;base64,", base64String);
                        item.EmployeePhoto = imageUrl;

                        //ImageStore imageStore = new ImageStore()
                        //{
                        //    CreateDate = DateTime.Now,
                        //    ImageBase64String = imageUrl,
                        //    ImageId = 0
                        //};

                        //db.ImageStore.Add(imageStore);
                        //db.SaveChanges();

                        db.Employees.Add(item);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
                //return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,EmployeeCode,EmailAddress,FirstName,Surname,CellNumber,IsDeleted,DisplayName,EmployeePhoto")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                
                if (employee.EmployeePhoto != null)
                {
                    string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                    //Convert Base64 Encoded string to Byte Array.
                    byte[] imageBytes = Convert.FromBase64String(employee.EmployeePhoto.Split(',')[1]);

                    //Save the Byte Array as Image File.
                    string filePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));

                    //  Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
                    System.IO.File.WriteAllBytes(filePath, imageBytes);
                    string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);
                    employee.EmployeePhoto = imageUrl;

                    //ImageStore imageStore = new ImageStore()
                    //{
                    //    CreateDate = DateTime.Now,
                    //    ImageBase64String = imageUrl,
                    //    ImageId = 0
                    //};

                    //db.ImageStore.Add(imageStore);
                    //db.SaveChanges();

                    db.Entry(employee).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    db.Entry(employee).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
               
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id,[Bind(Include = "EmployeeID,EmployeeCode,EmailAddress,FirstName,Surname,CellNumber,IsDeleted,DisplayName")] Employee employee)
        {
            employee = await db.Employees.FindAsync(id);
            employee.IsDeleted =true;
            //db.Entry(employee).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

            return Content("true");
        }

        [HttpGet]
        public ActionResult Capture()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Capture(string name)
        {
            var files = HttpContext.Request.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    
                        // Getting Filename  
                        var fileName = file;
                        // Unique filename "Guid"  
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        // Getting Extension  
                        var fileExtension = Path.GetExtension(fileName.ToString());
                        // Concating filename + fileExtension (unique filename)  
                        var newFileName = string.Concat(myUniqueFileName, fileExtension);
                        //  Generating Path to store photo   
                        //var filepath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{newFileName}";

                        //if (!string.IsNullOrEmpty(filepath))
                        //{
                        //    // Storing Image in Folder  
                        //    StoreInFolder(file, filepath);
                        //}

                        var imageBytes = System.IO.File.ReadAllBytes(newFileName);
                        if (imageBytes != null)
                        {
                            // Storing Image in Folder  
                            StoreInDatabase(imageBytes);
                        }

                    }
                }
                return Json(true);
            //}
            //else
            //{
            //    return Json(false);
            //}

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
