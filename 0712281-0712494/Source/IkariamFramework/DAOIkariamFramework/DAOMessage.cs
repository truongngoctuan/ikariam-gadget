using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOMessage
    {
        public static DTOMessage[] Get10LastMessage()
        {//xem nhu chuyen trang roi`

            HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathMessage.MessageEntry);

            List<DTOMessage> list = new List<DTOMessage>();
            foreach (HtmlNode node in nodeCol)
            {
                DTOMessage mess = new DTOMessage();
                mess.Message = node.ChildNodes[1].InnerText;
                list.Add(mess);
            }

            Gloval.Database.Account.Message = list.ToArray();

            return Gloval.Database.Account.Message;
        }
    }
}
