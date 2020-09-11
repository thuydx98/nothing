using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnzamtechWS.Areas.Admin.Models
{
    public class TagModel:ModelBase
    {
        public TagModel()
        {
            context = new ANZEntities();
            TagID = 0;
            TenTag = "";
        }
        private ANZEntities context;
        public int TagID { set; get; }
        public string TenTag { set; get; }
        public void AddTag()
        {
            try
            {
                Tag t = new Tag()
                {
                    TagName = TenTag
                };
                context.Tags.Add(t);
                context.SaveChanges();
                TagID = t.MaTag;
                Error = ErrorStatus.Message;
                Message = "Thêm Tag thành công";
            }
            catch (Exception)
            {
                Error = ErrorStatus.DatabaseError;
                Message = "Lôi xảy ra khi thêm Tag";
            }
        }

        public List<Tag> GetTags()
        {
            return context.Tags.ToList<Tag>();
        }

    }
}