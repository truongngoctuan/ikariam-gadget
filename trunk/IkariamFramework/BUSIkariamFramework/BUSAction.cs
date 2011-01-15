using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IkariamFramework.DAOIkariamFramework;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.BUSIkariamFramework
{
    [ComVisible(true)]
    public class BUSAction: IDisposable
    {
        public BUSAction()
        {
        }

        public static string InnerHTML()
        {
            return Gloval.Database.DocumentNode.InnerHtml;
        }

        public static string InnerText()
        {
            return Gloval.Database.DocumentNode.InnerText;
        }

        public static int Login(string username, string password, string server)
        {
            // Login : trả về bool, thành công - thất bại
            // Hoặc tra về errorCode
            if (DAOAccount.Login(username, password, server))
            {
                return 0;
            }
            return 1;
        }

        public static void Logout()
        {
            DAOAccount.Logout();
        }

        public String btadvCities_Click()
        {
            HtmlNode node = Gloval.Database.Document.DocumentNode.SelectNodes("html/body/div/div/div[13]/ul/li/a")[0];
            string strhref = node.GetAttributeValue("href", "err");
            BaseFunction.GetHtmlSite("http://s15.en.ikariam.com/index.php" + strhref);
            //cap nhat oldview
            Gloval.Database.strOldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            return Gloval.Database.strOldView;
        }

        public string GetErrorMessage(int errMessageCode)
        {
            string errorMessage = null;
            if (errors.TryGetValue(errMessageCode, out errorMessage))
                return errorMessage;
            return null;
        }

        Dictionary<int, string> errors = new Dictionary<int, string>
        {
            {0, "No errors"},
            {1, "Unknown errors"},
            {2, "Username or password incorrect"},
            {3, "Connection timeout"},
            {4, "Username and password can't be empty"}
        };

        #region IDisposable Members

        public void Dispose()
        {
            // Nothing to do here.
        }

        #endregion
    }
}
