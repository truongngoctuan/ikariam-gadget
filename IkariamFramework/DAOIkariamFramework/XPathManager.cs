using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework.DAOIkariamFramework
{
    public class XPathManager
    {
        //dùng khi gửi POST
        public static string actionRequest = "/html/body/div/div/div[8]/form/fieldset/input[3]";
        public static string oldView = "/html/body/div/div/div[8]/form/fieldset/input[4]";
        
        public class XPathAccount
        {
        }

        public class XPathCity
        {
            public static string ListCities = "//select[@id='citySelect']";
            public static string cityResources = "/html/body/div/div/div[12]/ul";
        }

        public class XPathAdvisor
        {
            public static string advCities = "//li[@id='advCities']/a";//"/html/body/div/div/div[11]/ul/li/a";
            public static string advMilitary = "/html/body/div/div/div[11]/ul/li[2]/a";
            public static string advResearch = "/html/body/div/div/div[11]/ul/li[3]/a";
            public static string advDiplomacy = "/html/body/div/div/div[11]/ul/li[4]/a";
        }

        public class XPathEvent
        {
            public static string EventsEntry = "//table[@id='inboxCity']/tbody/tr";
        }

        public class XPathMilitary
        {
        }

        public class XPathResearch
        {
        }

        public class XPathTransport
        {
        }
    }
}
