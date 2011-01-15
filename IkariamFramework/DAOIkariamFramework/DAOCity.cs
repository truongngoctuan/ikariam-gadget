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
            //cityResources
            HtmlNode node = Gloval.Database.DocumentNode.SelectSingleNode(
                    XPathManager.XPathCity.cityResources);
            //MessageBox.Show(node.InnerHtml);

            HtmlNodeCollection nodeLi = node.ChildNodes;
            foreach (HtmlNode nodeLiChild in nodeLi)
            {
                switch (nodeLiChild.GetAttributeValue("class", "err"))
                {
                    case "population":
                        {
                            string strpolulation = "Population:  ";
                            string str = nodeLiChild.InnerText;
                            string strPo = str.Substring(strpolulation.Length, str.IndexOf('(') - 1 - strpolulation.Length);
                            strPo = strPo.Replace(",", "");
                            Gloval.Database.Account.Cities[iIndexCity].Population = int.Parse(strPo);

                            string strPoLimit = str.Substring(str.IndexOf('(') + 1, str.IndexOf(')') - 1 - str.IndexOf('('));
                            strPoLimit = strPoLimit.Replace(",", "");

                            Gloval.Database.Account.Cities[iIndexCity].PopulationLimit = int.Parse(strPoLimit);
                            
                            break;
                        }
                    case "actions":
                        {
                            string strTemp = "Action Points: ";
                            string strActionPoint = nodeLiChild.InnerText.Replace(strTemp, "");
                            Gloval.Database.Account.Cities[iIndexCity].ActionPoint = int.Parse(strActionPoint);
                            break;
                        }
                    case "wood":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Wood = GetResource(nodeLiChild);
                            Gloval.Database.Account.Cities[iIndexCity].WoodPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "wine":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Wine = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Gloval.Database.Account.Cities[iIndexCity].WinePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "marble":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Marble = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Gloval.Database.Account.Cities[iIndexCity].MarblePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "glass":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Crystal = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Gloval.Database.Account.Cities[iIndexCity].CrystalPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "sulfur":
                        {
                            Gloval.Database.Account.Cities[iIndexCity].Sulphur = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Gloval.Database.Account.Cities[iIndexCity].SulphurPerHour = GetResourcePerHour(nodeLiChild);
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

        static int GetResource(HtmlNode node)
        {
            string strRes = node.ChildNodes[3].InnerText;
            strRes = strRes.Replace(",", "");
            return int.Parse(strRes);
        }

        static int GetResourcePerHour(HtmlNode node)
        {
            //perhour 5 - 2
            string strRes;strRes = node.ChildNodes[5].ChildNodes[2].InnerText;
            strRes = strRes.Replace("\n", "");
            strRes = strRes.Replace(",", "");
            strRes = strRes.Trim();
            return int.Parse(strRes);
        }

        //cap nhat lvl, type cac nhà trong thành phố
        //public static void GetBuildingCity(int iIndexCity)
        //{//xem nhu da vao duoc view thanh pho
        //    HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(
        //            XPathManager.XPathCity.ListBuilding);

        //    foreach (HtmlNode node in nodeCol)
        //    {
        //        if (node.GetAttributeValue("class", "err") == "coords")
        //        {
        //            //DTOCity ct = new DTOCity();
        //            //ct.ID = nodeCity.GetAttributeValue("value", 0);
        //        }
        //    }
        //}

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
        #endregion
    }
}
