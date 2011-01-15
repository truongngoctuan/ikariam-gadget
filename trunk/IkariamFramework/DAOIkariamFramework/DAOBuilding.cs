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
        public static void GetBuildingCity(int iIndexCity)
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
                        building.Type = DTOBuilding.BUILDING_TYPE.townHall;
                        break;
                    case "museum":
                        building.Type = DTOBuilding.BUILDING_TYPE.museum;
                        break;
                    case "palace":
                        building.Type = DTOBuilding.BUILDING_TYPE.palace;
                        break;
                    case "vineyard":
                        building.Type = DTOBuilding.BUILDING_TYPE.vineyard;
                        break;
                    case "tavern":
                        building.Type = DTOBuilding.BUILDING_TYPE.tavern;
                        break;
                    case "safehouse":
                        building.Type = DTOBuilding.BUILDING_TYPE.safehouse;
                        break;
                    case "architect":
                        building.Type = DTOBuilding.BUILDING_TYPE.architect;
                        break;
                    case "dump":
                        building.Type = DTOBuilding.BUILDING_TYPE.dump;
                        break;
                    case "academy":
                        building.Type = DTOBuilding.BUILDING_TYPE.academy;
                        break;
                    case "forester":
                        building.Type = DTOBuilding.BUILDING_TYPE.forester;
                        break;
                    case "winegrower":
                        building.Type = DTOBuilding.BUILDING_TYPE.winegrower;
                        break;
                    case "carpentering":
                        building.Type = DTOBuilding.BUILDING_TYPE.carpentering;
                        break;
                    case "wall":
                        building.Type = DTOBuilding.BUILDING_TYPE.wall;
                        break;
                    case "port":
                        building.Type = DTOBuilding.BUILDING_TYPE.port;
                        break;
                    case "shipyard":
                        building.Type = DTOBuilding.BUILDING_TYPE.shipyard;
                        break;
                    case "palaceColony":
                        building.Type = DTOBuilding.BUILDING_TYPE.palaceColony;
                        break;
                    case "stonemason":
                        building.Type = DTOBuilding.BUILDING_TYPE.stonemason;
                        break;
                    case "barracks":
                        building.Type = DTOBuilding.BUILDING_TYPE.barracks;
                        break;
                    case "fireworker":
                        building.Type = DTOBuilding.BUILDING_TYPE.fireworker;
                        break;
                    case "warehouse":
                        building.Type = DTOBuilding.BUILDING_TYPE.warehouse;
                        break;
                    case "temple":
                        building.Type = DTOBuilding.BUILDING_TYPE.temple;
                        break;
                    case "glassblowing":
                        building.Type = DTOBuilding.BUILDING_TYPE.glassblowing;
                        break;
                    case "optician":
                        building.Type = DTOBuilding.BUILDING_TYPE.optician;
                        break;
                    case "alchemist":
                        building.Type = DTOBuilding.BUILDING_TYPE.alchemist;
                        break;
                    case "embassy":
                        building.Type = DTOBuilding.BUILDING_TYPE.embassy;
                        break;
                    default:
                        building.Type = DTOBuilding.BUILDING_TYPE.UNKNOW;
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
                strTemp = node.ChildNodes[3].GetAttributeValue("title", "err");
                strTemp = strTemp.Substring(strTemp.LastIndexOf(' '));
                building.Lvl = int.Parse(strTemp);
                
                list.Add(building);
            }

            Gloval.Database.Account.Cities[iIndexCity].ListBuilding = list.ToArray();
        }
    }
}
