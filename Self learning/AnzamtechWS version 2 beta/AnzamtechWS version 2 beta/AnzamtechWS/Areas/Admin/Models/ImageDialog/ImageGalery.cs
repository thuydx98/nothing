using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
/// <summary>
/// Summary description for ImageGalery
/// </summary>
/// 
namespace AnzamtechWS.Areas.Admin.Models.ImageDialog
{
    public class ImageGallery
    {
        public List<Image> Images { set; get; }
        public ImageFolder ParentFolder { set; get; }
        public List<ImageFolder> ImageGallerys { set; get; }                                                            

        public ImageGallery(string name, string path)
        {
            this.ImageGallerys = new List<ImageFolder>();
            this.Images = new List<Image>();
            this.ParentFolder = new ImageFolder(name, path);
            GetAll(path);
           
           
        }

        private void GetAll(string path)
        {
            string fullpath = HttpContext.Current.Server.MapPath(path);
            
            if (Directory.Exists(fullpath))
            {
                string[] name = Directory.GetDirectories(fullpath);
                for (int i = 0; i < name.Length; i++)
                {
                    DirectoryInfo di = new DirectoryInfo(name[i]);
                    ImageGallerys.Add(new ImageFolder(di.Name, path + "/" + di.Name));
                }
               
            }
            if (Directory.GetFiles(fullpath).Length > 0)
            {
                string[] files = Directory.GetFiles(fullpath);

                for (int j = 0; j < files.Length; j++)
                {
                    FileInfo fi = new FileInfo(files[j]);
                    Image img = new Image()
                    {
                        FileName = fi.Name,
                        Path = path + "/" + fi.Name
                    };
                    Images.Add(img);
                }
            }
        }

    
    }
}