using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Brand
    {
        public Brand()
        {
            ProductsNTP = new HashSet<ProductsNTP>();
        }

        public int BrandID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductsNTP> ProductsNTP { get; set; }
    }
}
