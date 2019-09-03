using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Folders
    {
        public Folders()
        {
            Employees = new HashSet<Employees>();
            Files = new HashSet<Files>();
            InverseSubFolder = new HashSet<Folders>();
            PagesNTP = new HashSet<PagesNTP>();
            PostsNTP = new HashSet<PostsNTP>();
        }

        public int FolderID { get; set; }
        public int? SubFolderID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Status { get; set; }

        public Folders SubFolder { get; set; }
        public ICollection<Employees> Employees { get; set; }
        public ICollection<Files> Files { get; set; }
        public ICollection<Folders> InverseSubFolder { get; set; }
        public ICollection<PagesNTP> PagesNTP { get; set; }
        public ICollection<PostsNTP> PostsNTP { get; set; }
    }
}
