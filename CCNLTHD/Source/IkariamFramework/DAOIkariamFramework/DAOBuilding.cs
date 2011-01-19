using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Windows.Forms;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOBuilding
    {
        #region ParserData
        public static DTOBuilding[] GetBuildingCity()
        {//xem nhu da vao duoc view thanh pho
            HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.ListBuilding);

            List<DTOBuilding> list = new List<DTOBuilding>();
            foreach (HtmlNode node in nodeCol)
            {
                if (!node.GetAttributeValue("id", "err").Contains("position")) continue;

                DTOBuilding building = new DTOBuilding();
                //tên của building --> type tương ứng
                string strTemp = node.GetAttributeValue("class", "err");
                switch (strTemp)
                {
                    case "townHall":
                        building.Type = DTOBuilding.TYPE.Townhall;
                        break;
                    case "museum":
                        building.Type = DTOBuilding.TYPE.Museum;
                        break;
                    case "palace":
                        building.Type = DTOBuilding.TYPE.Palace;
                        break;
                    case "vineyard":
                        building.Type = DTOBuilding.TYPE.Winepress;
                        break;
                    case "tavern":
                        building.Type = DTOBuilding.TYPE.Tavern;
                        break;
                    case "safehouse":
                        building.Type = DTOBuilding.TYPE.Hideout;
                        break;
                    case "architect":
                        building.Type = DTOBuilding.TYPE.Architect;
                        break;
                    case "dump":
                        building.Type = DTOBuilding.TYPE.Dump;
                        break;
                    case "academy":
                        building.Type = DTOBuilding.TYPE.Academy;
                        break;
                    case "forester":
                        building.Type = DTOBuilding.TYPE.Forester;
                        break;
                    case "winegrower":
                        building.Type = DTOBuilding.TYPE.Winegrower;
                        break;
                    case "carpentering":
                        building.Type = DTOBuilding.TYPE.Carpenter;
                        break;
                    case "wall":
                        building.Type = DTOBuilding.TYPE.Townwall;
                        break;
                    case "port":
                        building.Type = DTOBuilding.TYPE.TradingPort;
                        break;
                    case "shipyard":
                        building.Type = DTOBuilding.TYPE.Shipyard;
                        break;
                    case "palaceColony":
                        building.Type = DTOBuilding.TYPE.PalaceColony;
                        break;
                    case "stonemason":
                        building.Type = DTOBuilding.TYPE.Stonemason;
                        break;
                    case "barracks":
                        building.Type = DTOBuilding.TYPE.Barracks;
                        break;
                    case "fireworker":
                        building.Type = DTOBuilding.TYPE.Firework;
                        break;
                    case "warehouse":
                        building.Type = DTOBuilding.TYPE.Warehouse;
                        break;
                    case "temple":
                        building.Type = DTOBuilding.TYPE.Temple;
                        break;
                    case "glassblowing":
                        building.Type = DTOBuilding.TYPE.GlassBlower;
                        break;
                    case "optician":
                        building.Type = DTOBuilding.TYPE.Optician;
                        break;
                    case "alchemist":
                        building.Type = DTOBuilding.TYPE.Alchemist;
                        break;
                    case "embassy":
                        building.Type = DTOBuilding.TYPE.Embassy;
                        break;
                    case "buildingGround land":
                        building.Type = DTOBuilding.TYPE.Flag;
                        break;
                    case "buildingGround shore":
                        building.Type = DTOBuilding.TYPE.Flag;
                        break;
                    case "branchOffice":
                        building.Type = DTOBuilding.TYPE.TradingPost;
                        break;
                    case "workshop":
                        building.Type = DTOBuilding.TYPE.Workshop;
                        break;
                    default:
                        building.Type = DTOBuilding.TYPE.Unknown;
                        break;
                }

                //kiểm tra is bulding
                if ("constructionSite" == node.ChildNodes[1].GetAttributeValue("class", "err"))
                {
                    building.IsBuilding = true;
                    //lấy thời gian xây .5.3
                    building.Time = node.ChildNodes[5].ChildNodes[2].InnerText;
                }
                else
                {
                    building.IsBuilding = false;
                }

                //lấy lvl
                //strTemp = node.ChildNodes[3].GetAttributeValue("title", "err");
                //strTemp = strTemp.Substring(strTemp.LastIndexOf(' '));
                building.Lvl = NodeParser.toInt(node.ChildNodes[3].GetAttributeValue("title", "err"));

                list.Add(building);
            }

            return list.ToArray();
        }
        #endregion
    }
}
