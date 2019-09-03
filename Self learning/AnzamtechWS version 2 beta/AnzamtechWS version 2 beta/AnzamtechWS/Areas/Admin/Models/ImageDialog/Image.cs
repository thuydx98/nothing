using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Image
/// </summary>
/// 
namespace AnzamtechWS.Areas.Admin.Models.ImageDialog
{ 
    public class Image
    {
    

	    public Image()
	    {
            this.FileName = "";
            this.Path = "";
            this.ID = 0;
	    }

        public int ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}