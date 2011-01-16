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
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListBuilding == null)
                {
                    BUSCity.ChangeCityTo(iIndexCity);
                    if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                    {
                        DAOCity.GoToCity();
                    }
                    DAOBuilding.GetBuildingCity(iIndexCity);
                }

                return Gloval.Database.Account.Cities[iIndexCity].ListBuilding.Count();
            }

            return -1;            
        }

        public static void ForceUpdate(int iIndexCity)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                BUSCity.ChangeCityTo(iIndexCity);
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                {
                    DAOCity.GoToCity();
                }
                //cap nhat thoi gian update tu ika server
                Gloval.Database.Account.Cities[iIndexCity].DTBuilding = DateTime.Now;
                DAOBuilding.GetBuildingCity(iIndexCity);
            }
        }

        public static DTOBuilding GetHouseInfomationInCity(int iIndexCity,
            int iIndexBuilding)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                ForceUpdate(iIndexCity);
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.Account.Cities[iIndexCity].ListBuilding == null)
                {
                    ForceUpdate(iIndexCity);
                }

                if (0 <= iIndexBuilding && iIndexBuilding < Gloval.Database.Account.Cities[iIndexCity].ListBuilding.Count())
                {
                    return Gloval.Database.Account.Cities[iIndexCity].ListBuilding[iIndexBuilding];
                }
            }

            //thong bao loi~
            return null;
        }
    }
}
