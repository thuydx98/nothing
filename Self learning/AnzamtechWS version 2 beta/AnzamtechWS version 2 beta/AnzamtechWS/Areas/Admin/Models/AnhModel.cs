using AnzamtechWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class AnhModel
    {
        private ANZEntities sc;
        public int MaAnh { get; set; }
        public int MaLA { get; set; }
        public int MaDMA { get; set; }
        public string TenAnh { get; set; }
        public string Path { get; set; }
        public int TrangThai { get; set; }
        public string Tag { set; get; }
       
        public AnhModel()
        {
            sc = new ANZEntities();
            MaAnh = 0;
            MaLA = 0;
            MaDMA = 0;
            TenAnh = "";
            Path = "";
            Tag = ""; 
            TrangThai = 0;
        }
        public AnhModel(ANZEntities context)
        {
            sc = context;
            MaAnh = 0;
            MaLA = 0;
            MaDMA = 0;
            TenAnh = "";
            Path = "";
            Tag = "";
            TrangThai = 0;
        }
        public dynamic GetAnhByID()
        {
            var tmp = sc.Anhs.Where(a => a.MaAnh == MaAnh && a.TrangThai == 0).ToList<Anh>().FirstOrDefault<Anh>();
            return new { Path = tmp.Path, Tag = tmp.Tag, TenAnh = tmp.TenAnh};

        }
        public void Add()
        {
            Anh a = new Anh()
            {
                MaLA = this.MaLA,
                MaDMA = this.MaDMA,
                TenAnh = this.TenAnh,
                Path = this.Path,
                Tag = this.Tag,
                TrangThai=this.TrangThai
            };
            sc.Anhs.Add(a);
            sc.SaveChanges();
            this.MaAnh = a.MaAnh;
        }

        internal void UpdateFileName()
        {
            var p = sc.Anhs.Single(u => u.MaAnh == this.MaAnh);
            p.TenAnh = this.TenAnh;
            sc.SaveChanges();
        }

        public void UpdateTrangThai()
        {
            var p = sc.Anhs.Single(u => u.MaAnh == this.MaAnh);
            p.TrangThai = this.TrangThai;
            sc.SaveChanges();
        }
        public void Remove()
        {
            var p = sc.Anhs.Single(u => u.MaAnh == this.MaAnh);
            p.TrangThai = 0;
            sc.SaveChanges();
        }

        public int XoaAnh()
        {
            using (var trans = sc.Database.BeginTransaction())
            {
                try
                {
                    var p = sc.Anhs.FirstOrDefault<Anh>(u => u.MaAnh == this.MaAnh);
                    string path = p.Path + "/" + p.TenAnh;
                    sc.Anhs.Remove(p);
                    sc.SaveChanges();
                    File.Delete(HttpContext.Current.Server.MapPath(path));
                    trans.Commit();
                    return 0;
                }catch (Exception){
                    trans.Rollback();
                    return -1;
                }
            }
        }
    }
}