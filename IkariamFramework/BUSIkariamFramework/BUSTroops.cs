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
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits == null)
                {
                    ForceUpdateUnits(iIndexCity);
                }

                return Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits.Count();
            }

            return -1;
        }

        public static void ForceUpdateUnits(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity, true);
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS)
            {
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                {
                    DAOCity.GoToCity();
                }

                //nhảy vào trang troops
                DAOTroops.GoToTroops();
            }

            Gloval.Database.Account.Cities[iIndexCity].DTTroopUnits = DateTime.Now;

            //lấy thông tin
            DAOTroops.GetUnits(iIndexCity);
        }

        public static DTOTroops GetUnitsInCity(int iIndexCity,
            int iIndexTroops)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();

            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits == null)
                {
                    ForceUpdateUnits(iIndexCity);
                }

                if (0 <= iIndexTroops && iIndexTroops < Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits.Count())
                {
                    return Gloval.Database.Account.Cities[iIndexCity].ListTroopsUnits[iIndexTroops];
                }
            }

            //thong bao loi~
            return null;
        }

        //--------------------------------------------------------
        //ships
        public static int CountShips(int iIndexCity)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips == null)
                {
                    ForceUpdateShips(iIndexCity);
                }

                return Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips.Count();
            }

            return -1;
        }

        public static void ForceUpdateShips(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity, true);
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS_SHIPS)
            {
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.TROOPS)
                {
                    if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                    {
                        DAOCity.GoToCity();
                    }

                    //nhảy vào trang troops
                    DAOTroops.GoToTroops();
                }
                DAOTroops.GoToShips();
            }

            Gloval.Database.Account.Cities[iIndexCity].DTTroopShips = DateTime.Now;

            //lấy thông tin
            DAOTroops.GetShipss(iIndexCity);
        }

        public static DTOTroops GetShipsInCity(int iIndexCity,
            int iIndexTroops)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips == null)
                {
                    ForceUpdateShips(iIndexCity);
                }

                if (0 <= iIndexTroops && iIndexTroops < Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips.Count())
                {
                    return Gloval.Database.Account.Cities[iIndexCity].ListTroopsShips[iIndexTroops];
                }
            }

            //thong bao loi~
            return null;
        }
    }
}
