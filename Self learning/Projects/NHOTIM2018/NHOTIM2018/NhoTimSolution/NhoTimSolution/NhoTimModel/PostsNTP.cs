using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class PostsNTP
    {
        public PostsNTP()
        {
            Post_TagNTP = new HashSet<Post_TagNTP>();
        }

        public int PostID { get; set; }
        public int FolderID { get; set; }
        public int EmpID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string LinkFB { get; set; }
        public int Status { get; set; }

        public PostCatNTP Cat { get; set; }
        public Employees Emp { get; set; }
        public Folders Folder { get; set; }
        public ICollection<Post_TagNTP> Post_TagNTP { get; set; }
    }
}
