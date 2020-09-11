using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class CategoryNTP
    {
        public CategoryNTP()
        {
            SubCategoryNTP = new HashSet<SubCategoryNTP>();
        }

        public int CatID { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategoryNTP> SubCategoryNTP { get; set; }
    }
}
