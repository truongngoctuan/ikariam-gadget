using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOAdvisor
    {
        public static void GoToadvCities()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advCities);
            Database.CurrentView = Database.SITE_VIEW.ADVISOR_CITY;
        }

        public static void GoToadvMilitary()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advMilitary);
            Database.CurrentView = Database.SITE_VIEW.ADVISOR_MILITARY;
        }

        public static void GoToadvResearch()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advResearch);
            Database.CurrentView = Database.SITE_VIEW.ADVISOR_RESEARCH;
        }

        public static void GoToadvDiplomacy()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advDiplomacy);
            Database.CurrentView = Database.SITE_VIEW.ADVISOR_DIPLOMACY;
        }
    }
}
