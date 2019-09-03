using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class DanhMucConModel:ModelBase
    {
        public DanhMucConModel()
        {
            sc = new ANZEntities();
            MaDMC = 0;
            MaDM = 0;
            TenDMC = "";
            Tag = "";
        }
        private ANZEntities sc;
        
        public int MaDMC { get; set; }
        [Required(ErrorMessage ="Danh mục sản phẩm không được trống")]
        public int MaDM { get; set; }
        [Required(ErrorMessage = "Tên danh mục con không được trống")]
        public string TenDMC { get; set; }
        public string Tag { get; set; }
        

        public void GetDAnhMucConByID()
        {
            DanhMucCon dm = (from r in sc.DanhMucCons
                          where r.MaDMC == this.MaDMC select r).FirstOrDefault<DanhMucCon>();
            this.MaDM = dm.MaDM;
            this.TenDMC = dm.TenDMC;
            this.MaDMC = dm.MaDMC;
            this.Tag = dm.Tag;

        }

        public dynamic GetDanhMucs()
        {
            return sc.DanhMucs.Select(m => new { MaDM = m.MaDM, TenDM = m.TenDanhMuc });
        }

        public void Update()
        {
            DanhMucCon dm = (from r in sc.DanhMucCons
                             where r.MaDMC == this.MaDMC
                             select r).FirstOrDefault<DanhMucCon>();
            dm.TenDMC = this.TenDMC;
            dm.MaDM = this.MaDM;
            dm.Tag = this.Tag;
            sc.SaveChanges();
        }

        public dynamic GetDanhMucCons()
        {
            return sc.DanhMucCons.Where(m => m.MaDM == this.MaDM).Select(m => new { MaDMC = m.MaDMC, TenDMC = m.TenDMC});
        }
        public void ThemDMC()
        {
            try
            {

                DanhMucCon dmc = new DanhMucCon()
                {
                    MaDM = this.MaDM,
                    TenDMC = this.TenDMC,
                    Tag = this.Tag
                };
                sc.DanhMucCons.Add(dmc);
                sc.SaveChanges();
                this.MaDM = dmc.MaDM;
                this.Error = ErrorStatus.Message;
                this.Message = "Thêm danh mục con thành công";
                Disposed();
            }catch (Exception)
            {
                this.Error = ErrorStatus.DatabaseError;
                this.Message = "Xảy ra lỗi trong quá trình thêm danh mục con";
            }
           
        }
        public void Disposed()
        {
            MaDMC = 0;
            MaDM = 0;
            TenDMC = "";
            Tag = "";
        }

    }
}