using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework
{
    public class Account
    {
        City[] _Cities = null;

        public City[] Cities
        {
            get { return _Cities; }
            set { _Cities = value; }
        }
        
        public static void Login(string strUsername, string strPassword)
        {
            BaseFunction.PostGetHtmlSite(GloVal.Document, GloVal.cookieContainer,
                "http://s15.en.ikariam.com/index.php?action=loginAvatar&function=login",
                string.Format("uni_url=s15.en.ikariam.com&name={0}&password={1}&kid=", 
                    strUsername, 
                    strPassword));
        }
    }
}
