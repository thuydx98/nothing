using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Files
    {
        public int FileID { get; set; }
        public int FolderID { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Path { get; set; }
        public string TypeFile { get; set; }
        public string CSS { get; set; }
        public string Description { get; set; }

        public Folders Folder { get; set; }
    }
}
