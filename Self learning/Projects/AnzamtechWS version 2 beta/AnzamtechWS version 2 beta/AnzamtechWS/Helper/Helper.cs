using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AnzamtechWS.Helper
{
    public class Helper
    {
        public static int Count = 0;
        public static List<string> ListTest;
        public static string ConvertVN(string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            for (int i = 0; i < chucodau.Length; i++)
            {
                int index2 = FindText.IndexOf(chucodau[i]);
                if (index2 == -1)
                {
                    continue;
                }
                chucodau = chucodau.Replace(chucodau[i], ReplText[index2]);
            }
            return chucodau;
        }

        public static string PrintMenuAdmin(string name)
        {
            string menu = "";
            JObject obj = JObject.Parse(File.ReadAllText(HttpContext.Current.Server.MapPath("/Helper/MenuAdmin.json")));
            var Nodes = obj["Nodes"];
            for (int i = 0; i < Nodes.Count(); i++)
            {
                string strActive = "";
                if (Nodes[i]["Name"].ToString() == name)
                {
                    strActive = "active";
                }
                string menuChild = "";
                if(Nodes[i]["Child"].Count() > 0)
                {
                  
                    
                    var Childs = Nodes[i]["Child"];
                    for (int j = 0; j < Childs.Count(); j++)
                    {
                        string strChildActive= "";
                        if (Childs[j]["Name"].ToString() == name)
                        {
                            strChildActive = "active";
                            strActive = strChildActive;
                            
                        }
                        else
                        {
                            strChildActive = "";
                        }
                        menuChild += "<li class=\"" + strActive + "\"><a href=\"" + Childs[j]["url"] + "\" class=\"waves-effect waves-block " + ((strChildActive != "") ? "toggled active" : "") + "\">";
                        menuChild += Childs[j]["Name"];
                        menuChild += "</li></a>";
                    }
                    menuChild = "<ul class=\"ml-menu\" style=\"" + ((strActive == "") ? "display:none" : "display:block") + "\">" + menuChild;

                    menuChild += "</ul>";
                }
                menu += "<li class=\"" + strActive + "\">";
                menu += "<a href=\"" + ((Nodes[i]["url"].ToString() == "")?"javascript:void(0)": Nodes[i]["url"].ToString()) + "\" class=\"" + ((menuChild != "")?"menu-toggle":"") +  " " + ((strActive != "")?"toggled":"") + " waves-effect waves-block\">";
                menu += "<i class=\"material-icons\">" + Nodes[i]["Icon"] + "</i>";
                menu += "<span>" + Nodes[i]["Name"] + "</span>";
                menu += "</a>";
                menu += menuChild;
                menu += "</li>";
            }
            return menu;
        }
        public static string CalculateMD5Hash(string input)

        {

            
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        private static CultureInfo CultureVN = CultureInfo.GetCultureInfo("vi-VN");
        public static string GetCurrencyVND(double d)
        {
            if ((d == 0))
            {
                return "0đ";
            }
            return d.ToString("#,###", CultureVN.NumberFormat) + "đ";
        }
    }
}