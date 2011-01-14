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
            public static string logOut = "//li[@class='logout']/a";
        }

        public class XPathCity
        {
            public static string ListCities = "//select[@id='citySelect']";
            public static string cityResources = "/html/body/div/div/div[12]/ul";

            public static string ShowCity = "//li[@class='viewCity']/a";
            public static string ShowIsland = "//li[@class='viewIsland']/a";
            public static string ShowWorld = "//li[@class='viewWorldmap']/a";

            public static string ListBuilding = "//ul[@id='locations']/li";

            public static string ShowTroops = "//div[@id='information']/div/ul/div/a";
            public static string ShowTroopsShips = "//a[@title='Ships']";//"//table[@id='tabz']/tbody/tr/td[2]/a";
            public static string DivTableUnits = "//div[@class='contentBox01h']";
            public static string DivTableShips = "//div[@class='contentBox01h']";
        }

        public class XPathAdvisor
        {
            public static string advCities = "//li[@id='advCities']/a";//"/html/body/div/div/div[11]/ul/li/a";
            public static string advMilitary = "//li[@id='advMilitary']/a";
            public static string advResearch = "//li[@id='advResearch']/a";
            public static string advDiplomacy = "//li[@id='advDiplomacy']/a";
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
            public static string ResearchEntry = "//div[@class='researchInfo']";
            public static string ResearchPoint = "//ul[@class='researchLeftMenu']";
        }

        public class XPathTransport
        {
        }

        public class XPathMessage
        {
            public static string MessageEntry = "//td[@class='msgText']";
        }


    }
}
