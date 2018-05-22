using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SparshWeb.Models;

namespace SparshWeb.ModelBusiness
{
    public class GalleryImageBusiness
    {
        static string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~\\GalleryImg");

        public static List<GalleryImageViewModel> ImageViewList()
        {
            List<GalleryImageViewModel> imgList = new List<GalleryImageViewModel>();

            if (Directory.Exists(xmlPath))
            {
                DirectoryInfo di = new DirectoryInfo(xmlPath);
                FileInfo[] smFiles = di.GetFiles("*.jpg");

                foreach (var item in smFiles)
                {
                    GalleryImageViewModel model = new GalleryImageViewModel();
                    model.ImgID = Convert.ToInt32(Path.GetFileNameWithoutExtension(item.FullName));
                    model.ImageName = Path.GetFileName(item.FullName);
                    model.ImageDesc = Path.GetFileName(item.FullName);
                    model.ImagePath = item.FullName;
                    imgList.Add(model);
                }
            }
            return imgList;
        }
    }
}