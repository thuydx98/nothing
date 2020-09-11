using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class GiaiPhapModel:ModelBase
    {
        private ANZEntities context;
        
        public string Keyword { set; get; }
        public int PageNumber { set; get; }
        private int PageSize = 5;
        public GiaiPhapModel()
        {
            context = new ANZEntities();
            ID = 0;
            MaDMA = 0;
            MaDM = 0;
            NoiDung = "";
            TenGP = "";
            PageNumber = 1;
        }
        public int ID { set; get; }
        public int MaDMA { set; get; }

        public int MaDM { set; get; }

        [AllowHtml]
        public string NoiDung { set; get; }

        internal void GetGiaiPhap()
        {
            GiaiPhap gp = context.GiaiPhaps.FirstOrDefault<GiaiPhap>(u => u.MaGP == ID);
            this.ID = gp.MaGP;
            this.MaDM = gp.MaDM;
            this.TenGP = gp.TenGP;
            this.MaDMA = gp.MaDMA;
            this.NoiDung = HttpContext.Current.Server.HtmlDecode(gp.NoiDung);
        }

        public string TenGP { set; get; }

        public void ThemGP()
        {
            try
            {
                GiaiPhap gp = new GiaiPhap()
                {
                    MaDM = this.MaDM,
                    MaDMA = this.MaDMA,
                    TenGP = this.TenGP,
                    NoiDung = HttpContext.Current.Server.HtmlEncode(this.NoiDung)
                };
                context.GiaiPhaps.Add(gp);
                context.SaveChanges();
                this.ID = gp.MaGP;
                Disposed();
                Error = ErrorStatus.Message;
                Message = "Thêm giải pháp thành công";
            }catch (Exception)
            {
                Error = ErrorStatus.DatabaseError;
                Message = "Xảy ra lỗi khi thêm giải pháp";
            }
        }

        public void XoaGP()
        {
            using(var trans = context.Database.BeginTransaction())
            {
                try
                {
                    
                    GiaiPhap gp = context.GiaiPhaps.FirstOrDefault<GiaiPhap>(u => u.MaGP == ID);
                    DanhMucAnhModel model = new DanhMucAnhModel(context)
                    {
                        Id = gp.MaDMA
                    };
                    model.XoaDMA();

                    context.GiaiPhaps.Remove(gp);
                    context.SaveChanges();
                    trans.Commit();
                    Error = ErrorStatus.Message;
                    Message = "Xóa giải pháp thành công";
                }
                catch (Exception)
                {
                    trans.Rollback();
                    Error = ErrorStatus.DatabaseError;
                    Message = "Xảy ra lỗi khi xóa giải pháp";
                }
            }
        }
        public void UpdateGP()
        {
            try
            {
                GiaiPhap gp = context.GiaiPhaps.FirstOrDefault<GiaiPhap>(u => u.MaGP == ID);
                gp.MaDM = this.MaDM;
                gp.TenGP = this.TenGP;
                gp.NoiDung = HttpContext.Current.Server.HtmlEncode(this.NoiDung);
                context.SaveChanges();
                Error = ErrorStatus.Message;
                Message = "Cập nhập giải pháp thành công";
            }
            catch (Exception)
            {
                Error = ErrorStatus.DatabaseError;
                Message = "Xảy ra lỗi khi cập nhập giải pháp";
            }
        }

        public IQueryable<GiaiPhap> GetListGPByMaDM(int num)
        {
            return context.GiaiPhaps.Where(u => u.MaDM == this.MaDM).Take<GiaiPhap>(num);
        }
        public IQueryable<GiaiPhap> GetListGPByMaDM()
        {
            return context.GiaiPhaps.Where(u => u.MaDM == this.MaDM);
        }
        public IQueryable<GiaiPhap> GetListGPPhanCung()
        {
            return context.GiaiPhaps.Where(u => u.MaDM == 7).Take<GiaiPhap>(6);
        }
        public IQueryable<GiaiPhap> GetListGPPhanCungs()
        {
            return context.GiaiPhaps.Where(u => u.MaDM == 7);
        }
        public GiaiPhap GetGiaiPhaps()
        {
            return context.GiaiPhaps.FirstOrDefault<GiaiPhap>(u=>u.MaGP == this.ID);
        }
        public IPagedList<GiaiPhap> GetDSTimKiem()
        {
            if (Keyword != null && Keyword.Trim().Length > 0)
            {
                return (from r in context.GiaiPhaps
                        where r.TenGP.Contains(Keyword) && r.MaDM == MaDM
                        orderby r.MaGP ascending
                        select r).ToPagedList<GiaiPhap>(PageNumber, PageSize);
            }
            else
            {
                return (from r in context.GiaiPhaps
                        where r.MaDM == MaDM
                        orderby r.MaGP ascending
                        select r).ToPagedList<GiaiPhap>(PageNumber, PageSize);
            }
        }
        public void Disposed()
        {
            ID = 0;
            MaDMA = 0;
            MaDM = 0;
            NoiDung = "";
            TenGP = "";
            PageNumber = 1;
        }
    }
}