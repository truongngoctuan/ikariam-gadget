using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOEvent
    {
        public static DTOEvent[] GetEventEntry()
        {
            DAOAdvisor.GoToadvCities();
            //tradeAdvisor
            HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathEvent.EventsEntry);
            //HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes("//table[@id='inboxCity']/tr");
            List<DTOEvent> events = new List<DTOEvent>();

            //bo cai node cuoi do no la cai chuyen trang
            for (int i = 0; i < nodeCol.Count - 1; i++ )
                //foreach (HtmlNode node in nodeCol)
                {
                    DTOEvent ev = new DTOEvent();

                    if (nodeCol[i].ChildNodes[1].GetAttributeValue("class", "err") == "wichtig")
                    {
                        ev.Type = DTOEvent.TYPE.NEW;
                    }
                    else
                    {
                        ev.Type = DTOEvent.TYPE.OLD;
                    }

                    ev.Town = nodeCol[i].ChildNodes[5].ChildNodes[1].InnerText;
                    ev.Town = ev.Town.Replace("\r\n", " ").Trim();

                    ev.Date = nodeCol[i].ChildNodes[7].InnerText;
                    ev.Message = nodeCol[i].ChildNodes[9].InnerText;

                    events.Add(ev);
                }
            Gloval.Database.Account.Event = events.ToArray();

            return Gloval.Database.Account.Event;
        }
    }
}
