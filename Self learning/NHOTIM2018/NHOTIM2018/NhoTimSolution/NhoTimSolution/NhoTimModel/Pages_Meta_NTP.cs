using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Pages_Meta_NTP
    {
        public int PageID { get; set; }
        public int MetaID { get; set; }
        public string Value { get; set; }

        public MetaTag Meta { get; set; }
        public PagesNTP Page { get; set; }
    }
}
