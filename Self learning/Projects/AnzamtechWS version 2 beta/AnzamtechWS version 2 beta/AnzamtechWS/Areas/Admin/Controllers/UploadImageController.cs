using AnzamtechWS.Areas.Admin.Models;
using AnzamtechWS.Areas.Admin.Models.ImageDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class UploadImageController : Controller
    {
        ANZEntities context = new ANZEntities();
        // GET: Admin/UploadImage
        [HttpPost]
        public JsonResult Index()
        {
            using (var trans = context.Database.BeginTransaction())
            {
                try
                {
                   
                    int MaDMA = int.Parse(Request.Headers["MaDMA"]);
                    int loaiAnh = int.Parse(Request.Headers["LoaiAnh"]);
                    string fileName = Request.Headers["TenAnh"];
                    string path = Request.Headers["Path"];
                    string tag = Request.Headers["Tag"];
                    string type = Request.Headers["File-type"];
                    var fileContent = Request.InputStream;
                    string tmp = fileName + "." + type;
                    AnhModel mAnh = new AnhModel(context)
                    {
                        MaDMA = MaDMA,
                        Path = path,
                        MaLA = loaiAnh,
                        TenAnh = tmp,
                        Tag = tag
                    };
                    mAnh.Add();
                    fileName =fileName+ "-" + mAnh.MaAnh + "." + type;
                    mAnh.TenAnh = fileName;
                    mAnh.UpdateFileName();
                    using (var fs = new FileStream(Server.MapPath(path) + "\\"+ fileName, FileMode.CreateNew, FileAccess.Write))
                    {
                        var buffer = new byte[1024];

                        var l = fileContent.Read(buffer, 0, 1024);
                        while (l > 0)
                        {
                            fs.Write(buffer, 0, l);
                            l = fileContent.Read(buffer, 0, 1024);
                        }
                        fs.Flush();
                        fs.Close();
                        //File's content is available in Request.InputStream property
                    }
                    trans.Commit();
                }catch(Exception ex){
                    Console.WriteLine(ex.ToString());
                    trans.Rollback();
                }
            }
           
            //Creating a FileStream to save file's content
         
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult XoaAnh(int MaAnh)
        {
            AnhModel model = new AnhModel()
            {
                MaAnh = MaAnh
            };
            int i = model.XoaAnh();
            if (i == -1)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Test()
        {
            return View("Index");
        }
        public JsonResult GetAlbum(int MaDMA)
        {
            DanhMucAnhModel m = new DanhMucAnhModel()
            {
                Id = MaDMA
            };
            DanhMucAnh obj = m.GetDanhMucAnhByID();
            ImageGallery imgGa = new ImageGallery(obj.TenDM, obj.Path + "/" + obj.TenDM);
            return Json(imgGa, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFolders(string name, string path)
        {
            ImageGallery imgGa = new ImageGallery(name, path);
            return Json(imgGa, JsonRequestBehavior.AllowGet);
        }


    }

   
}