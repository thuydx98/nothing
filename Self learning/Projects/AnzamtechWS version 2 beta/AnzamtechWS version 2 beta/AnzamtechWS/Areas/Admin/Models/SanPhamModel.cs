using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Models
{
    /*
     * Trạng thái:
     * 0 sản phẩm đăng chưa bán
     * 1 sản phẩm đang hoạt động
     * 2 sản phẩm hết hàng
     * 3 sản phẩm bị xóa
     */
    public class SanPhamModel
    {

        public SanPhamModel()
        {
            ContextANZ = new ANZEntities();
            MaSP = 0;
            MaDMA = 0;
            MaDMC = 0;
            TenSP = "";
            Mota = "";
            Tag = "";
            ChiTietSP = "";
            GiaTienSP = 0;
            SoLuong = 0;
            MaDM = 0;
            Keyword = "";
            NgayTao = new DateTime(1990, 1, 1);
            NgaySua = new DateTime(1990, 1, 1);
            PageNumber = 1;
            SoNguoiTruyCap = 0;
            TrangThai = 0;
        }
        private int PageSize = 12;

        public int PageNumber { set; get; }
        public ANZEntities ContextANZ { set; get; }
        public int MaDM { set; get; }
        public string Keyword { set; get; }
        public int MaSP { set; get; }
        public int MaDMA { get; set; }
        public int MaDMC { get; set; }
        public string TenSP { get; set; }
        public string Mota { get; set; }
        public string Tag { get; set; }

        [AllowHtml]
        public string ChiTietSP { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayTao { get; set; }

        internal void Update()
        {
            using (var trans = ContextANZ.Database.BeginTransaction())
            {
                try
                {
                    if (GiaTienSP > 0)
                    {
                        GiaSPModel model = new GiaSPModel()
                        {
                            sc = ContextANZ
                        };
                        model.MaNV = 2; ///////////////////////////////Sửa session login
                        model.GiaSP = GiaTienSP;
                        model.MaSP = MaSP;
                        model.Add();
                    }

                    var item = ContextANZ.SanPhams.Where(s => s.MaSP == this.MaSP).FirstOrDefault<SanPham>();
                    item.MaDMC = MaDMC;
                    item.TenSP = TenSP;
                    item.Tag = Tag;
                    item.MoTa = Mota;
                    item.ChiTienSP = ChiTietSP;
                    ContextANZ.SaveChanges();
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
        }

        public DateTime NgaySua { get; set; }
        public int SoNguoiTruyCap { get; set; }
        public int TrangThai { get; set; }
        public float GiaTienSP { set; get; }
        
        public SanPham GetSanPhamByMaSP()
        {
            return ContextANZ.SanPhams.Where(sp => sp.MaSP == MaSP).ToList<SanPham>().FirstOrDefault<SanPham>(); 
        }


        public void GetSanPhamByID()
        {
            SanPham s = ContextANZ.SanPhams.Where(sp => sp.MaSP == MaSP).ToList<SanPham>().FirstOrDefault<SanPham>();
   
            MaDMA = (s.MaDMA==null)?0:(int)s.MaDMA;
            MaDM = s.DanhMucCon.MaDM;
            MaDMC = s.MaDMC;
            TenSP = s.TenSP;
            Mota = s.MoTa;
            Tag = s.Tag;
            ChiTietSP = HttpContext.Current.Server.HtmlDecode(s.ChiTienSP);
            SoLuong = (s.SoLuong == null) ? 0 : (int)s.SoLuong;
            NgayTao = s.NgayTao;
            NgaySua = (s.NgaySua == null) ? new DateTime() : (DateTime)s.NgaySua; ;
            SoNguoiTruyCap = (s.SoNguoiTruyCap == null) ? 0 : (int)s.SoNguoiTruyCap;
            TrangThai = s.TrangThai;
        }

        public SelectList DSTrangThai()
        {
            List<object> list = new List<Object>
            {
                new { TrangThai = -1, TT = "Chọn" },
                new { TrangThai = 0, TT = "Đăng chưa bán" },
                new { TrangThai = 1, TT = "Đang hoạt động" },
                new { TrangThai = 2, TT = "Hết hàng" },
                new { TrangThai = 3, TT = "Đã xóa" }
            };
            return new SelectList(list, "TrangThai", "TT");
        }
        public dynamic TimKiemSP()
        {
            if (Keyword != null)
            {
                if (this.MaDM != 0 && this.MaDMC == 0 && this.TrangThai == -1)
                {
                    return from r in ContextANZ.SanPhams
                           join dmc in ContextANZ.DanhMucCons
                           on r.MaDMC equals dmc.MaDMC
                           where dmc.MaDM == this.MaDM &&
                           (r.TenSP == DbFunctions.AsUnicode(this.Keyword) || r.MoTa == DbFunctions.AsUnicode(this.Keyword) || r.ChiTienSP == DbFunctions.AsUnicode(this.Keyword))
                           select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
                }
                else if (this.MaDM != 0 && this.MaDMC != 0 && this.TrangThai == -1)
                {
                    return from r in ContextANZ.SanPhams
                           where r.MaDMC == this.MaDMC &&
                            (r.TenSP == DbFunctions.AsUnicode(this.Keyword) || r.MoTa == DbFunctions.AsUnicode(this.Keyword) || r.ChiTienSP == DbFunctions.AsUnicode(this.Keyword))
                           select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
                }
                else if(this.MaDM != 0 && this.MaDMC != 0 && this.TrangThai != -1)
                {
                    return from r in ContextANZ.SanPhams
                           where r.MaDMC == this.MaDMC && r.TrangThai == this.TrangThai &&
                            (r.TenSP == DbFunctions.AsUnicode(this.Keyword) || r.MoTa == DbFunctions.AsUnicode(this.Keyword) || r.ChiTienSP == DbFunctions.AsUnicode(this.Keyword))
                           select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
                }
                else
                {
                    return from r in ContextANZ.SanPhams
                           where r.TrangThai == this.TrangThai &&
                           (r.TenSP.Contains(DbFunctions.AsUnicode(this.Keyword)) || r.MoTa.Contains(DbFunctions.AsUnicode(this.Keyword)) || r.ChiTienSP.Contains(DbFunctions.AsUnicode(this.Keyword)))
                           select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
                }

            }

            if (this.MaDM != 0 && this.MaDMC == 0 && this.TrangThai == -1)
            {
                return from r in ContextANZ.SanPhams
                       join dmc in ContextANZ.DanhMucCons
                       on r.MaDMC equals dmc.MaDMC
                       where dmc.MaDM == this.MaDM
                       select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
            }
            else if (this.MaDM != 0 && this.MaDMC != 0 && this.TrangThai == -1)
            {
                return from r in ContextANZ.SanPhams
                       where r.MaDMC == this.MaDMC
                       select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
            }
            else if (this.MaDM != 0 && this.MaDMC != 0 && this.TrangThai != -1)
            {
                return from r in ContextANZ.SanPhams
                       where r.MaDMC == this.MaDMC && r.TrangThai == this.TrangThai
                       select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh = r.DanhMucAnh, TenSP = r.TenSP, GiaSPs = r.GiaSPs, SoLuong = r.SoLuong, TrangThai = r.TrangThai };
            }
            else
            {
                return from r in ContextANZ.SanPhams
                       where r.TrangThai == this.TrangThai
                       select new SanPhamResult { MaSP = r.MaSP, DanhMucAnh= r.DanhMucAnh,TenSP = r.TenSP, GiaSPs= r.GiaSPs, SoLuong = r.SoLuong, TrangThai =  r.TrangThai };
            }



        }
        public IEnumerable<SanPham> GetSanPhamLienQuan()
        {
            return (from r in ContextANZ.SanPhams
                    where r.TrangThai == 0 || r.TrangThai == 1 && r.TenSP.Contains(this.TenSP)
                    select r).Take<SanPham>(4);
        }
        public IQueryable<SanPham> GetSanPhamNoiBat()
        {
            return (from r in ContextANZ.SanPhams
                    where r.TrangThai == 0 || r.TrangThai == 1
                    orderby r.NgayTao descending
                    select r).Take<SanPham>(8);
        }

        public IPagedList<SanPham> GetSanPhamByMaDM()
        {
            return (from r in ContextANZ.SanPhams
                    where (r.TrangThai == 0 || r.TrangThai == 1) && r.DanhMucCon.MaDM == this.MaDM
                    orderby r.NgayTao descending
                    select r
                    ).ToPagedList<SanPham>(PageNumber, PageSize);
        }
        public IPagedList<SanPham> GetSanPhamByMaDMC()
        {
            return (from r in ContextANZ.SanPhams
                    where (r.TrangThai == 0 || r.TrangThai == 1) && r.MaDMC == this.MaDMC
                    orderby r.NgayTao descending
                    select r
                    ).ToPagedList<SanPham>(PageNumber, PageSize);
        }
        public void Add()
        {
            SanPham sp = new SanPham()
            {
                MaSP = this.MaSP,
                MaDMA = this.MaDMA,
                MaDMC = this.MaDMC,
                TenSP = this.TenSP,
                MoTa = this.Mota,
                Tag = this.Tag,
                ChiTienSP = "",
                SoLuong = 0,
                NgayTao = DateTime.Now.Date,
                NgaySua = null,
                SoNguoiTruyCap = 0,
                TrangThai = 0
            };
            ContextANZ.SanPhams.Add(sp);
            ContextANZ.SaveChanges();
            this.MaSP = sp.MaSP;
        }
        public void Remove()
        {
            var item = ContextANZ.SanPhams.Single(u => u.MaSP == this.MaSP);
            item.TrangThai = 0;
            ContextANZ.SaveChanges();
        }
        public void UpdateMoTa()
        {
            var p = ContextANZ.SanPhams.Single(u => u.MaSP == this.MaSP);
            p.MoTa = this.Mota;
            ContextANZ.SaveChanges();
        }
        public void UpdateChiTiet()
        {
            var p = ContextANZ.SanPhams.Single(u => u.MaSP == this.MaSP);
            p.ChiTienSP = HttpContext.Current.Server.HtmlEncode(this.ChiTietSP);
            ContextANZ.SaveChanges();
        }
        public void UpdateTrangThai()
        {
            var p = ContextANZ.SanPhams.Single(u => u.MaSP == this.MaSP);
            p.TrangThai = this.TrangThai;
            ContextANZ.SaveChanges();
        }

    }

    public class SanPhamResult
    {
        public int MaSP { set; get; }
        public DanhMucAnh DanhMucAnh { set; get; }
        public string TenSP { set; get; }
        
        public int? SoLuong { set; get; }
        public int TrangThai { set; get; }
        public ICollection<GiaSP> GiaSPs { set; get; }
    }
}