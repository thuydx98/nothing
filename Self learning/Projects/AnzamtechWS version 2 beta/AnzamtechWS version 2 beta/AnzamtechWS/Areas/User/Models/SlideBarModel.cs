using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.User.Models
{
    public class SlideBarModel
    {
        public int MaDMBV { set; get; }
        public List<DanhMucBaiViet> DanhMucBaiViets { set; get; }
    }
}