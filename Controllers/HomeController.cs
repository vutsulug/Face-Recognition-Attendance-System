using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}


        //static async Task<string> MakeAnalysisRequest(byte[] imageBytes)
        //{ }
        //    [HttpPost]
        //public async Task<dynamic> Capture(string base64String)
        //{

        //    if (!string.IsNullOrEmpty(base64String))
        //    {
        //        var imageParts = base64String.Split(',').ToList<string>();
        //        byte[] imageBytes = Convert.FromBase64String(imageParts[1]);
        //        DateTime nm = DateTime.Now;
        //        string date = nm.ToString("yyyymmddMMss");
        //        var path = Server.MapPath("~/CapturedPhotos/" + date + "CamCapture.jpg");

        //        var response = await MakeAnalysisRequest(imageBytes);

        //        System.IO.File.WriteAllBytes(path, imageBytes);
        //        return Json(data: response);
        //    }
        //    else
        //    {
        //        return Json(data: false);
        //    }
        //}
    }
}