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
        #region goto 
        public static void ChangeCityTo(int iIndex)
        {
            if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
            {
                if (Gloval.Database.CurrentCity != iIndex)
                {
                    DAOCity.requestChangeCityTo(Gloval.Database.Account.Cities[iIndex].ID);

                    Gloval.Database.CurrentCity = iIndex;
                }
            }
        }
        public static void GoToCity()
        {
            DAOCity.GoToCity();
            Gloval.Database.CurrentView = Data.SITE_VIEW.CITY;
        }
        public static void GoToIsland()
        {
            DAOCity.GoToIsland();
            Gloval.Database.CurrentView = Data.SITE_VIEW.ISLAND;
        }
        public static void GoToWorld()
        {
            DAOCity.GoToWorld();
            Gloval.Database.CurrentView = Data.SITE_VIEW.WORLD;
        }
        public static void GoToTownHall()
        {
            DAOCity.GoToTownHall();
            Gloval.Database.CurrentView = Data.SITE_VIEW.TOWN_HALL;
        }
        #endregion

        #region city
        public static void requestCities()
        {
            if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
            {
                DAOCity.GoToCity();
            }
            //cap nhat thoi gian update tu ika server
            Gloval.Database.Account.Cities = DAOCity.ParseCity(Gloval.Database.DocumentNode);
        }
        #endregion

        #region res
        public static void requestResourceCity(int iIndex)
        {
            if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
            {
                ChangeCityTo(iIndex);
                Gloval.Database.Account.Cities[iIndex].DTResourceCity = DateTime.Now;
                Gloval.Database.Account.Cities[iIndex] = DAOCity.ParseResources(Gloval.Database.Account.Cities[iIndex]);
            }
        }

        public static void CalculateFromLocalData()
        {
            DateTime dtnew = DateTime.Now;
            int nCities = Gloval.Database.Account.Cities.Count();
            for (int i = 0; i < nCities; i++)
            {

                DTOCity ct = Gloval.Database.Account.Cities[i];
                TimeSpan tp = new TimeSpan(dtnew.Ticks - ct.DTResourceCity.Ticks);

                //cap nhat dan - townhall
                ct.FreePopulation += BaseFunction.updateValue(ct.PopulationGrow, (float)tp.TotalSeconds);
                ct.Population = BaseFunction.updateValueHaveLimit(ct.Population, ct.PopulationLimit, ct.PopulationGrow, (float)tp.TotalSeconds);

                //cap nhat res
                ct.Wood = BaseFunction.updateValueHaveLimit(ct.Wood, ct.WoodLimit, ct.WoodPerHour, (float)tp.TotalSeconds);
                ct.Wine = BaseFunction.updateValueHaveLimit(ct.Wine, ct.WineLimit, ct.WinePerHour, (float)tp.TotalSeconds);
                ct.Marble = BaseFunction.updateValueHaveLimit(ct.Marble, ct.MarbleLimit, ct.MarblePerHour, (float)tp.TotalSeconds);
                ct.Crystal = BaseFunction.updateValueHaveLimit(ct.Crystal, ct.CrystalLimit, ct.CrystalPerHour, (float)tp.TotalSeconds);
                ct.Sulphur = BaseFunction.updateValueHaveLimit(ct.Sulphur, ct.SulphurLimit, ct.SulphurPerHour, (float)tp.TotalSeconds);

                //cap nhat cooldown cac building

                //cap nhat lai thoi gian
                ct.DTResourceCity = dtnew;
                ct.DTTownHall = dtnew;
                Gloval.Database.Account.Cities[i] = ct;
            }
        }
        #endregion

        #region townhall
        public static void requestTownHall(int iIndexCity)
        {
            if (0 <= iIndexCity && iIndexCity < Gloval.Database.Account.Cities.Count())
            {
                BUSCity.ChangeCityTo(iIndexCity);

                if (Gloval.Database.CurrentView != Data.SITE_VIEW.TOWN_HALL)
                {
                    if (Gloval.Database.CurrentView != Data.SITE_VIEW.CITY)
                    {
                        BUSCity.GoToCity();
                    }

                    BUSCity.GoToTownHall();
                }

                Gloval.Database.Account.Cities[iIndexCity].DTTownHall = DateTime.Now;

                //lấy thông tin
                long PopulationLimit; 
                float PopulationGrow;
                long GoldPerHour;
                int ResearchPointPerHour;
                DAOCity.GetTownHallInfomation(iIndexCity, 
                    out PopulationLimit,
                    out PopulationGrow,
                    out GoldPerHour,
                    out ResearchPointPerHour);

                Gloval.Database.Account.Cities[iIndexCity].PopulationLimit = PopulationLimit;
                Gloval.Database.Account.Cities[iIndexCity].PopulationGrow = PopulationGrow;
                Gloval.Database.Account.Cities[iIndexCity].GoldPerHour = GoldPerHour;
                Gloval.Database.Account.Cities[iIndexCity].ResearchPointPerHour = ResearchPointPerHour;
            }
        }
        #endregion

        #region old code
        //public static int GetCurrentCity()
        //{
        //    if (Gloval.Database.Account.Cities == null)
        //    {
        //        DAOCity.GetCities();
        //    }

        //    return Gloval.Database.CurrentCity;
        //}

        //public static int Count()
        //{
        //    if (Gloval.Database.Account.Cities == null)
        //    {
        //        return DAOCity.GetCities().Count();
        //    }

        //    return Gloval.Database.Account.Cities.Count();
        //}

        //public static DTOCity GetCity(int iIndex)
        //{
        //    //tu dong cap nhat danh sach neu chua co
        //    if (Gloval.Database.Account.Cities == null)
        //    {
        //        DAOCity.GetCities();
        //    }

        //    if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
        //    {
        //        return Gloval.Database.Account.Cities[iIndex];
        //    }

        //    //thong bao loi~
        //    return null;
        //}

        //public static void UpdateCities(int iIndex)
        //{
        //    DAOCity.GetCities();
        //}

        //public static DTOCity ChangeCityTo(int iIndex,
        //    bool bForceUpdateSite)
        //{
        //    if (Gloval.Database.Account.Cities == null)
        //    {
        //        DAOCity.GetCities();
        //    }

        //    if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
        //    {
        //        if (Gloval.Database.CurrentCity == iIndex)
        //        {
        //            if (bForceUpdateSite)
        //            {
        //                DAOCity.ChangeCity(iIndex);
        //            }
        //        }
        //        else
        //        {
        //            DAOCity.ChangeCity(iIndex);
        //        }

        //        return Gloval.Database.Account.Cities[Gloval.Database.CurrentCity];
        //    }

        //    //thong bao loi~
        //    return null;
        //}

        //public static DTOCity ChangeCityTo(int iIndex)
        //{
        //    return ChangeCityTo(iIndex, false);
        //}


        ////nếu = true thì cập nhật lại site rùi mới lấy thông tin
        //public static DTOCity GetResourceCity(int iIndex, bool bForceUpdateSite)
        //{
        //    if (Gloval.Database.Account.Cities == null)
        //    {
        //        DAOCity.GetCities();
        //    }

        //    if (0 <= iIndex && iIndex < Gloval.Database.Account.Cities.Count())
        //    {
        //        //dung them 1 bien bool dat trogn lop de kiem tra,
        //        //neu da update roi thi ko update nua
        //        //tru khi dung bien forceupdate
        //        if (!Gloval.Database.Account.Cities[iIndex].IsUpdatedResource)
        //        {
        //            if (Gloval.Database.CurrentCity == iIndex)
        //            {//chưa update, nhưng đang ở viewhiện tại nên lấy res ko request
        //                DAOCity.UpdateResourceCity(iIndex);
        //                Gloval.Database.Account.Cities[iIndex].DTResourceCity = DateTime.Now;
        //                return Gloval.Database.Account.Cities[iIndex];
        //            }
        //            //chưa update --> buôc request
        //            Gloval.Database.Account.Cities[iIndex].IsUpdatedResource = true;
        //            bForceUpdateSite = true;
        //        }

        //        if (bForceUpdateSite)
        //        {//buộc request
        //            ChangeCityTo(iIndex);
        //            Gloval.Database.Account.Cities[iIndex].DTResourceCity = DateTime.Now;
        //            DAOCity.UpdateResourceCity(iIndex);
        //        }

        //        return Gloval.Database.Account.Cities[iIndex];
        //    }

        //    //thong bao loi~
        //    return null;
        //}
        //public static DTOCity GetResourceCity(int iIndex)
        //{
        //    return GetResourceCity(iIndex, false);
        //}
        #endregion
    }
}
