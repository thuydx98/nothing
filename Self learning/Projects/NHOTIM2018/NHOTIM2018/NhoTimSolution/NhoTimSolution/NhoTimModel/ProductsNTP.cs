using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class ProductsNTP
    {
        public int ProductID { get; set; }
        public int SubID { get; set; }
        public int BrandID { get; set; }
        public int EmpID { get; set; }
        public int FolderID { get; set; }
        public string Name { get; set; }
        public string SEO { get; set; }
        public string LinkFB { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public int? Status { get; set; }

        public Brand Brand { get; set; }
        public Employees Emp { get; set; }
        public SubCategoryNTP Sub { get; set; }
    }
}
