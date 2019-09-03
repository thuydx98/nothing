using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.User.Models
{
    public class GioHangModel
    {
        ANZEntities sc = new ANZEntities();
        public int MaSP { get; set; }
        public int SoLuong { set; get; }

        public string TenSP { set; get; }
        public int MaDMA { set; get; }
        public double GiaSP { set; get; }
        public string GetImagePath()
        {
            var item = (from s in sc.Anhs where s.MaDMA == this.MaDMA select new { s.Path }).Take(1).Single();
            return item.Path;
        }
    }
}