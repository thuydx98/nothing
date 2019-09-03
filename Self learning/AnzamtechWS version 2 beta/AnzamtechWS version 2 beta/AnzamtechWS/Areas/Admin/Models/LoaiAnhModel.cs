using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class LoaiAnhModel
    {
        private ANZEntities sc;
        public LoaiAnhModel()
        {
            sc = new ANZEntities();
            MaLA = 0;
            TenLoai = "";
        }

        public int MaLA { get;set; }
        public string TenLoai { get; set; }

        public LoaiAnh GetLoaiAnhByID()
        {
            return sc.LoaiAnhs.Where(la => la.MaLA == MaLA).ToList<LoaiAnh>().FirstOrDefault<LoaiAnh>();
        }
        public void Add()
        {
            LoaiAnh la = new LoaiAnh()
            {
                MaLA=this.MaLA,
                TenLoai=this.TenLoai
            };
            sc.LoaiAnhs.Add(la);
            sc.SaveChanges();
            this.MaLA = la.MaLA;
        }

    }
}