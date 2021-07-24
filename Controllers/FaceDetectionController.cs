using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    public class FaceDetectionController : Controller
    {
        private static string ServiceKey = ConfigurationManager.AppSettings["FaceServiceKey"];
        // GET: FaceDetection


        private static string directory = "/Captures";
        private static string UplImageName = string.Empty;

        private readonly ObservableCollection<Face> _detectedFaces = new ObservableCollection<Face>();
        private readonly ObservableCollection<Face> _resultCollection = new ObservableCollection<Face>();
        public ObservableCollection<Face> DetectedFaces
        {
            get
            {
                return _detectedFaces;
            }
        }
        public ObservableCollection<Face> ResultCollection
        {
            get
            {
                return _resultCollection;
            }
        }
        public int MaxImageSize
        {
            get
            {
                return 450;
            }
        }

        // GET: FaceDetection  
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCandidateFiles()
        {
            string message = string.Empty, fileName = string.Empty, actualFileName = string.Empty; bool flag = false;
            //Requested File Collection  
            HttpFileCollection fileRequested = System.Web.HttpContext.Current.Request.Files;
            if (fileRequested != null)
            {
                //Create New Folder  
                CreateDirectory();

                //Clear Existing File in Folder  
                ClearDirectory();

                for (int i = 0; i < fileRequested.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName = file.FileName;
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    int size = file.ContentLength;

                    try
                    {
                        file.SaveAs(Path.Combine(Server.MapPath(directory), fileName));
                        message = "File uploaded successfully";
                        UplImageName = fileName;
                        flag = true;
                    }
                    catch (Exception)
                    {
                        message = "File upload failed! Please try again";
                    }
                }
            }
            return new JsonResult
            {
                Data = new
                {
                    Message = message,
                    UplImageName = fileName,
                    Status = flag
                }
            };
        }

        [HttpGet]
        public async Task<dynamic> GetDetectedFaces()
        {
            ResultCollection.Clear();
            DetectedFaces.Clear();

            var DetectedResultsInText = string.Format("Detecting...");
            var FullImgPath = Server.MapPath(directory) + '/' + UplImageName as string;
            var QueryFaceImageUrl = directory + '/' + UplImageName;

            if (UplImageName != "")
            {
                //Create New Folder  
                CreateDirectory();

                try
                {
                    // Call detection REST API  
                    using (var fStream = System.IO.File.OpenRead(FullImgPath))
                    {
                        // User picked one image  
                        var imageInfo = UIHelper.GetImageInfoForRendering(FullImgPath);

                        // Create Instance of Service Client by passing Servicekey as parameter in constructor   
                        var faceServiceClient = new FaceServiceClient(ServiceKey);
                        Face[] faces = await faceServiceClient.DetectAsync(fStream, true, true, new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses });
                        DetectedResultsInText = string.Format("{0} face(s) has been detected!!", faces.Length);
                        Bitmap CroppedFace = null;

                        foreach (var face in faces)
                        {
                            //Create & Save Cropped Images  
                            var croppedImg = Convert.ToString(Guid.NewGuid()) + ".jpeg" as string;
                            var croppedImgPath = directory + '/' + croppedImg as string;
                            var croppedImgFullPath = Server.MapPath(directory) + '/' + croppedImg as string;
                            CroppedFace = CropBitmap(
                                            (Bitmap)Image.FromFile(FullImgPath),
                                            face.FaceRectangle.Left,
                                            face.FaceRectangle.Top,
                                            face.FaceRectangle.Width,
                                            face.FaceRectangle.Height);
                            CroppedFace.Save(croppedImgFullPath, ImageFormat.Jpeg);
                            if (CroppedFace != null)
                                ((IDisposable)CroppedFace).Dispose();


                            DetectedFaces.Add(new vmFace()
                            {
                                ImagePath = FullImgPath,
                                FileName = croppedImg,
                                FilePath = croppedImgPath,
                                Left = face.FaceRectangle.Left,
                                Top = face.FaceRectangle.Top,
                                Width = face.FaceRectangle.Width,
                                Height = face.FaceRectangle.Height,
                                FaceId = face.FaceId.ToString(),
                                Gender = face.FaceAttributes.Gender,
                                Age = string.Format("{0:#} years old", face.FaceAttributes.Age),
                                IsSmiling = face.FaceAttributes.Smile > 0.0 ? "Smile" : "Not Smile",
                                Glasses = face.FaceAttributes.Glasses.ToString(),
                            });
                        }

                        // Convert detection result into UI binding object for rendering  
                        var rectFaces = UIHelper.CalculateFaceRectangleForRendering(faces, MaxImageSize, imageInfo);
                        foreach (var face in rectFaces)
                        {
                            ResultCollection.Add(face);
                        }
                    }
                }
                catch (FaceAPIException)
                {
                    //do exception work  
                }
            }
            return new JsonResult
            {
                Data = new
                {
                    QueryFaceImage = QueryFaceImageUrl,
                    MaxImageSize = MaxImageSize,
                    FaceInfo = DetectedFaces,
                    FaceRectangles = ResultCollection,
                    DetectedResults = DetectedResultsInText
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public void CreateDirectory()
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath(directory));
            if (!exists)
            {
                try
                {
                    Directory.CreateDirectory(Server.MapPath(directory));
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }

        public void ClearDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(Server.MapPath(directory)));
            var files = dir.GetFiles();
            if (files.Length > 0)
            {
                try
                {
                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
    }
    //public ActionResult Index()
    //    {
    //        if (Request.HttpMethod == "POST")
    //        {
    //            ViewBag.ImageProcessed = true;
    //            // Try to process the image.  
    //            if (Request.Files.Count > 0)
    //            {
    //                // There will be just one file.  
    //                var file = Request.Files[0];
    //                var fileName = Guid.NewGuid().ToString() + ".jpg";
    //                file.SaveAs(Server.MapPath("~/Captures/" + fileName));
    //                // Load the saved image, for native processing using Emgu CV.  
    //                var bitmap = new Bitmap(Server.MapPath("~/Captures/" + fileName));
    //                var faces = FaceDetector.DetectFaces(new Image<Bgr, byte>(bitmap).Mat);
    //                // If faces where found.  
    //                if (faces.Count > 0)
    //                {
    //                    ViewBag.FacesDetected = true;
    //                    ViewBag.FaceCount = faces.Count;
    //                    var positions = new List<Location>();
    //                    foreach (var face in faces)
    //                    {
    //                        // Add the positions.  
    //                        positions.Add(new Location
    //                        {
    //                            X = face.X,
    //                            Y = face.Y,
    //                            Width = face.Width,
    //                            Height = face.Height
    //                        });
    //                    }
    //                    ViewBag.FacePositions = JsonConvert.SerializeObject(positions);
    //                }
    //                ViewBag.ImageUrl = fileName;
    //            }
    //        }
    //        return View();
    //    }
    //}
}