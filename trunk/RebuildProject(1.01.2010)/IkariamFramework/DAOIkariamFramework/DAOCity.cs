using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using System.Windows.Forms;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOCity
    {
        #region request
        public static void requestChangeCityTo(int DesID)
        {
            BaseFunction.PostGetHtmlSite(Gloval.Database.WebUrl,
                string.Format("action=header&function=changeCurrentCity&actionRequest={0}&oldView={1}&cityId={2}",
                    BaseFunction.GetactionRequest(), BaseFunction.OldView, DesID));

            //Gloval.Database.CurrentCity = iIndexCity;
        }

        public static void GoToCity()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowCity);
        }
        public static void GoToIsland()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowIsland);
        }
        public static void GoToWorld()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowWorld);
        }
        public static void GoToTownHall()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowTownHall);
        }
        #endregion

        #region ParserData
        public static DTOCity[] ParseCity(HtmlNode DocumentNode)
        {//chỉ những thành phố của người chơi
            //nếu muốn lấy những thành phố khác thì phải chỉnh trogn xpath
            HtmlNodeCollection listCities = DocumentNode.SelectNodes(
                XPathManager.XPathCity.ListCities);

            //List<DTOCity> arrCity = new List<DTOCity>();
            //foreach (HtmlNode nodeCity in listCities)
            DTOCity[] arrCity = new DTOCity[listCities.Count];
            for (int i = 0; i < arrCity.Count(); i++)
            {
                arrCity[i] = NodeParser.toCityBasicInfo(listCities[i]);
            }

            return arrCity;
        }

        //cap nhat dan so
        //action point
        //4 loai tai nguyen cua tp
        public static DTOCity ParseResources(DTOCity ct)
        {
            HtmlNodeCollection nodeLi = Gloval.Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.cityResources);

            foreach (HtmlNode nodeLiChild in nodeLi)
            {
                switch (nodeLiChild.GetAttributeValue("class", "err"))
                {
                    case "population":
                        {//population: 966 (1,627)
                            string str = nodeLiChild.InnerText;
                            string strFreePo = str.Substring(0, str.IndexOf('(') - 1);
                            ct.FreePopulation = NodeParser.toInt(strFreePo);

                            string strPo = str.Substring(str.IndexOf('(') + 1);
                            ct.Population = NodeParser.toInt(strPo);
                            break;
                        }
                    case "actions":
                        {
                            ct.ActionPoint = NodeParser.toInt(nodeLiChild.InnerText);
                            break;
                        }
                    case "wood":
                        {
                            HtmlNode node = nodeLiChild.SelectSingleNode("./span[@id='value_wood']");
                            if (node == null) break;
                            ct.Wood = NodeParser.toInt(node.InnerText);

                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                ct.WoodLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            ct.WoodPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            ct.WoodLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "wine":
                        {
                            ct.Wine = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_wine']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                ct.WineLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            ct.WinePerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            ct.WineLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "marble":
                        {
                            ct.Marble = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_marble']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                ct.MarbleLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            ct.MarblePerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            ct.MarbleLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "glass":
                        {
                            ct.Crystal = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_crystal']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                ct.CrystalLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }

                            ct.CrystalPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            ct.CrystalLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "sulfur":
                        {
                            ct.Sulphur = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_sulfur']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                ct.SulphurLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            ct.SulphurPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            ct.SulphurLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                }
            }
            return ct;
        }

        public static void GetTownHallInfomation(int iIndex,
            out long PopulationLimit,
            out float PopulationGrow,
            out long GoldPerHour,
            out int ResearchPointPerHour)
        {//xem nhu da o trong trang townhall
            //total gold and total gold per hour
            HtmlNode node1 = Gloval.Database.DocumentNode.SelectSingleNode(
                    XPathManager.XPathCity.PopulationLimit);

            PopulationLimit = NodeParser.toLong(node1.InnerText);

            //----------------
            HtmlNode node2 = Gloval.Database.DocumentNode.SelectSingleNode(
                    XPathManager.XPathCity.PopulationGrow);

            PopulationGrow = float.Parse(node2.InnerText.Split(' ')[0]);

            //net gold
            HtmlNode node3 = Gloval.Database.DocumentNode.SelectSingleNode(
        XPathManager.XPathCity.NetGold);

            GoldPerHour = NodeParser.toUnsignedLong(node3.InnerText);

            //scientist point per hour
            HtmlNode node4 = Gloval.Database.DocumentNode.SelectSingleNode(
XPathManager.XPathCity.ScientistPointPerHour);

            ResearchPointPerHour = NodeParser.toInt(node4.NextSibling.InnerText);
        }
        #endregion
    }
}
