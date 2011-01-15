using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOAccount
    {
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
    }
}
