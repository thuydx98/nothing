using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Rules
    {
        public Rules()
        {
            Rule_Group = new HashSet<Rule_Group>();
        }

        public int RuleID { get; set; }
        public string Name { get; set; }

        public ICollection<Rule_Group> Rule_Group { get; set; }
    }
}
