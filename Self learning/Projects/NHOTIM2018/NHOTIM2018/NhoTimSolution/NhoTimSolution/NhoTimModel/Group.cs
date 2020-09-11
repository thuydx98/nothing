using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Group
    {
        public Group()
        {
            Employee_Group = new HashSet<Employee_Group>();
            Rule_Group = new HashSet<Rule_Group>();
        }

        public int GroupID { get; set; }
        public string Name { get; set; }

        public ICollection<Employee_Group> Employee_Group { get; set; }
        public ICollection<Rule_Group> Rule_Group { get; set; }
    }
}
