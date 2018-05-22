using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparshWeb.Models
{
    public class GalleryImageViewModel
    {
        public int ImgID { get; set; }
        public string ImageName { get; set; }
        public string ImageDesc { get; set; }
        public string ImagePath { get; set; }
    }
}