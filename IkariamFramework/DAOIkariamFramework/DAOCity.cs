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
            //nếu muốn lấy những thành phố khác thì phải chỉnh trogn hàm if
            if (Database.accInf.Cities == null)
            {
                List<DTOCity> arrCity = new List<DTOCity>();
                HtmlNodeCollection test = Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.ListCities);

                HtmlNode node = test[0];
                HtmlNodeCollection listCities = node.ChildNodes;
                foreach (HtmlNode nodeCity in listCities)
                {//moi thanh pho bat dau lay thong tin
                    //chi lay thong tin cac thanh pho cua minh
                    if (nodeCity.GetAttributeValue("class", "err") == "coords")
                    {
                        DTOCity ct = new DTOCity();
                        ct.ID = nodeCity.GetAttributeValue("value", 0);

                        string strInnerText = nodeCity.NextSibling.InnerText;
                        ct.X = int.Parse(strInnerText.Substring(1, 2));
                        ct.Y = int.Parse(strInnerText.Substring(4, 2));

                        ct.Name = strInnerText.Substring(13, strInnerText.Length - 13);

                        //loai thành phố ??
                        //Trade good: Marble
                        string strType = nodeCity.GetAttributeValue("title", "err");
                        strType = strType.Substring(12);
                        switch (strType)
                        {
                            case "Wine":
                                {
                                    ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.WINE;
                                    break;
                                }
                            case "Marble":
                                {
                                    ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.MARBLE;
                                    break;
                                }
                            case "Crystal Glass":
                                {
                                    ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.CRYSTAL;
                                    break;
                                }
                            case "Sulphur":
                                {
                                    ct.TypeTradeGood = DTOCity.TRADE_GOOD_TYPE.SULPHUR;
                                    break;
                                }
                        }

                        //MessageBox.Show(strInnerText);
                        //MessageBox.Show(ct.ID.ToString()
                        //    + ct.X.ToString()
                        //    + ct.Y.ToString()
                        //    + ct.Name.ToString());
                        arrCity.Add(ct);
                    }
                }

                Database.accInf.Cities = arrCity.ToArray();

                //MessageBox.Show(node.InnerHtml);
            }

            Database.UpdateOldView();

            return Database.accInf.Cities;
        }

        public static void ChangeCity(int iIndexCity)
        {//action=header&function=changeCurrentCity&actionRequest=18210ea9a1d639a30f458267cdcafcb4&oldView=city&cityId=27916
            //GetCities();
            
            int DesID = Database.accInf.Cities[iIndexCity].ID;
            //if (DesID == Database.iCurrentCity)
            //{//nếu là city hiện tại thì không cần thay đổi
            //    return;
            //}

            BaseFunction.PostGetHtmlSite(Database.WebUrl,
                string.Format("action=header&function=changeCurrentCity&actionRequest={0}&oldView={1}&cityId={2}",
                    Database.GetactionRequest(), Database.strOldView, DesID));

            Database.iCurrentCity = iIndexCity;
        }

        //cap nhat dan so
        //action point
        //4 loai tai nguyen cua tp
        public static void UpdateResourceCity(int iIndexCity)
        {
            //cityResources
            HtmlNode node = Database.DocumentNode.SelectSingleNode(
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
                            Database.accInf.Cities[iIndexCity].Population = int.Parse(strPo);

                            string strPoLimit = str.Substring(str.IndexOf('(') + 1, str.IndexOf(')') - 1 - str.IndexOf('('));
                            strPoLimit = strPoLimit.Replace(",", "");

                            Database.accInf.Cities[iIndexCity].PopulationLimit = int.Parse(strPoLimit);
                            
                            break;
                        }
                    case "actions":
                        {
                            string strTemp = "Action Points: ";
                            string strActionPoint = nodeLiChild.InnerText.Replace(strTemp, "");
                            Database.accInf.Cities[iIndexCity].ActionPoint = int.Parse(strActionPoint);
                            break;
                        }
                    case "wood":
                        {
                            Database.accInf.Cities[iIndexCity].Wood = GetResource(nodeLiChild);
                            Database.accInf.Cities[iIndexCity].WoodPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "wine":
                        {
                            Database.accInf.Cities[iIndexCity].Wine = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Database.accInf.Cities[iIndexCity].WinePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "marble":
                        {
                            Database.accInf.Cities[iIndexCity].Marble = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Database.accInf.Cities[iIndexCity].MarblePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "glass":
                        {
                            Database.accInf.Cities[iIndexCity].Crystal = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Database.accInf.Cities[iIndexCity].CrystalPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "sulfur":
                        {
                            Database.accInf.Cities[iIndexCity].Sulphur = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            Database.accInf.Cities[iIndexCity].SulphurPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                }
            }

            //MessageBox.Show(Database.accInf.Cities[iIndexCity].Population.ToString() + " " + Database.accInf.Cities[iIndexCity].PopulationLimit.ToString()
            //    + "/ " + Database.accInf.Cities[iIndexCity].ActionPoint.ToString()
            //+ "/ " + Database.accInf.Cities[iIndexCity].Wood.ToString() + " " + Database.accInf.Cities[iIndexCity].WoodPerHour.ToString()
            //+ "/ " + Database.accInf.Cities[iIndexCity].Wine.ToString() + " " + Database.accInf.Cities[iIndexCity].WinePerHour.ToString()
            //+ "/ " + Database.accInf.Cities[iIndexCity].Marble.ToString() + " " + Database.accInf.Cities[iIndexCity].MarblePerHour.ToString()
            //+ "/ " + Database.accInf.Cities[iIndexCity].Crystal.ToString() + " " + Database.accInf.Cities[iIndexCity].CrystalPerHour.ToString()
            //+ "/ " + Database.accInf.Cities[iIndexCity].Sulphur.ToString() + " " + Database.accInf.Cities[iIndexCity].SulphurPerHour.ToString());
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
        //    HtmlNodeCollection nodeCol = Database.DocumentNode.SelectNodes(
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
            Database.CurrentView = Database.SITE_VIEW.CITY;
        }
        public static void GoToIsland()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowIsland);
            Database.CurrentView = Database.SITE_VIEW.ISLAND;
        }
        public static void GoToWorld()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowWorld);
            Database.CurrentView = Database.SITE_VIEW.WORLD;
        }
        #endregion
    }
}
