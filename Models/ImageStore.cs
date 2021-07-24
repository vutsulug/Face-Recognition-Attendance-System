using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ImageStore
    {
        
            [Key]
            public int ImageId { get; set; }
            public string ImageBase64String { get; set; }
            public DateTime? CreateDate { get; set; }
    }
}