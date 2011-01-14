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
        public static void GetUnits(int iIndexCity)
        {//xem nhu da vao duoc view troops
            HtmlNodeCollection nodeCol = Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.DivTableUnits);

            List<DTOTroops> list = new List<DTOTroops>();

            //lay linh cua mình 
            HtmlNode OurTroops = nodeCol[0];
            HtmlNode FirstLine = OurTroops.ChildNodes[3].ChildNodes[1].ChildNodes[3];
            HtmlNode SecondLine = OurTroops.ChildNodes[3].ChildNodes[3].ChildNodes[3];

            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[1], DTOTroops.TROOPS_TYPE.Hoplite, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[2], DTOTroops.TROOPS_TYPE.Steam_Giant, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[3], DTOTroops.TROOPS_TYPE.Spearmen, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[4], DTOTroops.TROOPS_TYPE.Swordsman, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[5], DTOTroops.TROOPS_TYPE.Slinger, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[6], DTOTroops.TROOPS_TYPE.Archer, true));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[7], DTOTroops.TROOPS_TYPE.Sulphur_Carabineers, true));

            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[1], DTOTroops.TROOPS_TYPE.Battering_ram, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[2], DTOTroops.TROOPS_TYPE.Catapult, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[3], DTOTroops.TROOPS_TYPE.Mortar, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[4], DTOTroops.TROOPS_TYPE.Gyrocopter, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[5], DTOTroops.TROOPS_TYPE.Balloon_Bombardier, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[6], DTOTroops.TROOPS_TYPE.Cook, true));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[7], DTOTroops.TROOPS_TYPE.Doctor, true));

            Database.accInf.Cities[iIndexCity].ListTroopsUnits = list.ToArray();
        }

        public static void GetShipss(int iIndexCity)
        {//xem nhu da vao duoc view troops  -> tab ships
            HtmlNodeCollection nodeCol = Database.DocumentNode.SelectNodes(
                    XPathManager.XPathCity.DivTableShips);

            List<DTOTroops> list = new List<DTOTroops>();

            //lay linh cua mình 
            HtmlNode OurTroops = nodeCol[0];
            HtmlNode FirstLine = OurTroops.ChildNodes[3].ChildNodes[1].ChildNodes[3];
            HtmlNode SecondLine = OurTroops.ChildNodes[3].ChildNodes[3].ChildNodes[3];

            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[1], DTOTroops.TROOPS_TYPE.Ram_Ship, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[2], DTOTroops.TROOPS_TYPE.Fire_Ship, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[3], DTOTroops.TROOPS_TYPE.Paddle_Wheel_Ram, false));
            list.Add(GetUnitAndTroop(FirstLine.ChildNodes[4], DTOTroops.TROOPS_TYPE.Ballista_ship, false));

            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[1], DTOTroops.TROOPS_TYPE.Catapult_Ship, false));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[2], DTOTroops.TROOPS_TYPE.Mortar_Ship, false));
            list.Add(GetUnitAndTroop(SecondLine.ChildNodes[3], DTOTroops.TROOPS_TYPE.Diving_boat, false));

            Database.accInf.Cities[iIndexCity].ListTroopsShips = list.ToArray();
        }

        static DTOTroops GetUnitAndTroop(HtmlNode node,
            DTOTroops.TROOPS_TYPE type,
            bool bIsUnits)
        {
            DTOTroops troop = new DTOTroops();
            troop.Type = type;
            troop.IsUnits = bIsUnits;
            string strTemp = node.InnerText;
            strTemp = strTemp.Replace(",", "");
            strTemp = strTemp.Replace("-", "");
            troop.Quality = int.Parse(strTemp);
            
            return troop;
        }

        public static void GoToTroops()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowTroops);
            Database.CurrentView = Database.SITE_VIEW.TROOPS;
        }

        public static void GoToShips()
        {
            BaseFunction.GoToLink(XPathManager.XPathCity.ShowTroopsShips);
            Database.CurrentView = Database.SITE_VIEW.TROOPS_SHIPS;
        }
    }
}
