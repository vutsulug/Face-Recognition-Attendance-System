using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Windows.Media.FaceAnalysis;

namespace WebApplication1.Controllers
{
    public class FaceDetectionController : Controller
    {
        // GET: FaceDetection
       public ActionResult Index() {  
    //if (Request.HttpMethod == "POST") {  
    //    ViewBag.ImageProcessed = true;  
    //    // Try to process the image.  
    //    if (Request.Files.Count > 0) {  
    //        // There will be just one file.  
    //        var file = Request.Files[0];  
    //        var fileName = Guid.NewGuid().ToString() + ".jpg";  
    //        file.SaveAs(Server.MapPath("~/Images/" + fileName));  
    //        // Load the saved image, for native processing using Emgu CV.  
    //        var bitmap = new Bitmap(Server.MapPath("~/Images/" + fileName));  
    //        var faces = FaceDetector.DetectFaces(new Image < Bgr, byte > (bitmap).Mat);  
    //        // If faces where found.  
    //        if (faces.Count > 0) {  
    //            ViewBag.FacesDetected = true;  
    //            ViewBag.FaceCount = faces.Count;  
    //            var positions = new List < Location > ();  
    //            foreach(var face in faces) {  
    //                // Add the positions.  
    //                positions.Add(new Location {  
    //                    X = face.X,  
    //                        Y = face.Y,  
    //                        Width = face.Width,  
    //                        Height = face.Height  
    //                });  
    //            }  
    //            ViewBag.FacePositions = JsonConvert.SerializeObject(positions);  
    //        }  
    //        ViewBag.ImageUrl = fileName;  
    //    }  
    //}  
    return View();  
}  
    }
}