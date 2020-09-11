using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhoTimSolution.Areas.Admin.Models;
using NhoTimSolution.NhoTimModel;
namespace NhoTimSolution.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        // GET: Admin/Upload
        [HttpGet]
        [Area("Admin")]
        public async Task<JsonResult> GetFolders(int id)
        {
            FileManager fm = new FileManager(id);
            await Task.WhenAll(fm.GetFolders(), fm.GetFiles(), fm.GetFolderInfor());
            return Json(fm.Folder);
        }
        
        [HttpPost]
        [Area("Admin")]
        public async Task<JsonResult> UploadFileAsync()
        {

            if (Request.Form.Files.Count > 0)
            {
                IFormFile files = Request.Form.Files[0];
                int FolderID = int.Parse(Request.Form["ID"].ToString());
                string path = Request.Form["Path"].ToString();
                string name = Request.Form["FileName"].ToString();
                string ext = Request.Form["FileType"].ToString();
                name = Helper.ProccessString.RemoveSign4VietnameseString(name).Replace(' ', '-');
                using (var db = new NhotimContext())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            Files f = new Files
                            {
                                FolderID = FolderID,
                                Name = name,
                                Path = path,
                                TypeFile = ext
                            };
                            db.Files.Add(f);
                            await db.SaveChangesAsync();
                            f.Path += "/" + f.FileID + "." + ext;
                            await Task.WhenAll(db.SaveChangesAsync());
                            string s = Environment.CurrentDirectory + "\\wwwroot" + path.Replace("/", "\\") + "\\" + f.FileID + "." + ext;
                            using (var stream = new FileStream(s, FileMode.Create))
                            {
                                await files.CopyToAsync(stream);
                            }
                            trans.Commit();
                            return Json("OK");
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return Json("Error");
                        }
                    }

                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

    }

}