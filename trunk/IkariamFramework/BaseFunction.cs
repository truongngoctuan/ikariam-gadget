using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace IkariamFramework
{
    public class BaseFunction
    {
        static String USER_AGENT = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.15 (KHTML, like Gecko) Chrome/10.0.612.3 Safari/534.15";
        static String CONTENT_TYPE = "application/x-www-form-urlencoded";
        static String ACCEPT = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

        public static Stream GetHtmlSite(HtmlAgilityPack.HtmlDocument Document,
            CookieContainer cookieContainer,
            string url)
        {//http://htmlagilitypack.codeplex.com/Thread/View.aspx?ThreadId=14255
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse;
            // need a cookie container to store cookies
            webRequest.CookieContainer = cookieContainer;
            webRequest.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            webRequest.Method = "GET";
            webRequest.UserAgent = USER_AGENT;
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            webResponse.Cookies = webRequest.CookieContainer.GetCookies(webRequest.RequestUri);
            //Let's show some information about the response
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Document.Load(webResponse.GetResponseStream());
                GloVal.DocumentNode = Document.DocumentNode;
                return webResponse.GetResponseStream();
            }
            return null;
        }

        public static Stream PostGetHtmlSite(HtmlAgilityPack.HtmlDocument Document, 
            CookieContainer cookieContainer, 
            string url, string postData)//, out bool responseOK)
        {//http://htmlagilitypack.codeplex.com/Thread/View.aspx?ThreadId=14255
            string htmlResponse = string.Empty;
            //responseOK = false;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse;
            //Our postvars
            byte[] buffer = Encoding.ASCII.GetBytes(postData);
            // need a cookie container to store cookies
            webRequest.CookieContainer = cookieContainer;
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
                Document.Load(webResponse.GetResponseStream());
                GloVal.DocumentNode = Document.DocumentNode;
                return webResponse.GetResponseStream();
            }
            return null;
        }
    }
}
