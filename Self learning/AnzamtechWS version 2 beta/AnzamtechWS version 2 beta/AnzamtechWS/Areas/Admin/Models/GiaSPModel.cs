using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class GiaSPModel
    {
        public GiaSPModel()
        {
            sc = new ANZEntities();
            MaGia = 0;
            MaSP = 0;
            MaNV = 0;
            GiaSP = 0;
            NgayTao = new DateTime(1990, 1, 1);
        }
        public ANZEntities sc { set; get; }

        public int MaGia { get; set; }
        public int MaSP { get; set; }
        public int MaNV { get; set; }
        public float GiaSP { get; set; }
        public DateTime NgayTao { get; set; }

        public GiaSP GetGiaSPByID()
        {
            return sc.GiaSPs.Where(gsp => gsp.MaGia == GiaSP).ToList<GiaSP>().FirstOrDefault<GiaSP>();
        }
        public IQueryable<GiaSP> GetGiaSPByMaSP()
        {
            return sc.GiaSPs.Where(r => r.MaSP == this.MaSP);
        }
        public void Add()
        {
            GiaSP gsp = new GiaSP()
            {
                MaSP = this.MaSP,
                MaNV = this.MaNV,
                GiaSP1 = this.GiaSP,
                NgayTao= DateTime.Now
            };
            sc.GiaSPs.Add(gsp);
            sc.SaveChanges();
            this.MaGia = gsp.MaGia;
        }

    }
}