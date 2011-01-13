using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework
{
    public class GloVal
    {
        public static HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
        public static HtmlAgilityPack.HtmlNode DocumentNode = null;
        public static CookieContainer cookieContainer = new CookieContainer();
        public static Account accInf = new Account();

        public static string WebUrl = "http://s15.en.ikariam.com/index.php";

        public static string strOldView = "";
        public static void UpdateOldView()
        {
            //HtmlNode node = DocumentNode.SelectSingleNode("//fieldset");
            ////MessageBox.Show(node.InnerHtml);
            //if (node == null)
            //{
            //    throw new Exception("lỗi: UpdateOldView");
            //}
            //strOldView = node.ChildNodes[6].GetAttributeValue("value", "err");
            ////MessageBox.Show(strOldView);

            HtmlNode node = GloVal.Document.DocumentNode.SelectNodes("html/body/div/div/div[13]/ul/li/a")[0];
            string strhref = node.GetAttributeValue("href", "err");
            GloVal.strOldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            //MessageBox.Show(GloVal.strOldView);
        }

        //phục vụ cho hàm thay đổi thành phố
        public static string GetactionRequest()
        {
            HtmlNode node = DocumentNode.SelectSingleNode("//fieldset");

            node = node.ChildNodes[4];
            if (node == null)
            {
                throw new Exception("lỗi: GetactionRequest");
            }
            //MessageBox.Show(node.GetAttributeValue("value", "err"));
            return node.GetAttributeValue("value", "err");
        }

        //quản lý hiện đang ở thành phố nào
        public static int iCurrentCity = 0;
    }
}
