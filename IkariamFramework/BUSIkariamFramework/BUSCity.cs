﻿using System;
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
                        return Gloval.Database.Account.Cities[iIndex];
                    }
                    //chưa update --> buôc request
                    Gloval.Database.Account.Cities[iIndex].IsUpdatedResource = true;
                    bForceUpdateSite = true;
                }

                if (bForceUpdateSite)
                {//buộc request
                    ChangeCityTo(iIndex, bForceUpdateSite);
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
    }
}