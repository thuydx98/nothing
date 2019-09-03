using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Departments
    {
        public Departments()
        {
            Emp_Dep = new HashSet<Emp_Dep>();
        }

        public int DepID { get; set; }
        public string Name { get; set; }

        public ICollection<Emp_Dep> Emp_Dep { get; set; }
    }
}
