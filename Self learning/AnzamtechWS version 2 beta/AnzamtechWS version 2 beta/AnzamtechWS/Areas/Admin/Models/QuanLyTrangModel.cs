using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class QuanLyTrangModel
    {
        public QuanLyTrangModel()
        {
            Context = new ANZEntities();
        }
        private ANZEntities Context;
        public string MoTaCongTy
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("MoTaCty")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("MoTaCty")).FirstOrDefault<ThongSo>().Value;
            }

        }

        public DanhMucAnh DMALogo
        {
            get
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("Logo")).FirstOrDefault<ThongSo>();
                return Context.DanhMucAnhs.Where(u=>u.TenDM.Contains(ts.Value)).FirstOrDefault<DanhMucAnh>();
            }
        }
        public DanhMucAnh DMABanner {
            get
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("Banner")).FirstOrDefault<ThongSo>();
                return Context.DanhMucAnhs.Where(u => u.TenDM.Contains(ts.Value)).FirstOrDefault<DanhMucAnh>();
            }
        }
        
        [AllowHtml]
        public string GioiThieuCongTy
        {
            set {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("GioiThieuCty")).FirstOrDefault<ThongSo>();
                ts.Text = HttpContext.Current.Server.HtmlEncode(value);
            }
            get
            {
                return HttpContext.Current.Server.HtmlDecode(Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("GioiThieuCty")).FirstOrDefault<ThongSo>().Text);
            }
        }

        public string LinkFB
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkFB")).FirstOrDefault<ThongSo>();
                ts.Value = HttpContext.Current.Server.HtmlEncode(value);
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkFB")).FirstOrDefault<ThongSo>().Value;
            }
        }

        public string LinkGPlus
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkGPlus")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkGPlus")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string LinkTweeter
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkTweeter")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkTweeter")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string LinkInstagram
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkInstagram")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("LinkInstagram")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string DTNVSale
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DTNVSale")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DTNVSale")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string DTNNKyThuat
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DTNNKyThuat")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DTNNKyThuat")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string EmailSale
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailSale")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailSale")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string EmailKyThuat
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailKyThuat")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailKyThuat")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public string ViTri {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("ViTri")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("ViTri")).FirstOrDefault<ThongSo>().Value;
            }
        }

        public string DTCoDinh {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DienThoaiCoDinh")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DienThoaiCoDinh")).FirstOrDefault<ThongSo>().Value;
            }
        }

        public string DTDiDong
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DienThoaiMobile")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DienThoaiMobile")).FirstOrDefault<ThongSo>().Value;
            }
        }

        public string DiaChiCty
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DiaChiCty")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("DiaChiCty")).FirstOrDefault<ThongSo>().Value;
            }
        }

        public string EmailCty
        {
            set
            {
                var ts = Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailCty")).FirstOrDefault<ThongSo>();
                ts.Value = value;
            }
            get
            {
                return Context.ThongSoes.Where<ThongSo>(u => u.TenTS.Contains("EmailCty")).FirstOrDefault<ThongSo>().Value;
            }
        }
        public void Update()
        {
            Context.SaveChanges();
        }
    }
}