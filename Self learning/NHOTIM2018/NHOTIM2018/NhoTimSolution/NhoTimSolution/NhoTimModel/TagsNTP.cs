using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class TagsNTP
    {
        public TagsNTP()
        {
            Post_TagNTP = new HashSet<Post_TagNTP>();
        }

        public int TagID { get; set; }
        public string Name { get; set; }

        public ICollection<Post_TagNTP> Post_TagNTP { get; set; }
    }
}
