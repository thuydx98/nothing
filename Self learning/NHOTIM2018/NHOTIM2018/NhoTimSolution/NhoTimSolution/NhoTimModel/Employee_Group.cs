using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Employee_Group
    {
        public int EmpID { get; set; }
        public int GroupID { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime ModifyDate { get; set; }

        public Employees Emp { get; set; }
        public Group Group { get; set; }
    }
}
