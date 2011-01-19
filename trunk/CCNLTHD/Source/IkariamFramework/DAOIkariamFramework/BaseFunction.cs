using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;
using IkariamFramework.DebuggingAndTracking;

namespace IkariamFramework.DAOIkariamFramework
{
    public class BaseFunction
    {
        static String USER_AGENT = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.15 (KHTML, like Gecko) Chrome/10.0.612.3 Safari/534.15";
        static String CONTENT_TYPE = "application/x-www-form-urlencoded";
        static String ACCEPT = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

        public static string OldView { get; private set; }

        public static Stream GetHtmlSite(string url)
        {//http://htmlagilitypack.codeplex.com/Thread/View.aspx?ThreadId=14255
            Gloval.NRequestPerTask++;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse;
            // need a cookie container to store cookies
            webRequest.CookieContainer = Gloval.Database.cookieContainer;
            webRequest.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            webRequest.Method = "GET";
            webRequest.UserAgent = USER_AGENT;
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            webResponse.Cookies = webRequest.CookieContainer.GetCookies(webRequest.RequestUri);
            //Let's show some information about the response
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Gloval.Database.Document.Load(webResponse.GetResponseStream(), Encoding.UTF8);
                Gloval.Database.DocumentNode = Gloval.Database.Document.DocumentNode;
                UpdateOldView();

                Debug.Logging(url);
                return webResponse.GetResponseStream();
            }

            Debug.Logging("Status Code: " + webResponse.StatusCode.ToString() + url);
            return null;
        }

        public static Stream PostGetHtmlSite(string url, string postData)//, out bool responseOK)
        {//http://htmlagilitypack.codeplex.com/Thread/View.aspx?ThreadId=14255
            Gloval.NRequestPerTask++;
            //responseOK = false;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse;
            //Our postvars
            byte[] buffer = Encoding.ASCII.GetBytes(postData);
            // need a cookie container to store cookies
            webRequest.CookieContainer = Gloval.Database.cookieContainer;
             webRequest.Referer = "http://en.ikariam.com/";
             webRequest.Accept = ACCEPT;
            //====================================================
            //Our method is post, otherwise the buffer (postvars) would be useless
            webRequest.Method = "POST";
            webRequest.UserAgent = USER_AGENT;
            webRequest.ContentType = CONTENT_TYPE;
            webRequest.ContentLength = 0;
            webRequest.ContentLength = buffer.Length;
            //We open a stream for writing the postvars
            //webRequest.ReadWriteTimeout = 5000;
            Stream StreamPostData = webRequest.GetRequestStream();
            //Now we write, and afterwards, we close. Closing is always important!
            StreamPostData.Write(buffer, 0, buffer.Length);
            StreamPostData.Close();
            //Get the response handle, we have no true response yet!
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            webResponse.Cookies = webRequest.CookieContainer.GetCookies(webRequest.RequestUri);
            //Let's show some information about the response
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Gloval.Database.Document.Load(webResponse.GetResponseStream(), Encoding.UTF8);
                Gloval.Database.DocumentNode = Gloval.Database.Document.DocumentNode;
                UpdateOldView();

                Debug.Logging(url);
                return webResponse.GetResponseStream();
            }

            Debug.Logging("Status Code: " + webResponse.StatusCode.ToString() + url);
            return null;
        }

        public static void GoToLink(string strXpath)
        {
            HtmlNode node = Gloval.Database.Document.DocumentNode.SelectSingleNode(strXpath);
            string strhref = node.GetAttributeValue("href", "err");

            strhref = strhref.Replace("amp;", "");
            BaseFunction.GetHtmlSite(Gloval.Database.WebUrl + strhref);
        }

        public static void UpdateOldView()
        {
            if (Gloval.Database.Authenticated == false)
            {
                return;
            }
            HtmlNode node = Gloval.Database.Document.DocumentNode.SelectSingleNode("//li[@id='advCities']/a");
            string strhref = node.GetAttributeValue("href", "err");

            if (strhref.IndexOf("oldView=") != -1)
            {
                OldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            }
            else
            {
                OldView = "";
            }
            //MessageBox.Show(GloVal.strOldView);
        }

        //phục vụ cho hàm thay đổi thành phố
        public static string GetactionRequest()
        {
            HtmlNode node = Gloval.Database.DocumentNode.SelectSingleNode("//fieldset");

            node = node.ChildNodes[4];
            if (node == null)
            {
                throw new Exception("lỗi: GetactionRequest");
            }
            //MessageBox.Show(node.GetAttributeValue("value", "err"));
            return node.GetAttributeValue("value", "err");
        }


        public static float updateValueHaveLimit(float fValue, float fValueLimit, float fValuePerHour, float fDelta)
        {
            fValue += updateValue(fValuePerHour, fDelta);
            if (fValue > fValueLimit) fValue = fValueLimit;
            return fValue;
        }
        public static float updateValue(float fValuePerHour, float fDelta)
        {
            return fValuePerHour / 3600f * (float)fDelta;
        }
    }
}
