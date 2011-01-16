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
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            return Gloval.Database.CurrentCity;
        }

        public static int Count()
        {
            if (Gloval.Database.Account.Cities == null)
            {
                return DAOCity.GetCities().Count();
            }

            return Gloval.Database.Account.Cities.Count();
        }

        public static DTOCity GetCity(int iIndex)
        {
            //tu dong cap nhat danh sach neu chua co
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
            {
                return Gloval.Database.Account.Cities[iIndex];
            }

            //thong bao loi~
            return null;
        }

        //public static void UpdateCities(int iIndex)
        //{
        //    DAOCity.GetCities();
        //}

        public static DTOCity ChangeCityTo(int iIndex,
            bool bForceUpdateSite)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.CurrentCity == iIndex)
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

                return Gloval.Database.Account.Cities[Gloval.Database.CurrentCity];
            }

            //thong bao loi~
            return null;
        }

        public static DTOCity ChangeCityTo(int iIndex)
        {
            return ChangeCityTo(iIndex, false);
        }

        //nếu = true thì cập nhật lại site rùi mới lấy thông tin
        public static DTOCity GetResourceCity(int iIndex, bool bForceUpdateSite)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
            }

            if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
            {
                //dung them 1 bien bool dat trogn lop de kiem tra,
                //neu da update roi thi ko update nua
                //tru khi dung bien forceupdate
                if (!Gloval.Database.Account.Cities[iIndex].IsUpdatedResource)
                {
                    if (Gloval.Database.CurrentCity == iIndex)
                    {//chưa update, nhưng đang ở viewhiện tại nên lấy res ko request
                        DAOCity.UpdateResourceCity(iIndex);
                        Gloval.Database.Account.Cities[iIndex].DTResourceCity = DateTime.Now;
                        return Gloval.Database.Account.Cities[iIndex];
                    }
                    //chưa update --> buôc request
                    Gloval.Database.Account.Cities[iIndex].IsUpdatedResource = true;
                    bForceUpdateSite = true;
                }

                if (bForceUpdateSite)
                {//buộc request
                    ChangeCityTo(iIndex, bForceUpdateSite);
                    Gloval.Database.Account.Cities[iIndex].DTResourceCity = DateTime.Now;
                    DAOCity.UpdateResourceCity(iIndex);
                }

                return Gloval.Database.Account.Cities[iIndex];
            }

            //thong bao loi~
            return null;
        }
        public static DTOCity GetResourceCity(int iIndex)
        {
            return GetResourceCity(iIndex, false);
        }

        //town hall info
        public static void ForceUpdateTownHall(int iIndexCity)
        {
            BUSCity.ChangeCityTo(iIndexCity, true);
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.TOWN_HALL)
            {
                if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                {
                    DAOCity.GoToCity();
                }

                DAOCity.GoToTownHall();
            }

            Gloval.Database.Account.Cities[iIndexCity].DTTownHall = DateTime.Now;

            //lấy thông tin
            DAOCity.GetTownHallInfomation(iIndexCity);
        }

        public static DTOCity GetTownHallInfomationInCity(int iIndexCity,
            bool bForceUpdate)
        {
            if (Gloval.Database.Account.Cities == null)
            {
                DAOCity.GetCities();
                bForceUpdate = true;
            }

            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                if (bForceUpdate)
                {
                    ForceUpdateTownHall(iIndexCity);
                }
                return Gloval.Database.Account.Cities[iIndexCity];
            }

            //thong bao loi~
            return null;
        }

        public static void CalculateResourceFromLocalData()
        {
            DateTime dtnew = DateTime.Now;
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                
                DTOCity ct = BUSCity.GetResourceCity(i);
                TimeSpan tp = new TimeSpan(dtnew.Ticks - ct.DTResourceCity.Ticks);
            
                //cap nhat dan
                ct.FreePopulation += (int )(ct.PopulationGrow / 3600f * (float)tp.TotalSeconds);
                ct.Population += (int)(ct.PopulationGrow / 3600f * (float)tp.TotalSeconds);

                if (ct.Population > ct.PopulationLimit) ct.Population = (int)ct.PopulationLimit;
                
                //cap nhat vang

                //cap nhat res

                //cap nhat cooldown cac building

                //cap nhat diem scientist point

            }
        }
    }
}
