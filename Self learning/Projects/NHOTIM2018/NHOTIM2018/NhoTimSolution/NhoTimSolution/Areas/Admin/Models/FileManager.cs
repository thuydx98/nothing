using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhoTimSolution.NhoTimModel;
namespace NhoTimSolution.Areas.Admin.Models
{
    public class FileJson
    {
        public int FileID { set; get; }
        public int Type { set; get; }
        public string Name { set; get; }
        public string Tag { set; get; }
        public string Path { set; get; }
        public string TypeFile { set; get; }
        public string CSS { set; get; }
        public string Description { set; get; }
        public FileJson()
        {
            this.FileID = 0;
            this.Type = 0;
            this.Name = "";
            this.Tag = "";
            this.Path = "";
            this.TypeFile = "";
            this.CSS = "";
            this.Description = "";
        }
    }

    public class FolderJson
    {
        public int FolderID { set; get; }
        public List<FolderJson> SubFolder { set; get; }
        public List<FileJson> Files { set; get; }
        public string Name { set; get; }
        public string Path { set; get; }
        public int Status { set; get; }
        public int CountFile { set; get; }
        public FolderJson()
        {
            this.FolderID = 0;
            this.SubFolder = new List<FolderJson>();
            this.Files = new List<FileJson>();
            this.Name = "";
            this.Path = "";
            this.Status = 0;
        }

    }

    public class FileManager
    {
        private NhotimContext ent;
        public FolderJson Folder { set; get; }
        public FileManager(int FolderID)
        {
            Folder = new FolderJson();
            Folder.FolderID = FolderID;
        }
        
        public async Task GetFiles()
        {
            using (var ent = new NhotimContext())
            {
                Folder.Files = await ent.Files
                    .Where(u => u.FolderID == Folder.FolderID)
                    .Select(c => new FileJson
                    {
                        FileID = c.FileID,
                        Type = (c.Type == null) ? 0 : (int)c.Type,
                        CSS = c.CSS,
                        Description = c.Description,
                        Name = c.Name + "." + c.TypeFile,
                        Path = c.Path,
                        Tag = c.Tag,
                        TypeFile = c.TypeFile
                    }).ToListAsync();
            }
        }

        public async Task GetFolders()
        {
            using (var ent = new NhotimContext())
            {
                Folder.SubFolder = await ent.Folders
                    .Include(u => u.Files)
                    .Where(u => u.SubFolderID == Folder.FolderID)
                    .Select(u => new FolderJson { FolderID = u.FolderID, Name = u.Name, Path = u.Path, Status = u.Status, CountFile = u.Files.Count })
                    .ToListAsync();
            }
        }

        public async Task GetFolderInfor()
        {
            using (var ent = new NhotimContext())
            {
                var p =  await ent.Folders.FirstOrDefaultAsync(u => u.FolderID == this.Folder.FolderID);
                Folder.Name = p.Name;
                Folder.Path = p.Path;
                Folder.Status = p.Status;
            }
        }

    }
}
