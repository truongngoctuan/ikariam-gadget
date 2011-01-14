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
    public class BUSBuilding
    {
        public static int Count(int iIndexCity)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListBuilding == null)
                {
                    BUSCity.ChangeCityTo(iIndexCity);
                    if (Database.CurrentView != Database.SITE_VIEW.CITY)
                    {
                        DAOCity.GoToCity();
                    }
                    DAOBuilding.GetBuildingCity(iIndexCity);
                }

                return Database.accInf.Cities[iIndexCity].ListBuilding.Count();
            }

            return -1;            
        }

        public static void ForceUpdate(int iIndexCity)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                BUSCity.ChangeCityTo(iIndexCity);
                if (Database.CurrentView != Database.SITE_VIEW.CITY)
                {
                    DAOCity.GoToCity();
                }
                DAOBuilding.GetBuildingCity(iIndexCity);
            }
        }

        public static DTOBuilding GetHouseInfomationInCity(int iIndexCity,
            int iIndexBuilding)
        {
            if (Database.accInf.Cities == null)
            {
                ForceUpdate(iIndexCity);
            }

            if (0 <= iIndexCity && iIndexCity < Database.accInf.Cities.Count())
            {
                if (Database.accInf.Cities[iIndexCity].ListBuilding == null)
                {
                    ForceUpdate(iIndexCity);
                }

                if (0 <= iIndexBuilding && iIndexBuilding < Database.accInf.Cities[iIndexCity].ListBuilding.Count())
                {
                    return Database.accInf.Cities[iIndexCity].ListBuilding[iIndexBuilding];
                }
            }

            //thong bao loi~
            return null;
        }
    }
}
