using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class DanhMucBaiVietModel:ModelBase
    {
        public DanhMucBaiVietModel()
        {
            sc = new ANZEntities();
            MaDMBV = 0;
            MaLBV = 0;
            TenDanhMuc = "";
        }
        public DanhMucBaiVietModel(ANZEntities sc)
        {
            this.sc = sc;
            MaDMBV = 0;
            MaLBV = 0;
            TenDanhMuc = "";
        }
        private ANZEntities sc;
        public int MaDMBV { get; set; }
        public int MaLBV { get; set; }
        public string TenDanhMuc { get; set; }

        public void Xoa()
        {
            var dmbv = sc.DanhMucBaiViets.FirstOrDefault<DanhMucBaiViet>(u => u.MaDMBV == this.MaDMBV);
            sc.DanhMucBaiViets.Remove(dmbv);
            sc.SaveChanges();
        }
        public DanhMucBaiViet GetDanhMucBaiVietByID()
        {
            return sc.DanhMucBaiViets.Where(dmbv=>dmbv.MaDMBV == MaDMBV).ToList<DanhMucBaiViet>().FirstOrDefault<DanhMucBaiViet>();

        }
        public List<BaiViet> BaiViets()
        {

            return sc.BaiViets.Where(bv=>bv.MaDMBV == MaDMBV).ToList<BaiViet>();
        }
        public void Add()
        {
            try
            {
                DanhMucBaiViet dmbv = new DanhMucBaiViet
                {
                    TenDanhMuc = this.TenDanhMuc
                };
                sc.DanhMucBaiViets.Add(dmbv);
                sc.SaveChanges();
                this.MaDMBV = dmbv.MaDMBV;
                Error = ErrorStatus.Message;
                Message = "Thêm danh mục bài viết thành công";
            }
            catch (Exception)
            {
                Error = ErrorStatus.DatabaseError;
                Message = "Lỗi xảy ra khi thêm danh mục bài viết";
            }
        }

    }
}