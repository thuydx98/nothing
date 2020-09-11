using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class PagesNTP
    {
        public PagesNTP()
        {
            InversePageChildNavigation = new HashSet<PagesNTP>();
            Pages_Meta_NTP = new HashSet<Pages_Meta_NTP>();
        }

        public int PageID { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public int? FolderID { get; set; }
        public int? PageChild { get; set; }

        public Folders Folder { get; set; }
        public PagesNTP PageChildNavigation { get; set; }
        public ICollection<PagesNTP> InversePageChildNavigation { get; set; }
        public ICollection<Pages_Meta_NTP> Pages_Meta_NTP { get; set; }
    }
}
