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

            if (Database.accInf.Research == null)
            {
                Database.accInf.Research = new DTOResearch();
            }
            HtmlNodeCollection noderesearchinfoCol = Database.DocumentNode.SelectNodes(XPathManager.XPathResearch.ResearchEntry);

            Database.accInf.Research.Seafaring = GetInfoResearchBranch(noderesearchinfoCol[0]);
            Database.accInf.Research.Economic = GetInfoResearchBranch(noderesearchinfoCol[1]);
            Database.accInf.Research.Scientific = GetInfoResearchBranch(noderesearchinfoCol[2]);
            Database.accInf.Research.Militaristic = GetInfoResearchBranch(noderesearchinfoCol[3]);
            
            return Database.accInf.Research;
        }

        static DTOResearchBranch GetInfoResearchBranch(HtmlNode node)
        {
            DTOResearchBranch rb = new DTOResearchBranch();//.1.0
            rb.Name = node.ChildNodes[1].ChildNodes[0].InnerText;
            rb.Name = rb.Name.Trim(new char[] { '\r', '\n', '\t' });

            rb.Description = node.ChildNodes[5].InnerText;

            string strTemp = node.ChildNodes[9].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText;//SelectSingleNode("//ul/li[@class='researchPoints']").InnerText;//.1.9.1.0
            strTemp = strTemp.Replace(",", "");
            rb.Need = int.Parse(strTemp);

            return rb;
        }

        public static DTOResearch GetCurrentResearchScientists()
        {//xem nhu chuyen trang roi`
            ////ul[@class='researchLeftMenu']
            if (Database.accInf.Research == null)
            {
                Database.accInf.Research = new DTOResearch();
            }

            HtmlNode node = Database.DocumentNode.SelectSingleNode(XPathManager.XPathResearch.ResearchPoint);

            string strTemp = "";
            strTemp = node.ChildNodes[1].InnerText.Replace("Scientists: ", "").Replace(",", "");
            Database.accInf.Research.Scientists = int.Parse(strTemp);

            strTemp = node.ChildNodes[3].InnerText.Replace("Research Points: ", "").Replace(",", "");
            Database.accInf.Research.ResearchPoints = long.Parse(strTemp);

            strTemp = node.ChildNodes[5].InnerText.Replace("per Hour: ", "").Replace(",", "");
            Database.accInf.Research.ResearchPointsPerHour = int.Parse(strTemp);


            //Database.accInf.Research = re;
            return Database.accInf.Research;
        }
    }
}
