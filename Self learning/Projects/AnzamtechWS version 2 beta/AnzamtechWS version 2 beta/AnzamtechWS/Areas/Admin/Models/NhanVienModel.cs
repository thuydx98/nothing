using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class NhanVienModel
    {
        public NhanVienModel()
        {
            sc = new ANZEntities();
            Manv = 0;
            Code = "";
            Tendn = "";
            Matkhau = "";
        }
        private ANZEntities sc;

        public int Manv { get; set; }
        public string Code { get; set; }
        public string Tendn { get; set; }
        public string Matkhau { get; set; }

        public NhanVien GetNhanVienByID()
        {
            return sc.NhanViens.Where(sp => sp.MaNV == Manv).ToList<NhanVien>().FirstOrDefault<NhanVien>();
        }
        public List<BaiViet> ListBaiViets()
        {

            return sc.BaiViets.Where(bv => bv.MaNV == Manv).ToList<BaiViet>();
        }
        public void Add()
        {
            NhanVien nv = new NhanVien()
            {
                Code = this.Code,
                TenDN=this.Tendn,
                MatKhau=this.Matkhau
            };
            sc.NhanViens.Add(nv);
            sc.SaveChanges();
            this.Manv = nv.MaNV;
        }
    }
}