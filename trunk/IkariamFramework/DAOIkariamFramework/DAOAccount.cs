using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            Database.WebUrl = string.Format("http://{0}/index.php", strServerUrl);

            BaseFunction.PostGetHtmlSite(string.Format("http://{0}/index.php?action=loginAvatar&function=login", strServerUrl),
                string.Format("uni_url={2}&name={0}&password={1}&kid=", 
                    strUsername, 
                    strPassword,
                    strServerUrl));

            Database.CurrentView = Database.SITE_VIEW.CITY;

            return true;
        }
    }
}
