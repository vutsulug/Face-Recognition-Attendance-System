

using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace WebApplication1.Helpers
{
    public class FaceDetectionHelper
    {


    }

    public class Location
    {
        public double X
        {
            get;
            set;
        }
        public double Y
        {
            get;
            set;
        }
        public double Width
        {
            get;
            set;
        }
        public double Height
        {
            get;
            set;
        }
    }

    // Face detector helper object  
    //public class FaceDetector
    //{
    //    public static List<Rectangle> DetectFaces(Mat image)
    //    {
    //        List<Rectangle> faces = new List<Rectangle>();
    //        var facesCascade = HttpContext.Current.Server.MapPath("~/haarcascade_frontalface_default.xml");
    //        using (CascadeClassifier face = new CascadeClassifier(facesCascade))
    //        {
    //            using (UMat ugray = new UMat())
    //            {
    //                CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
    //                //normalizes brightness and increases contrast of the image  
    //                CvInvoke.EqualizeHist(ugray, ugray);
    //                //Detect the faces from the gray scale image and store the locations as rectangle  
    //                //The first dimensional is the channel  
    //                //The second dimension is the index of the rectangle in the specific channel  
    //                Rectangle[] facesDetected = face.DetectMultiScale(ugray, 1.1, 10, new Size(20, 20));
    //                faces.AddRange(facesDetected);
    //            }
    //        }
    //        return faces;
    //    }
    //}

    /// <summary>  
    /// UI helper functions  
    /// </summary>  
    internal static class UIHelper
    {
        #region Methods  

        /// <summary>  
        /// Calculate the rendering face rectangle  
        /// </summary>  
        /// <param name="faces">Detected face from service</param>  
        /// <param name="maxSize">Image rendering size</param>  
        /// <param name="imageInfo">Image width and height</param>  
        /// <returns>Face structure for rendering</returns>  
        public static IEnumerable<vmFace> CalculateFaceRectangleForRendering(IEnumerable<Microsoft.ProjectOxford.Face.Contract.Face> faces, int maxSize, Tuple<int, int> imageInfo)
        {
            var imageWidth = imageInfo.Item1;
            var imageHeight = imageInfo.Item2;
            float ratio = (float)imageWidth / imageHeight;

            int uiWidth = 0;
            int uiHeight = 0;

            uiWidth = maxSize;
            uiHeight = (int)(maxSize / ratio);

            float scale = (float)uiWidth / imageWidth;

            foreach (var face in faces)
            {
                yield return new vmFace()
                {
                    FaceId = face.FaceId.ToString(),
                    Left = (int)(face.FaceRectangle.Left * scale),
                    Top = (int)(face.FaceRectangle.Top * scale),
                    Height = (int)(face.FaceRectangle.Height * scale),
                    Width = (int)(face.FaceRectangle.Width * scale),
                };
            }
        }

        /// <summary>  
        /// Get image basic information for further rendering usage  
        /// </summary>  
        /// <param name="imageFilePath">Path to the image file</param>  
        /// <returns>Image width and height</returns>  
        public static Tuple<int, int> GetImageInfoForRendering(string imageFilePath)
        {
            try
            {
                using (var s = File.OpenRead(imageFilePath))
                {
                    JpegBitmapDecoder decoder = new JpegBitmapDecoder(s, BitmapCreateOptions.None, BitmapCacheOption.None);
                    var frame = decoder.Frames.First();

                    // Store image width and height for following rendering  
                    return new Tuple<int, int>(frame.PixelWidth, frame.PixelHeight);
                }
            }
            catch
            {
                return new Tuple<int, int>(0, 0);
            }
        }
        #endregion Methods  
    }

    internal class vmFace : Face
    {
        public string FaceId { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImagePath { get; set; }
        public string FilePath { get; set; }
        public string  Gender { get; set; }
        public string Age { get; set; }
        public string IsSmiling { get; set; }
        public string Glasses { get; set; }
        public string FileName { get; set; }

    }
}