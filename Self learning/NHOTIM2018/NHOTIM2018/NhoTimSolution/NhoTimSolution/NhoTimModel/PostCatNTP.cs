using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class PostCatNTP
    {
        public PostCatNTP()
        {
            PostsNTP = new HashSet<PostsNTP>();
        }

        public int CatID { get; set; }
        public string Name { get; set; }

        public ICollection<PostsNTP> PostsNTP { get; set; }
    }
}
