using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class GroupDanhMuc
    {
        public GroupDanhMuc()
        {
            this.DanhMucModel = new DanhMucModel();
            this.DanhMucConModel = new DanhMucConModel();
        }
        public DanhMucModel DanhMucModel { set; get; }
        public DanhMucConModel DanhMucConModel { set; get; }
    }
}