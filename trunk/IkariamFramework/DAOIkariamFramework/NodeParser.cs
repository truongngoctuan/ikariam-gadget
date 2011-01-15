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
        #region Support Method
        static string GetDictValue(string strKey)
        {
            string strout = "";
            Gloval.Dict.TryGetValue(strKey, out strout);
            return strout;
        }

        static bool IsDigit(char c)
        {
            if (48 <= c && c < 58) return true;
            return false;
        }

        public static int toInt(string str)
        {
            //bỏ các kí tự chữ
            char[] arr_c = str.ToCharArray();
            StringBuilder sb = new StringBuilder(15);
            sb.Append('0');
            for (int i = 0; i < str.Length; i++)
            {
                if (IsDigit(arr_c[i])) sb.Append(arr_c[i]);
            }

            return int.Parse(sb.ToString());
        }

        public static long toLong(string str)
        {
            //bỏ các kí tự chữ
            char[] arr_c = str.ToCharArray();
            StringBuilder sb = new StringBuilder(15);
            for (int i = 0; i < str.Length; i++)
            {
                if (IsDigit(arr_c[i])) sb.Append(arr_c[i]);
            }

            return long.Parse(sb.ToString());
        }
        #endregion

        #region City
        public static DTOCity toCityBasicInfo(HtmlNode node)
        {
            DTOCity ct = new DTOCity();
            ct.ID = node.GetAttributeValue("value", 0);

            string strInnerText = node.NextSibling.InnerText;
            ct.X = int.Parse(strInnerText.Substring(1, 2));
            ct.Y = int.Parse(strInnerText.Substring(4, 2));

            ct.Name = strInnerText.Substring(13, strInnerText.Length - 13);

            //Trade good: Marble
            //Hàng giao dịch: Lưu huỳnh
            string strType = node.GetAttributeValue("title", "err");
            if (strType.EndsWith(GetDictValue("Wine")))
            {
                ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.WINE;
                return ct;
            }

            if (strType.EndsWith(GetDictValue("Marble")))
            {
                ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.MARBLE;
                return ct;
            }

            if (strType.EndsWith(GetDictValue("Crystal Glass")))
            {
                ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.CRYSTAL;
                return ct;
            }

            if (strType.EndsWith(GetDictValue("Sulphur")))
            {
                ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.SULPHUR;
                return ct;
            }

            return ct;
        }
        #endregion
    }
}
