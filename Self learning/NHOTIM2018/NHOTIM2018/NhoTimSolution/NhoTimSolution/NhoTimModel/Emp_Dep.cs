using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Emp_Dep
    {
        public int DepID { get; set; }
        public int EmpID { get; set; }
        public int PosID { get; set; }
        public string Description { get; set; }

        public Departments Dep { get; set; }
        public Employees Emp { get; set; }
        public Position Pos { get; set; }
    }
}
