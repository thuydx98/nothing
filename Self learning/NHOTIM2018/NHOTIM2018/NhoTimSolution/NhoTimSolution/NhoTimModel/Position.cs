using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Position
    {
        public Position()
        {
            Emp_Dep = new HashSet<Emp_Dep>();
        }

        public int PosID { get; set; }
        public string Name { get; set; }

        public ICollection<Emp_Dep> Emp_Dep { get; set; }
    }
}
