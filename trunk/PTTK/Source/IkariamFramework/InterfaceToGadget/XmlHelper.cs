using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IkariamFramework.InterfaceToGadget
{
    public class XmlHelper
    {
        public static Dictionary<string, string> LoadFile(string strFileName,
            string strXpath)
        {
            // Load the XML file.
            XmlDocument dom = new XmlDocument();
            dom.Load(strFileName);

            XmlNodeList oNodes = dom.SelectNodes(strXpath);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (XmlNode node in oNodes)
            {
                dict.Add(node.Attributes["key"].Value,
                 node.Attributes["value"].Value);
            }

            return dict;
        }

        public static Dictionary<string, string> LoadFile(string strFileName)
        {
            return LoadFile(strFileName, "/res/item");
        }
    }
}
