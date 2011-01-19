using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Windows.Forms;
using IkariamFramework.DTOIkariamFramework;
using System.Runtime.Serialization;

namespace IkariamFramework.DTOIkariamFramework
{
    public class Data 
    {
        private static Data INSTANCE = new Data();

        private Data()
        {
            Authenticated = false;
            CurrentCity = 0;
            Account = new DTOAccount();
        }

        public static Data getInstance() 
        {
            return INSTANCE;
        }

        public static void ResetData()
        {
            INSTANCE = new Data();
        }


        public DTOAccount Account { get; set; } 
        public bool Authenticated { get; set; }

        public HtmlAgilityPack.HtmlDocument Document = new HtmlAgilityPack.HtmlDocument();
        public HtmlAgilityPack.HtmlNode DocumentNode = null;
        public CookieContainer cookieContainer = new CookieContainer();

        public string WebUrl = "http://s15.en.ikariam.com/index.php";

        public string strOldView = "";

        //quản lý hiện đang ở thành phố nào
        public int CurrentCity { get; set; }

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
            TROOPS_SHIPS,
            GOLD_PAGE,
            TOWN_HALL
            //con` nhiều nữa...
        }
        public SITE_VIEW CurrentView { get; set; }
    }
}
