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
        }

        public static void GoToadvMilitary()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advMilitary);
        }

        public static void GoToadvResearch()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advResearch);
        }

        public static void GoToadvDiplomacy()
        {
            BaseFunction.GoToLink(XPathManager.XPathAdvisor.advDiplomacy);
        }
    }
}
