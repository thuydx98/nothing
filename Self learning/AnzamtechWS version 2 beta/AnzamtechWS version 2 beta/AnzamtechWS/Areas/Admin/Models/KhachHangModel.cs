using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class KhachHangModel
    {
        public KhachHangModel()
        {
            sc = new ANZEntities();
            MaKH = 0;
            HoKH = "";
            TenKH = "";
            NgaySinh= new DateTime(1700, 1, 1);
            SDT = "";
            Email = "";
        }
        private ANZEntities sc;

        public int MaKH { get; set; }
        public string HoKH { get; set; }
        public string TenKH { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set ; }

        public KhachHang GetKhachHangByID()
        {
            return sc.KhachHangs.Where(kh => kh.MaKH == MaKH).ToList<KhachHang>().FirstOrDefault<KhachHang>();
        }
        public void Add()
        {
            KhachHang kh = new KhachHang()
            {
                MaKH=this.MaKH,
                HoKH=this.HoKH,
                NgaySinh=this.NgaySinh,
                SDT=this.SDT,
                Email=this.Email
            };
            sc.KhachHangs.Add(kh);
            sc.SaveChanges();
            this.MaKH = kh.MaKH;
        }
    }
}