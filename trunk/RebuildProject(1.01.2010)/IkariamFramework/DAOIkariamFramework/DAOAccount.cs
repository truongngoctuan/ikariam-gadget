using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;
using HtmlAgilityPack;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOAccount
    {
        #region request
        #endregion

        #region goToPage
        #endregion

        #region ParserData
        #endregion

        //chưa kiểm tra có login thành công hay không
        public static bool Login(string strUsername, 
            string strPassword,
            string strServerUrl)
        {
            //s15.en.ikariam.com
            Gloval.Database.WebUrl = string.Format("http://{0}/index.php", strServerUrl);

            BaseFunction.PostGetHtmlSite(string.Format("http://{0}/index.php?action=loginAvatar&function=login", strServerUrl),
                string.Format("uni_url={2}&name={0}&password={1}&kid=", 
                    strUsername, 
                    strPassword,
                    strServerUrl));

            Gloval.Database.CurrentView = Data.SITE_VIEW.CITY;

            if (Gloval.Database.DocumentNode.SelectSingleNode("//h1").InnerText == "Error!")
            {
                return false;
            }

            Gloval.Database.Authenticated = true;
            return true;
        }

        public static void Logout()
        {
            Gloval.Database.Authenticated = false;
            BaseFunction.GoToLink(XPathManager.XPathAccount.logOut);
            Gloval.Database.Account = new IkariamFramework.DTOIkariamFramework.DTOAccount();
            Gloval.Database.cookieContainer = new System.Net.CookieContainer();
            Gloval.Database.CurrentView = Data.SITE_VIEW.CITY;
            Gloval.Database.CurrentCity = 0;
            Gloval.Database.WebUrl = "";
        }

        //gold
        public static void GoToGoldPage()
        {
            BaseFunction.GoToLink(XPathManager.XPathAccount.GoldPage);
//            Gloval.Database.CurrentView = Data.SITE_VIEW.GOLD_PAGE;
        }

        //public static void GetTotalGold()
        //{//total gold and total gold per hour
        //    HtmlNode node1 = Gloval.Database.DocumentNode.SelectSingleNode(
        //            XPathManager.XPathAccount.GoldTotal);

        //    Gloval.Database.Account.TotalGold = NodeParser.toUnsignedLong(node1.InnerText);

        //    //----------------
        //    HtmlNode node2 = Gloval.Database.DocumentNode.SelectNodes(
        //            XPathManager.XPathAccount.GoldTotalPerHour).Last();

        //    Gloval.Database.Account.TotalGoldPerHour = NodeParser.toUnsignedLong(node2.InnerText);
        //}

        //adv check
        public static int CheckAdvStatus()
        {
            int iResult = 0;
            iResult |= CheckOneAdvStatus(XPathManager.XPathAdvisor.advCities, DTOAccount.ADV_ACTIVE.MAYOR);
            iResult |= CheckOneAdvStatus(XPathManager.XPathAdvisor.advMilitary, DTOAccount.ADV_ACTIVE.GENERAL);
            iResult |= CheckOneAdvStatus(XPathManager.XPathAdvisor.advResearch, DTOAccount.ADV_ACTIVE.SCIENTIST);
            iResult |= CheckOneAdvStatus(XPathManager.XPathAdvisor.advDiplomacy, DTOAccount.ADV_ACTIVE.DIPLOMAT);

            Gloval.Database.Account.AdvActive = iResult;
            return iResult;
        }

        public static int CheckOneAdvStatus(string strXPath, DTOAccount.ADV_ACTIVE activeType)
        {
            HtmlNode node = Gloval.Database.DocumentNode.SelectSingleNode(
                    strXPath);

            if (node.GetAttributeValue("class", "err").Contains("active"))
            {
                return (int)activeType;
            }

            return 0;
        }
    }
}
