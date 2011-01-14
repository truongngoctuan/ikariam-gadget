using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Windows.Forms;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class Database
    {
        public static DTOAccount accInf = new DTOAccount();
        public static bool Authenticated { get; private set; }

        public static HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
        public static HtmlAgilityPack.HtmlNode DocumentNode = null;
        public static CookieContainer cookieContainer = new CookieContainer();

        public static string WebUrl = "http://s15.en.ikariam.com/index.php";

        public static string strOldView = "";

        private Database()
        {
            Authenticated = false;
        }
        public static void UpdateOldView()
        {
            HtmlNode node = Database.Document.DocumentNode.SelectSingleNode("//li[@id='advCities']/a");
            string strhref = node.GetAttributeValue("href", "err");

            if (strhref.IndexOf("oldView=") != -1)
            {
                Database.strOldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            }
            else
            {
                Database.strOldView = "";
            }
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

        //quản lý hiện đang ở view nào
        public enum SITE_VIEW
        {
            CITY,
            ISLAND,
            WORLD,
            ADVISOR_CITY,
            ADVISOR_MILITARY,
            ADVISOR_RESEARCH,
            ADVISOR_DIPLOMACY,
            TROOPS,
            TROOPS_SHIPS
            //con` nhiều nữa...
        }
        public static SITE_VIEW CurrentView { get; set; }
    }
}
