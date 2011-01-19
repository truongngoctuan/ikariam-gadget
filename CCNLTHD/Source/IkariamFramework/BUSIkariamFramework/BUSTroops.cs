using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DAOIkariamFramework;
using IkariamFramework.DTOIkariamFramework;
using System.Runtime.InteropServices;

namespace IkariamFramework.BUSIkariamFramework
{
    [ComVisible(true)]
    public class BUSTroops
    {
        #region units
        public static void requestUnits(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity);
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS)
            {
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                {
                    BUSCity.GoToCity();
                }

                //nhảy vào trang troops
                BUSTroops.GoToTroops();
            }

            Gloval.Database.Account.Cities[iIndexCity].DTTroopUnits = DateTime.Now;
            //lấy thông tin
            Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits = DAOTroops.ParseUnits(iIndexCity);
        }

        public static void GoToTroops()
        {
            DAOTroops.GoToTroops();
            Gloval.Database.CurrentView = Data.SITE_VIEW.TROOPS;
        }

        //public static int CountUnits(int iIndexCity)
        //{
        //    //if (Gloval.Database.Account.Cities == null)
        //    //{
        //    //    DAOCity.GetCities();
        //    //}

        //    if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
        //    {
        //        if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits == null)
        //        {
        //            ForceUpdateUnits(iIndexCity);
        //        }

        //        return Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits.Count();
        //    }

        //    return -1;
        //}        

        //public static DTOTroops GetUnitsInCity(int iIndexCity,
        //    int iIndexTroops)
        //{
        //    //if (Gloval.Database.Account.Cities == null)
        //    //{
        //    //    DAOCity.GetCities();

        //    //}

        //    if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
        //    {
        //        if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits == null)
        //        {
        //            ForceUpdateUnits(iIndexCity);
        //        }

        //        if (0 <= iIndexTroops && iIndexTroops < Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits.Count())
        //        {
        //            return Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits[iIndexTroops];
        //        }
        //    }

        //    //thong bao loi~
        //    return null;
        //}

        #endregion

        #region ships
        public static void requestShips(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity);
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS_SHIPS)
            {
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS)
                {
                    if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                    {
                        BUSCity.GoToCity();
                    }

                    //nhảy vào trang troops
                    DAOTroops.GoToTroops();
                }
                BUSTroops.GoToShips();
            }

            Gloval.Database.Account.Cities[iIndexCity].DTTroopShips = DateTime.Now;

            //lấy thông tin
            Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips = DAOTroops.ParseShips(iIndexCity);
        }

        public static void GoToShips()
        {
            DAOTroops.GoToShips();
            Gloval.Database.CurrentView = Data.SITE_VIEW.TROOPS_SHIPS;
        }

        //public static int CountShips(int iIndexCity)
        //{
        //    //if (Gloval.Database.Account.Cities == null)
        //    //{
        //    //    DAOCity.GetCities();
        //    //}

        //    if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
        //    {
        //        if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips == null)
        //        {
        //            ForceUpdateShips(iIndexCity);
        //        }

        //        return Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips.Count();
        //    }

        //    return -1;
        //}

        //public static DTOTroops GetShipsInCity(int iIndexCity,
        //    int iIndexTroops)
        //{
        //    //if (Gloval.Database.Account.Cities == null)
        //    //{
        //    //    DAOCity.GetCities();
        //    //}

        //    if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
        //    {
        //        if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips == null)
        //        {
        //            ForceUpdateShips(iIndexCity);
        //        }

        //        if (0 <= iIndexTroops && iIndexTroops < Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips.Count())
        //        {
        //            return Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips[iIndexTroops];
        //        }
        //    }

        //    //thong bao loi~
        //    return null;
        //}

        #endregion
    }
}
