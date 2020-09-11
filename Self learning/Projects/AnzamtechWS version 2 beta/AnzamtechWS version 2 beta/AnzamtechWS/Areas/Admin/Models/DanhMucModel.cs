using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class DanhMucModel:ModelBase
    {
        public DanhMucModel()
        {
            sc = new ANZEntities();
            MaDM = 0;
            TenDanhMuc = "";
            Tag = "";
        }
        ANZEntities sc;
        
        public int MaDM { set; get; }
        public string PageController { set; get; }
        public string Keyword { set; get; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string TenDanhMuc { set; get; }
        public string Tag { set; get; }
        public ICollection<DanhMucCon> DanhMucCons { set; get; }
        public void ThemDM()
        {
            try
            {
                DanhMuc dm = new DanhMuc
                {
                    TenDanhMuc = this.TenDanhMuc,
                    Tag = this.Tag

                };
                sc.DanhMucs.Add(dm);
                sc.SaveChanges();
                this.MaDM = dm.MaDM;
                this.Error = ErrorStatus.Message;
                this.Message = "Thêm danh mục thành công";
                this.Disposed();
            }catch (Exception)
            {
                this.Error = ErrorStatus.DatabaseError;
                this.Message = "Xảy ra lỗi trong quá trình thêm dữ liệu";
            }
        }
    
        public IQueryable<DanhMuc> GetDSTimKiem()
        {
           if (this.Keyword != null && this.Keyword.Trim() != "")
            {
                return from r in sc.DanhMucs
                       where r.TenDanhMuc.Contains(Keyword)
                       select r;
            }
            else
            {
                return from r in sc.DanhMucs
                       select r;
            }
                  
        }

        public void GetDAnhMucByID()
        {
            DanhMuc dm = (from r in sc.DanhMucs
                          where r.MaDM == this.MaDM 
                          select r).FirstOrDefault<DanhMuc>();
            this.MaDM = dm.MaDM;
            this.TenDanhMuc = dm.TenDanhMuc;
            this.Tag = dm.Tag;
            this.PageController = dm.PageController;
            this.DanhMucCons = dm.DanhMucCons;

        }
        public void UpdateDanhMuc()
        {
            DanhMuc dm = (from r in sc.DanhMucs
                          where r.MaDM == this.MaDM
                          select r).FirstOrDefault<DanhMuc>();
            dm.TenDanhMuc = this.TenDanhMuc;
            dm.Tag = this.Tag;
            dm.PageController = this.PageController;
            sc.SaveChanges();
        }
        public IQueryable GetJSONDSDanhMuc()
        {
            var dm = (from r in sc.DanhMucs
                      select new { MaDM = r.MaDM, TenDM = r.TenDanhMuc });
            return dm;
        }
        public IQueryable<DanhMuc> GetDSDanhMuc()
        {
            var dm = (from r in sc.DanhMucs
                      select r);
            return dm;
        }
        public IQueryable<DanhMuc> GetDSDanhMucGP()
        {
            var dm = (from r in sc.DanhMucs
                      where r.PageController != null
                      select r);
            return dm;
        }

        public void Disposed()
        {
            MaDM = 0;
            TenDanhMuc = "";
            Tag = "";
        }
    }
}