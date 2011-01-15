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
        public static DTOResearch GetInfomation4BrandOfResearch()
        {//xem nhu chuyen trang roi`

            if (Gloval.Database.Account.Research == null)
            {
                Gloval.Database.Account.Research = new DTOResearch();
            }
            HtmlNodeCollection noderesearchinfoCol = Gloval.Database.DocumentNode.SelectNodes(XPathManager.XPathResearch.ResearchEntry);

            Gloval.Database.Account.Research.Seafaring = GetInfoResearchBranch(noderesearchinfoCol[0]);
            Gloval.Database.Account.Research.Economic = GetInfoResearchBranch(noderesearchinfoCol[1]);
            Gloval.Database.Account.Research.Scientific = GetInfoResearchBranch(noderesearchinfoCol[2]);
            Gloval.Database.Account.Research.Militaristic = GetInfoResearchBranch(noderesearchinfoCol[3]);
            
            return Gloval.Database.Account.Research;
        }

        static DTOResearchBranch GetInfoResearchBranch(HtmlNode node)
        {
            DTOResearchBranch rb = new DTOResearchBranch();

            rb.Name = node.ChildNodes[1].ChildNodes[0].InnerText.Trim(new char[] { '\r', '\n', '\t' });
            rb.Description = node.ChildNodes[5].InnerText;
            rb.Need = NodeParser.toInt(node.ChildNodes[9].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText);

            return rb;
        }

        public static DTOResearch GetCurrentResearchScientists()
        {//xem nhu chuyen trang roi`
            ////ul[@class='researchLeftMenu']
            if (Gloval.Database.Account.Research == null)
            {
                Gloval.Database.Account.Research = new DTOResearch();
            }

            HtmlNode node = Gloval.Database.DocumentNode.SelectSingleNode(XPathManager.XPathResearch.ResearchPoint);

            Gloval.Database.Account.Research.Scientists = NodeParser.toInt(node.ChildNodes[1].InnerText);
            Gloval.Database.Account.Research.ResearchPoints = NodeParser.toLong(node.ChildNodes[3].InnerText);
            Gloval.Database.Account.Research.ResearchPointsPerHour = NodeParser.toInt(node.ChildNodes[5].InnerText);

            return Gloval.Database.Account.Research;
        }
    }
}
