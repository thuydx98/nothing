using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnzamtechWS.Helper;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class DangNhapModel
    {

        ANZEntities db = new ANZEntities();

        public DangNhapModel()
        {

        }

        public string TenDN { get; set;}
        public string Code { get; set; }
        public string MatKhau { get; set; }

        public bool DangNhapHopLe()
        {
            var list = db.NhanViens.Where(u => u.TenDN == TenDN && u.MatKhau == MatKhau);

            if (list.Count()>0)
            {
                return true;
            }
            return false;
        }

    }
}