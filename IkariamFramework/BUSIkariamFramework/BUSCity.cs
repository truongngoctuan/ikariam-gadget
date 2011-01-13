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
    public class BUSCity
    {
        public static int GetCurrentCity()
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            return Database.iCurrentCity;
        }

        public static int Count()
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities().Count();
            }

            return Database.accInf.Cities.Count();
        }

        public static DTOCity GetCity(int iIndex)
        {
            //tu dong cap nhat danh sach neu chua co
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Database.accInf.Cities.Count())
            {
                return Database.accInf.Cities[iIndex];
            }

            //thong bao loi~
            return null;
        }

        public static void UpdateCities(int iIndex)
        {
            DAOCity.GetCities();
        }

        public static DTOCity ChangeCityTo(int iIndex)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Database.accInf.Cities.Count())
            {
                DAOCity.ChangeCity(iIndex);
                return Database.accInf.Cities[Database.iCurrentCity];
            }

            //thong bao loi~
            return null;
        }

        //nếu = true thì cập nhật lại site rùi mới lấy thông tin
        public static DTOCity GetResourceCity(int iIndex, bool bForceUpdateSite)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Database.accInf.Cities.Count())
            {
                if (Database.iCurrentCity == iIndex)
                {
                    if (bForceUpdateSite)
                    {
                        DAOCity.ChangeCity(iIndex);
                    }
                }
                else
                {
                    DAOCity.ChangeCity(iIndex);
                }

                DAOCity.UpdateResourceCity(iIndex);
                return Database.accInf.Cities[iIndex];
            }

            //thong bao loi~
            return null;
        }
        public static DTOCity GetResourceCity(int iIndex)
        {
            return GetResourceCity(iIndex, false);
        }

        public static DTOCity GetHouseInfomationInCity(int iIndex)
        {
            if (Database.accInf.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Database.accInf.Cities.Count())
            {
                DAOCity.ChangeCity(iIndex);
                //DAOCity.UpdateResourceCity(iIndex);
                //change city sau khi post chỗ list; 
                //cần giả lập click vào chỗ xem thành phố để 
                //vào thành phố (nếu cần lấy danh sách nhà, lính,..)
                return Database.accInf.Cities[iIndex];
            }

            //thong bao loi~
            return null;
        }
    }
}
