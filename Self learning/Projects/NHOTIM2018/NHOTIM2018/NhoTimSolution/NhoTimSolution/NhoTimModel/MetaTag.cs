using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class MetaTag
    {
        public MetaTag()
        {
            Pages_Meta_NTP = new HashSet<Pages_Meta_NTP>();
        }

        public int MetaID { get; set; }
        public string MetaTag1 { get; set; }
        public string MetaName { get; set; }
        public int? MaxLength { get; set; }

        public ICollection<Pages_Meta_NTP> Pages_Meta_NTP { get; set; }
    }
}
