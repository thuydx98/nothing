using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Rule_Group
    {
        public int RuleID { get; set; }
        public int GroupID { get; set; }

        public Group Group { get; set; }
        public Rules Rule { get; set; }
    }
}
