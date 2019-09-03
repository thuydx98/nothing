using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class SubCategoryNTP
    {
        public SubCategoryNTP()
        {
            ProductsNTP = new HashSet<ProductsNTP>();
        }

        public int SubID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }

        public CategoryNTP Cat { get; set; }
        public ICollection<ProductsNTP> ProductsNTP { get; set; }
    }
}
