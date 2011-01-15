using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class NodeParser
    {
        public static DTOCity toCityBasicInfo(HtmlNode node)
        {
            DTOCity ct = new DTOCity();
            ct.ID = node.GetAttributeValue("value", 0);

            string strInnerText = node.NextSibling.InnerText;
            ct.X = int.Parse(strInnerText.Substring(1, 2));
            ct.Y = int.Parse(strInnerText.Substring(4, 2));

            ct.Name = strInnerText.Substring(13, strInnerText.Length - 13);

            //Trade good: Marble
            string strType = node.GetAttributeValue("title", "err");
            strType = strType.Substring(12);
            switch (strType)
            {
                case "Wine":
                    {
                        ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.WINE;
                        break;
                    }
                case "Marble":
                    {
                        ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.MARBLE;
                        break;
                    }
                case "Crystal Glass":
                    {
                        ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.CRYSTAL;
                        break;
                    }
                case "Sulphur":
                    {
                        ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.SULPHUR;
                        break;
                    }
            }

            return ct;
        }
    }
}
