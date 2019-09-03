using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class DanhMucAnhModel
    {
        public DanhMucAnhModel()
        {
            sc = new ANZEntities();
            Id = 0;
            TenDM = "";
            Path = "";
            TrangThai = "";
        }
        public DanhMucAnhModel(ANZEntities context)
        {
            sc = context;
            Id = 0;
            TenDM = "";
            Path = "";
            TrangThai = "";
        }
        private ANZEntities sc;

        public int Id { get; set; }
        public string TenDM { get; set; }
        public string Path { get; set; }
        public string TrangThai { get; set; }
        public void XoaDMA()
        {
          
            var dma = sc.DanhMucAnhs.FirstOrDefault<DanhMucAnh>(u => u.MaDMA == this.Id);
            var path = dma.Path + "/" + dma.TenDM;
            sc.Anhs.RemoveRange(sc.Anhs.Where<Anh>(u => u.MaDMA == dma.MaDMA));
            sc.SaveChanges();
            sc.DanhMucAnhs.Remove(dma);
            sc.SaveChanges();
            if (Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.Delete(HttpContext.Current.Server.MapPath(path), true);
            }
        }
        public void Add()
        {
            DanhMucAnh dma = new DanhMucAnh()
            {
                Path = this.Path,
                TenDM = this.TenDM,
                TrangThai = 0
            };
            sc.DanhMucAnhs.Add(dma);
            sc.SaveChanges();
            this.Id = dma.MaDMA;
        }

        public DanhMucAnh GetDanhMucAnhByID()
        {
            return sc.DanhMucAnhs.Where(dm => dm.MaDMA == Id).ToList<DanhMucAnh>().FirstOrDefault<DanhMucAnh>();

        }

        public List<Anh> GetAnhs()
        {
           
            return sc.Anhs.Where(a => a.MaDMA == Id).ToList<Anh>();
        }

        internal void UpdateID()
        {
            DanhMucAnh dma = sc.DanhMucAnhs.Where(a => a.MaDMA == this.Id).FirstOrDefault<DanhMucAnh>();
            dma.TenDM += "-" + dma.MaDMA;
            sc.SaveChanges();
            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dma.Path + "/" + dma.TenDM));

        }
    }
}