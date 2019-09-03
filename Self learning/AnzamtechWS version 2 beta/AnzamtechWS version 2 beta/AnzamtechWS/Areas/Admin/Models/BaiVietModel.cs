using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class BaiVietModel:ModelBase
    {
        public BaiVietModel()
        {
            sc = new ANZEntities();
            MaDMA = 0;
            MaBV = 0;
            MaNV = 0;
            NgayDang= new DateTime(1990, 1, 1);
            MaDMBV = 0;
            NoiDung = "";
            LinkFB = "";
            TrangThai = 0;
            MoTa = "";
            PageNumber = 1;
            TenBV = "";
            ListTags = null;
            Keyword = "";
        }

        public void XoaBV()
        {
            using(var trans = sc.Database.BeginTransaction())
            {
                try
                {

                    var bv = sc.BaiViets.FirstOrDefault<BaiViet>(u => u.MaBV == this.MaBV);
                    var listtag = sc.Tags.Where<Tag>(u => u.BaiViets.Any(a => a.MaBV == bv.MaBV));
                    foreach (var item in listtag)
                    {
                        bv.Tags.Remove(item);
                    }
                    sc.SaveChanges();
                    sc.BaiViets.Remove(bv);
                    sc.SaveChanges();
                    DanhMucAnhModel dmamodel = new DanhMucAnhModel(sc)
                    {
                        Id = bv.MaDMA   
                    };
                    dmamodel.XoaDMA();
                    trans.Commit();
                    Error = ErrorStatus.Message;
                    Message = "Xóa bài viết thành công";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    Error = ErrorStatus.DatabaseError;
                    Message = ex.ToString();
                }
            }
        }

        public void GetBaiViet()
        {
            BaiViet bv = sc.BaiViets.FirstOrDefault<BaiViet>(u => u.MaBV == this.MaBV);
            this.TenBV = bv.TenBV;
            this.MaDMA = bv.MaDMA;
            this.MaNV = bv.MaNV;
            this.NgayDang = bv.NgayDang;
            this.MaDMBV = bv.MaDMBV;
            this.NoiDung = HttpContext.Current.Server.HtmlDecode(bv.NoiDung);
            this.LinkFB = bv.LinkFB;
            this.MoTa = bv.MoTa;
            this.TrangThai = bv.TrangThai;
            this.ListTags = bv.Tags;
        }
        
        public string TenBV { set; get; }
        private int PageSize = 5;
        public int PageNumber { set; get; }
        public ICollection<Tag> ListTags { set; get; }
        private ANZEntities sc;
        public int MaDMA { set; get; }
        public int MaBV { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayDang { get; set; }
        public int MaDMBV { get; set; }
        [AllowHtml]
        public string NoiDung { get ; set; }
        public string LinkFB { get; set; }
        public string MoTa { set; get; }
        public int TrangThai { get; set; }
        public string Keyword { set; get; }

        public IPagedList<BaiViet> GetDSTimKiem()
        {
            if (Keyword != null && Keyword.Trim().Length > 0)
            {
                return (from r in sc.BaiViets
                        where (r.Tags.Any(u=>u.TagName.Contains(Keyword)) || r.TenBV.Contains(Keyword)) && r.MaDMBV == MaDMBV
                        orderby r.NgayDang ascending
                        select r).ToPagedList<BaiViet>(PageNumber, PageSize);
            }
            else
            {
                return (from r in sc.BaiViets
                        where r.MaDMBV == MaDMBV
                        orderby r.NgayDang ascending
                        select r).ToPagedList<BaiViet>(PageNumber, PageSize);
            }
        }

        public BaiViet GetBaiVietByID()
        {
            return sc.BaiViets.Where(bv => bv.MaBV == MaBV).ToList<BaiViet>().FirstOrDefault<BaiViet>();

        }

        public void Update()
        {
            using(var trans = sc.Database.BeginTransaction())
            {
                try
                {
                    var p = sc.BaiViets.Single(u => u.MaBV == this.MaBV);

                    p.TenBV = this.TenBV;
                    p.MaDMBV = this.MaDMBV;
                    p.NoiDung = HttpContext.Current.Server.HtmlDecode(this.NoiDung);
                    p.LinkFB = this.LinkFB;
                    p.MoTa = this.MoTa;
                    if (ListTags != null)
                    {
                        var bv = sc.BaiViets.FirstOrDefault<BaiViet>(u => u.MaBV == this.MaBV);
                        var listtag = sc.Tags.Where<Tag>(u => u.BaiViets.Any(a => a.MaBV == this.MaBV));
                        foreach (var item in listtag)
                        {
                            bv.Tags.Remove(item);
                        }
                        sc.SaveChanges();
                        foreach (var item in ListTags)
                        {
                            bv.Tags.Add(sc.Tags.FirstOrDefault<Tag>(u => u.MaTag == item.MaTag));
                        }
                    }
                    sc.SaveChanges();
                    trans.Commit();
                    Error = ErrorStatus.Message;
                    Message = "Sửa bài viết thành công";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    Error = ErrorStatus.Message;
                    Message = ex.ToString();
                }
            }
        }
        public void Add()
        {
            using (var trans = sc.Database.BeginTransaction())
            {
                try
                {
                    BaiViet bv = new BaiViet
                    {
                        MaDMA = this.MaDMA,
                        MaNV = this.MaNV,
                        NgayDang = DateTime.Now,
                        MaDMBV = this.MaDMBV,
                        NoiDung = HttpContext.Current.Server.HtmlEncode(this.NoiDung),
                        TrangThai = 0,
                        LinkFB = this.LinkFB,
                        MoTa = this.MoTa,
                        TenBV = this.TenBV

                    };
                    sc.BaiViets.Add(bv);
                    sc.SaveChanges();
                    if (ListTags != null)
                    {
                        var bv1 = sc.BaiViets.FirstOrDefault<BaiViet>(u => u.MaBV == bv.MaBV);
                        foreach (var item in ListTags)
                        {
                            bv1.Tags.Add(sc.Tags.FirstOrDefault<Tag>(u => u.MaTag == item.MaTag)); 
                        }
                        sc.SaveChanges();
                    }
                    trans.Commit();
                    Error = ErrorStatus.Message;
                    Message = "Thêm bài viết thành công";
                    Disposed();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    trans.Dispose();
                    Error = ErrorStatus.DatabaseError;
                    Message = "Có lỗi xảy ra trong quá trình thêm bài viết";
                }
            }
        }

        public SelectList GetDanhMucBV()
        {
            return new SelectList(sc.DanhMucBaiViets.Select(u => new { MaDM = u.MaDMBV, TenDM = u.TenDanhMuc }), "MaDM", "TenDM");
        }
        public List<DanhMucBaiViet> GetDanhMucs()
        {
            return sc.DanhMucBaiViets.ToList<DanhMucBaiViet>();
        }
        public IPagedList<BaiViet> ListBaiVietByMaDM(int MADMBV)
        {
            return (from r in sc.BaiViets
                    where r.MaDMBV == MADMBV
                    orderby r.NgayDang descending
                    select r).ToPagedList<BaiViet>(PageNumber, PageSize);
        }
      

        public IQueryable<BaiViet> GetBaiVietNoiBat(int nums)
        {
            return (from r in sc.BaiViets
                    orderby r.NgayDang descending select r).Take<BaiViet>(nums);

        }
        public void Disposed()
        {
            MaDMA = 0;
            MaBV = 0;
            MaNV = 0;
            NgayDang = new DateTime(1990, 1, 1);
            MaDMBV = 0;
            NoiDung = "";
            LinkFB = "";
            TrangThai = 0;
            MoTa = "";
            PageNumber = 1;
            TenBV = "";
            ListTags = null;
            Keyword = "";
        }

    }
}