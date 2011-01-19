using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOResearch
    {
        #region ParserData
        static DTOResearchBranch GetInfoResearchBranch(HtmlNode node)
        {
            DTOResearchBranch rb = new DTOResearchBranch();

            rb.Name = node.ChildNodes[1].ChildNodes[0].InnerText.Trim(new char[] { '\r', '\n', '\t' });
            rb.Description = node.ChildNodes[5].InnerText;
            //HtmlNode nodeNeed = node.ChildNodes[9].ChildNodes[3].ChildNodes[1].ChildNodes[0];
            HtmlNode nodeNeed = node.SelectSingleNode("./div[@class='costs']/ul/li");
            if (nodeNeed != null)
            {
                rb.Need = NodeParser.toInt(nodeNeed.InnerText);
                rb.NeedDescription = "";
            }
            else
            {
                rb.Need = 0;
                rb.NeedDescription = node.SelectSingleNode("./div[2]").InnerText.Trim(new char[] {'\r', '\n', '\t'});
            }
            
            return rb;
        }

        public static DTOResearch GetResearchInformation()
        {//xem nhu chuyen trang roi`
            DTOResearch rs = new DTOResearch();

            //scientist point
            HtmlNode node = Gloval.Database.DocumentNode.SelectSingleNode(XPathManager.XPathResearch.ResearchPoint);

            rs.Scientists = NodeParser.toInt(node.ChildNodes[1].InnerText);
            rs.ResearchPoints = NodeParser.toLong(node.ChildNodes[3].InnerText);
            rs.ResearchPointsPerHour = NodeParser.toInt(node.ChildNodes[5].InnerText);

            //4 branch
            HtmlNodeCollection noderesearchinfoCol = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathResearch.ResearchEntry);

            rs.Seafaring = GetInfoResearchBranch(noderesearchinfoCol[0]);
            rs.Economic = GetInfoResearchBranch(noderesearchinfoCol[1]);
            rs.Scientific = GetInfoResearchBranch(noderesearchinfoCol[2]);
            rs.Militaristic = GetInfoResearchBranch(noderesearchinfoCol[3]);

            return rs;
        }
        #endregion
    }
}
