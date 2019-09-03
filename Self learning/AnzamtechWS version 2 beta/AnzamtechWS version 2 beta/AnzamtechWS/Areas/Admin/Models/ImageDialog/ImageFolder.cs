using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models.ImageDialog
{
    public class ImageFolder
    {
        public ImageFolder(string nameGalery, string path)
        {
            FolderName = nameGalery;
            Path = path;
            string fullpath = HttpContext.Current.Server.MapPath(path);
            if (Directory.GetFiles(fullpath).Length > 0 || Directory.GetDirectories(fullpath).Length > 0)
            {
                PathImageFolder = "/image/folderi.png";
            }
            else
            {
                PathImageFolder = "/image/foder.png";
            }
        }

        public string PathImageFolder { set; get; }
        public string FolderName { set; get; }
        public string Path { set; get; }
    }
}