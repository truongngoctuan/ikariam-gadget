using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework
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

        public class XPathEvent
        {
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
