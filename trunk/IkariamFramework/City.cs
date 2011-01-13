using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace IkariamFramework
{
    public class City
    {
        int _ix;
        int _iy;
        int _iID;
        string _strName;

        int _iWood;
        int _iWine;
        int _iMarble;
        int _iCrystal;
        int _iSulphur;

        int _iWoodPerHour;
        int _iWinePerHour;
        int _iMarblePerHour;
        int _iCrystalPerHour;
        int _iSulphurPerHour;

        int _iPopulation;
        int _iPopulationLimit;

        int _iActionPoint;

        int _ilvlWood;
        int _ilvlTradeGood;
        public enum TRADE_GOOD_TYPE
        {
            WINE,
            MARBLE,
            CRYSTAL,
            SULPHUR
        }
        TRADE_GOOD_TYPE _typeTradeGood;

        //int _iGoldPerHour;

        //house

        //trops

        #region
        public int WoodPerHour
        {
            get { return _iWoodPerHour; }
            set { _iWoodPerHour = value; }
        }
        public int Wood
        {
            get { return _iWood; }
            set { _iWood = value; }
        }
        public int ActionPoint
        {
            get { return _iActionPoint; }
            set { _iActionPoint = value; }
        }
        public int PopulationLimit
        {
            get { return _iPopulationLimit; }
            set { _iPopulationLimit = value; }
        }
        public int Population
        {
            get { return _iPopulation; }
            set { _iPopulation = value; }
        }
        public int SulphurPerHour
        {
            get { return _iSulphurPerHour; }
            set { _iSulphurPerHour = value; }
        }
        public int CrystalPerHour
        {
            get { return _iCrystalPerHour; }
            set { _iCrystalPerHour = value; }
        }
        public int MarblePerHour
        {
            get { return _iMarblePerHour; }
            set { _iMarblePerHour = value; }
        }
        public int WinePerHour
        {
            get { return _iWinePerHour; }
            set { _iWinePerHour = value; }
        }
        public int X
        {
            get { return _ix; }
            set { _ix = value; }
        }
        
        public int Y
        {
            get { return _iy; }
            set { _iy = value; }
        }
        
        public string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }        

        public int ID
        {
            get { return _iID; }
            set { _iID = value; }
        } 

        public int Marble
        {
            get { return _iMarble; }
            set { _iMarble = value; }
        }
        
        public int Wine
        {
            get { return _iWine; }
            set { _iWine = value; }
        }
        
        public int Crystal
        {
            get { return _iCrystal; }
            set { _iCrystal = value; }
        }        

        public int Sulphur
        {
            get { return _iSulphur; }
            set { _iSulphur = value; }
        }     

        public int LvlWood
        {
            get { return _ilvlWood; }
            set { _ilvlWood = value; }
        }

        public int LvlTradeGood
        {
            get { return _ilvlTradeGood; }
            set { _ilvlTradeGood = value; }
        }

        public TRADE_GOOD_TYPE TypeTradeGood
        {
            get { return _typeTradeGood; }
            set { _typeTradeGood = value; }
        }
        #endregion
        
        #region Method
        public static City[] GetCities()
        {//chỉ những thành phố của người chơi
            //nếu muốn lấy những thành phố khác thì phải chỉnh trogn hàm if
            if (GloVal.accInf.Cities == null)
            {
                List<City> arrCity = new List<City>();
                HtmlNodeCollection test = GloVal.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.ListCities);

                HtmlNode node = test[0];
                HtmlNodeCollection listCities = node.ChildNodes;
                foreach (HtmlNode nodeCity in listCities)
                {//moi thanh pho bat dau lay thong tin
                    //chi lay thong tin cac thanh pho cua minh
                    if (nodeCity.GetAttributeValue("class", "err") == "coords")
                    {
                        City ct = new City();
                        ct.ID = nodeCity.GetAttributeValue("value", 0);

                        string strInnerText = nodeCity.NextSibling.InnerText;
                        ct.X = int.Parse(strInnerText.Substring(1, 2));
                        ct.Y = int.Parse(strInnerText.Substring(4, 2));

                        ct.Name = strInnerText.Substring(13, strInnerText.Length - 13);

                        //MessageBox.Show(strInnerText);
                        //MessageBox.Show(ct.ID.ToString()
                        //    + ct.X.ToString()
                        //    + ct.Y.ToString()
                        //    + ct.Name.ToString());
                        arrCity.Add(ct);
                    }
                }

                GloVal.accInf.Cities = arrCity.ToArray();

                //MessageBox.Show(node.InnerHtml);
            }

            GloVal.UpdateOldView();

            return GloVal.accInf.Cities;
        }

        public static void ChangeCity(int iIndexCity)
        {//action=header&function=changeCurrentCity&actionRequest=18210ea9a1d639a30f458267cdcafcb4&oldView=city&cityId=27916
            //GetCities();
            int DesID = GloVal.accInf.Cities[iIndexCity].ID;
            BaseFunction.PostGetHtmlSite(GloVal.Document, GloVal.cookieContainer,
                GloVal.WebUrl,
                string.Format("action=header&function=changeCurrentCity&actionRequest={0}&oldView={1}&cityId={2}",
                    GloVal.GetactionRequest(), GloVal.strOldView, DesID));
        }

        //cap nhat dan so
        //action point
        //4 loai tai nguyen cua tp
        public static void UpdateResourceCity(int iIndexCity)
        {
            //cityResources
            HtmlNode node = GloVal.DocumentNode.SelectSingleNode(
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
                            GloVal.accInf.Cities[iIndexCity].Population = int.Parse(strPo);

                            string strPoLimit = str.Substring(str.IndexOf('(') + 1, str.IndexOf(')') - 1 - str.IndexOf('('));
                            strPoLimit = strPoLimit.Replace(",", "");

                            GloVal.accInf.Cities[iIndexCity].PopulationLimit = int.Parse(strPoLimit);
                            
                            break;
                        }
                    case "actions":
                        {
                            string strTemp = "Action Points: ";
                            string strActionPoint = nodeLiChild.InnerText.Replace(strTemp, "");
                            GloVal.accInf.Cities[iIndexCity].ActionPoint = int.Parse(strActionPoint);
                            break;
                        }
                    case "wood":
                        {
                            GloVal.accInf.Cities[iIndexCity].Wood = GetResource(nodeLiChild);
                            GloVal.accInf.Cities[iIndexCity].WoodPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "wine":
                        {
                            GloVal.accInf.Cities[iIndexCity].Wine = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            GloVal.accInf.Cities[iIndexCity].WinePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "marble":
                        {
                            GloVal.accInf.Cities[iIndexCity].Marble = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            GloVal.accInf.Cities[iIndexCity].MarblePerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "glass":
                        {
                            GloVal.accInf.Cities[iIndexCity].Crystal = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            GloVal.accInf.Cities[iIndexCity].CrystalPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                    case "sulfur":
                        {
                            GloVal.accInf.Cities[iIndexCity].Sulphur = GetResource(nodeLiChild);
                            if (nodeLiChild.ChildNodes[5].ChildNodes.Count == 3) break;
                            GloVal.accInf.Cities[iIndexCity].SulphurPerHour = GetResourcePerHour(nodeLiChild);
                            break;
                        }
                }
            }

            MessageBox.Show(GloVal.accInf.Cities[iIndexCity].Population.ToString() + " " + GloVal.accInf.Cities[iIndexCity].PopulationLimit.ToString()
                + "/ " + GloVal.accInf.Cities[iIndexCity].ActionPoint.ToString()
            + "/ " + GloVal.accInf.Cities[iIndexCity].Wood.ToString() + " " + GloVal.accInf.Cities[iIndexCity].WoodPerHour.ToString()
            + "/ " + GloVal.accInf.Cities[iIndexCity].Wine.ToString() + " " + GloVal.accInf.Cities[iIndexCity].WinePerHour.ToString()
            + "/ " + GloVal.accInf.Cities[iIndexCity].Marble.ToString() + " " + GloVal.accInf.Cities[iIndexCity].MarblePerHour.ToString()
            + "/ " + GloVal.accInf.Cities[iIndexCity].Crystal.ToString() + " " + GloVal.accInf.Cities[iIndexCity].CrystalPerHour.ToString()
            + "/ " + GloVal.accInf.Cities[iIndexCity].Sulphur.ToString() + " " + GloVal.accInf.Cities[iIndexCity].SulphurPerHour.ToString());
        }

        public static int GetResource(HtmlNode node)
        {
            string strRes = node.ChildNodes[3].InnerText;
            strRes = strRes.Replace(",", "");
            return int.Parse(strRes);
        }

        public static int GetResourcePerHour(HtmlNode node)
        {
            //perhour 5 - 2
            string strRes;strRes = node.ChildNodes[5].ChildNodes[2].InnerText;
            strRes = strRes.Replace("\n", "");
            strRes = strRes.Replace(",", "");
            strRes = strRes.Trim();
            return int.Parse(strRes);
        }

        //cap nhat lvl, type cac nhà trong thành phố
        public static void UpdateHouseInCity()
        {

        }
        #endregion
    }
}
