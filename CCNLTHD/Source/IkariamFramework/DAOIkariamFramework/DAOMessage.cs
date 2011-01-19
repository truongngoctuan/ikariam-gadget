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
        #region ParserData
        public static DTOMessage[] Get10LastMessage()
        {//xem nhu chuyen trang roi`

            HtmlNodeCollection nodeColMessage = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathMessage.MessageEntry);
            HtmlNodeCollection nodeColSender = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathMessage.MessageSender);

            DTOMessage[] list;
            if (nodeColMessage == null)
            {
                //khong co tin nhan nao
                list = new DTOMessage[1] {new DTOMessage{Message = "don't have any message", Sender = ""}};
                return list;
            }

            list = new DTOMessage[nodeColSender.Count];

            for (int i = nodeColSender.Count - 1; i >= 0 ; i--)
            {
                DTOMessage mes = new DTOMessage();

                //khử nhiễu                
                string[] strSplit = nodeColMessage[i].InnerText.Split(new char[] {'\r', '\n', '\t'});

                StringBuilder strResultMessage = new StringBuilder(500);
                foreach (string str in strSplit)
                {
                    if (str.Trim() != "")
                    {
                        strResultMessage.Append(str);
                        strResultMessage.Append("<br/>");
                    }
                }

                mes.Message = strResultMessage.ToString();
                mes.Sender = nodeColSender[i].InnerText.Trim(new char[] {'\t', '\r', '\n', ' '});

                list[i] = mes;
            }

            return list;
        }
        #endregion
    }
}
