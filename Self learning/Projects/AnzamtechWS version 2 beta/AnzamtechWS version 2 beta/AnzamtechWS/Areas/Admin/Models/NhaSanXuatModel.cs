using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class NhaSanXuatModel
    {
        public NhaSanXuatModel()
        {
            sc = new ANZEntities();
            MaNSX = 0;
            TenNSX = "";
        }
        private ANZEntities sc;
        public int MaNSX { set; get; }
        public string TenNSX { set; get; }
        public NhaSanXuat GetNSXByID()
        {
            return sc.NhaSanXuats.Where(r => r.MaNSX == MaNSX).ToList<NhaSanXuat>().FirstOrDefault<NhaSanXuat>();
        }
        public void Add()
        {
            NhaSanXuat nsx = new NhaSanXuat()
            {
                MaNSX=this.MaNSX,
                TenNSX=this.TenNSX

            };
            sc.NhaSanXuats.Add(nsx);
            sc.SaveChanges();
            this.MaNSX = nsx.MaNSX;
        }
    }
}