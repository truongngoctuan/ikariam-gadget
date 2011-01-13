using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework
{
    /// <summary>
    /// A Gateway for receiving player info
    /// </summary>
    [ComVisible(true)]
    public class Gadget : IDisposable
    {
        public bool Authenticated { get; private set; }
        public Gadget()
        {
            Authenticated = false;
        }

        public int Login(string username, string password, string server)
        {
            // Login : trả về bool, thành công - thất bại
            // Hoặc tra về errorCode
            Account.Login(username, password);
            Authenticated = true;
            return 0;
        }

        public String btadvCities_Click()
        {
            HtmlNode node = GloVal.Document.DocumentNode.SelectNodes("html/body/div/div/div[13]/ul/li/a")[0];
            string strhref = node.GetAttributeValue("href", "err");
            BaseFunction.GetHtmlSite(GloVal.Document, GloVal.cookieContainer,
                "http://s15.en.ikariam.com/index.php" + strhref);
            //cap nhat oldview
            GloVal.strOldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            return GloVal.strOldView;
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
