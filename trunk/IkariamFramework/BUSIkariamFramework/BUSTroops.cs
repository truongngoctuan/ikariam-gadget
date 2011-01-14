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
        public static int CountUnits(int iIndexCity)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListTroopsUnits == null)
                {
                    ForceUpdateUnits(iIndexCity);
                }

                return Database.accInf.Cities[iIndexCity].ListTroopsUnits.Count();
            }

            return -1;
        }

        public static void ForceUpdateUnits(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity, true);
            if (Database.CurrentView != Database.SITE_VIEW.TROOPS)
            {
                if (Database.CurrentView != Database.SITE_VIEW.CITY)
                {
                    DAOCity.GoToCity();
                }

                //nhảy vào trang troops
                DAOTroops.GoToTroops();
            }

            //lấy thông tin
            DAOTroops.GetUnits(iIndexCity);
        }

        public static DTOTroops GetUnitsInCity(int iIndexCity,
            int iIndexTroops)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();

            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListTroopsUnits == null)
                {
                    ForceUpdateUnits(iIndexCity);
                }

                if (0 <= iIndexTroops && iIndexTroops < Database.accInf.Cities[iIndexCity].ListTroopsUnits.Count())
                {
                    return Database.accInf.Cities[iIndexCity].ListTroopsUnits[iIndexTroops];
                }
            }

            //thong bao loi~
            return null;
        }

        //--------------------------------------------------------
        //ships
        public static int CountShips(int iIndexCity)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListTroopsShips == null)
                {
                    ForceUpdateShips(iIndexCity);
                }

                return Database.accInf.Cities[iIndexCity].ListTroopsShips.Count();
            }

            return -1;
        }

        public static void ForceUpdateShips(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity, true);
            if (Database.CurrentView != Database.SITE_VIEW.TROOPS_SHIPS)
            {
                if (Database.CurrentView != Database.SITE_VIEW.TROOPS)
                {
                    if (Database.CurrentView != Database.SITE_VIEW.CITY)
                    {
                        DAOCity.GoToCity();
                    }

                    //nhảy vào trang troops
                    DAOTroops.GoToTroops();
                }

                DAOTroops.GoToShips();
            }

            //lấy thông tin
            DAOTroops.GetShipss(iIndexCity);
        }

        public static DTOTroops GetShipsInCity(int iIndexCity,
            int iIndexTroops)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListTroopsShips == null)
                {
                    ForceUpdateShips(iIndexCity);
                }

                if (0 <= iIndexTroops && iIndexTroops < Database.accInf.Cities[iIndexCity].ListTroopsShips.Count())
                {
                    return Database.accInf.Cities[iIndexCity].ListTroopsShips[iIndexTroops];
                }
            }

            //thong bao loi~
            return null;
        }
    }
}
