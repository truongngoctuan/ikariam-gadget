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
        #region Method
        public static DTOCity[] GetCities()
        {//chỉ những thành phố của người chơi
            //nếu muốn lấy những thành phố khác thì phải chỉnh trogn xpath
            if (Gloval.Database.Account.Cities == null)
            {
                HtmlNodeCollection listCities = Gloval.Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.ListCities);

                List<DTOCity> arrCity = new List<DTOCity>();
                foreach (HtmlNode nodeCity in listCities)
                {
                    {
                        DTOCity ct = NodeParser.toCityBasicInfo(nodeCity);
                        arrCity.Add(ct);
                        //MessageBox.Show(ct.ID.ToString()
                        //    + ct.X.ToString()
                        //    + ct.Y.ToString()
                        //    + ct.Name.ToString());
                    }
                }

                Gloval.Database.Account.Cities = arrCity.ToArray();
            }
            return Gloval.Database.Account.Cities;
        }

        public static void ChangeCity(int iIndexCity)
        {//action=header&function=changeCurrentCity&actionRequest=18210ea9a1d639a30f458267cdcafcb4&oldView=city&cityId=27916
            //GetCities();
            
            int DesID = Gloval.Database.Account.Cities[iIndexCity].ID;
            //if (DesID == Gloval.Database.iCurrentCity)
            //{//nếu là city hiện tại thì không cần thay đổi
            //    return;
            //}

            BaseFunction.PostGetHtmlSite(Gloval.Database.WebUrl,
                string.Format("action=header&function=changeCurrentCity&actionRequest={0}&oldView={1}&cityId={2}",
                    BaseFunction.GetactionRequest(), Gloval.Database.strOldView, DesID));

            Gloval.Database.CurrentCity = iIndexCity;
        }

        //cap nhat dan so
        //action point
        //4 loai tai nguyen cua tp
        public static void UpdateResourceCity(int iIndexCity)
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
                            Gloval.Database.Account.Cities[iIndexCity].FreePopulation = NodeParser.toInt(strFreePo);

                            string strPo = str.Substring(str.IndexOf('(') + 1);
                            Gloval.Database.Account.Cities[iIndexCity].Population = NodeParser.toInt(strPo);
                            break;
                        }
                    case "actions":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].ActionPoint = NodeParser.toInt(nodeLiChild.InnerText);
                            break;
                        }
                    case "wood":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Wood = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_wood']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                Gloval.Database.Account.Cities[iIndexCity].WoodLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            Gloval.Database.Account.Cities[iIndexCity].WoodPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            Gloval.Database.Account.Cities[iIndexCity].WoodLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "wine":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Wine = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_wine']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                Gloval.Database.Account.Cities[iIndexCity].WineLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            Gloval.Database.Account.Cities[iIndexCity].WinePerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            Gloval.Database.Account.Cities[iIndexCity].WineLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "marble":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Marble = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_marble']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                Gloval.Database.Account.Cities[iIndexCity].MarbleLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            Gloval.Database.Account.Cities[iIndexCity].MarblePerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            Gloval.Database.Account.Cities[iIndexCity].MarbleLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "glass":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Crystal = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_crystal']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                Gloval.Database.Account.Cities[iIndexCity].CrystalLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }

                            Gloval.Database.Account.Cities[iIndexCity].CrystalPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            Gloval.Database.Account.Cities[iIndexCity].CrystalLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                    case "sulfur":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Sulphur = NodeParser.toInt(nodeLiChild.SelectSingleNode("./span[@id='value_sulfur']").InnerText);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3)
                            {
                                Gloval.Database.Account.Cities[iIndexCity].SulphurLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                                break;
                            }
                            Gloval.Database.Account.Cities[iIndexCity].SulphurPerHour = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[1]").NextSibling.InnerText);
                            Gloval.Database.Account.Cities[iIndexCity].SulphurLimit = NodeParser.toInt(nodeLiChild.SelectSingleNode("./div/span[2]").NextSibling.InnerText);
                            break;
                        }
                }
            }

            Gloval.Database.Account.Cities[iIndexCity].IsUpdatedResource = true;

            //MessageBox.Show(Gloval.Database.accInf.Cities[iIndexCity].Population.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].PopulationLimit.ToString()
            //    + "/ " + Gloval.Database.accInf.Cities[iIndexCity].ActionPoint.ToString()
            //+ "/ " + Gloval.Database.accInf.Cities[iIndexCity].Wood.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].WoodPerHour.ToString()
            //+ "/ " + Gloval.Database.accInf.Cities[iIndexCity].Wine.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].WinePerHour.ToString()
            //+ "/ " + Gloval.Database.accInf.Cities[iIndexCity].Marble.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].MarblePerHour.ToString()
            //+ "/ " + Gloval.Database.accInf.Cities[iIndexCity].Crystal.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].CrystalPerHour.ToString()
            //+ "/ " + Gloval.Database.accInf.Cities[iIndexCity].Sulphur.ToString() + " " + Gloval.Database.accInf.Cities[iIndexCity].SulphurPerHour.ToString());
        }

        public static void GoToCity()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowCity);
            Gloval.Database.CurrentView = Data.SITE_VIEW.CITY;
        }
        public static void GoToIsland()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowIsland);
            Gloval.Database.CurrentView = Data.SITE_VIEW.ISLAND;
        }
        public static void GoToWorld()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowWorld);
            Gloval.Database.CurrentView = Data.SITE_VIEW.WORLD;
        }

        public static void GetTotalGold(int iIndex)
        {//xem nhu da o trong trang townhall
        }
        #endregion
    }
}
