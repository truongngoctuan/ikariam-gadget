﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.DAOIkariamFramework
{
    public class DAOTroops
    {
        #region request
        #endregion

        #region goToPage
        #endregion

        #region ParserData
        #endregion

        public static DTOTroops[] ParseUnits(int iIndexCity)
        {//xem nhu da vao duoc view troops
            HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.DivTableUnits);

            List<DTOTroops> list = new List<DTOTroops>();

            //lay linh cua mình 
            HtmlNode OurTroops = nodeCol[0];
            HtmlNode FirstLine = OurTroops.ChildNodes[3].ChildNodes[1].ChildNodes[3];
            HtmlNode SecondLine = OurTroops.ChildNodes[3].ChildNodes[3].ChildNodes[3];

            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[1], DTOTroops.TYPE.Hoplite, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[2], DTOTroops.TYPE.Steam_Giant, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[3], DTOTroops.TYPE.Spearman, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[4], DTOTroops.TYPE.Swordsman, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[5], DTOTroops.TYPE.Slinger, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[6], DTOTroops.TYPE.Archer, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[7], DTOTroops.TYPE.Sulphur_Carabineer, true));

            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[1], DTOTroops.TYPE.Ram, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[2], DTOTroops.TYPE.Catapult, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[3], DTOTroops.TYPE.Mortar, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[4], DTOTroops.TYPE.Gyrocopter, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[5], DTOTroops.TYPE.Balloon_Bombardier, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[6], DTOTroops.TYPE.Cook, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[7], DTOTroops.TYPE.Doctor, true));

            return list.ToArray();
        }

        public static DTOTroops[] ParseShips(int iIndexCity)
        {//xem nhu da vao duoc view troops  -> tab ships
            HtmlNodeCollection nodeCol = Gloval.Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.DivTableShips);

            List<DTOTroops> list = new List<DTOTroops>();

            //lay linh cua mình 
            HtmlNode OurTroops = nodeCol[0];
            HtmlNode FirstLine = OurTroops.ChildNodes[3].ChildNodes[1].ChildNodes[3];
            HtmlNode SecondLine = OurTroops.ChildNodes[3].ChildNodes[3].ChildNodes[3];

            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[1], DTOTroops.TYPE.Ram_Ship, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[2], DTOTroops.TYPE.Fire_Ship, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[3], DTOTroops.TYPE.Paddle_Wheel_Ram, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[4], DTOTroops.TYPE.Ballista_Ship, false));

            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[1], DTOTroops.TYPE.Catapult_Ship, false));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[2], DTOTroops.TYPE.Mortar_Ship, false));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[3], DTOTroops.TYPE.Diving_Boat, false));

            return list.ToArray();
        }

        static DTOTroops GetUnitAndTroop(HtmlNode node,
            DTOTroops.TYPE type,
            bool bIsUnits)
        {
            DTOTroops troop = new DTOTroops();
            troop.Type = type;
            troop.IsUnits = bIsUnits;
            //string strTemp = node.InnerText;
            //strTemp = strTemp.Replace(",", "");
            //strTemp = strTemp.Replace("-", "");
            //troop.Quality = int.Parse(strTemp);
            troop.Quality = NodeParser.toInt(node.InnerText);
            
            return troop;
        }

        public static void GoToTroops()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowTroops);
        }

        public static void GoToShips()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowTroopsShips);
        }
    }
}
