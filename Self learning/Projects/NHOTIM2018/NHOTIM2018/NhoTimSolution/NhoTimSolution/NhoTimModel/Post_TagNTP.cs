using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Post_TagNTP
    {
        public int PostID { get; set; }
        public int TagID { get; set; }

        public PostsNTP Post { get; set; }
        public TagsNTP Tag { get; set; }
    }
}
