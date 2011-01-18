using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IkariamFramework.DAOIkariamFramework;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;
using IkariamFramework.PresentationUnit;
using Newtonsoft.Json;

namespace IkariamFramework.BUSIkariamFramework
{
    [ComVisible(true)]
    public class BUSAction
    {
        public static string InnerHTML()
        {
            return Gloval.Database.DocumentNode.InnerHtml;
        }

        public static string InnerText()
        {
            return Gloval.Database.DocumentNode.InnerText;
        }

        public static int Login(string username, string password, string server)
        {
            // Login : trả về bool, thành công - thất bại
            // Hoặc tra về errorCode
            if (DAOAccount.Login(username, password, server))
            {
                return 0;
            }
            return 1;
        }

        public static void Logout()
        {
            DAOAccount.Logout();
        }

        //gold
        //public static long GetTotalGold(bool bForceUpdate)
        //{
        //    if (bForceUpdate)
        //    {
        //        DAOAccount.GoToGoldPage();
        //        DAOAccount.GetTotalGold();

        //        return Gloval.Database.Account.TotalGold;
        //    }
        //    else
        //    {
        //        return Gloval.Database.Account.TotalGold;
        //    }
        //}

        //public static long GetTotalGoldPerHour()
        //{//mac dinh ham nay di sau gettotal nen ko can forceupdate
        //    return Gloval.Database.Account.TotalGoldPerHour;
        //}

        #region adv status
        public static int CheckAdvStatus()
        {
            return DAOAccount.CheckAdvStatus();
        }

        public static void AutoLoadDefaultPage()
        {
            DAOAccount.GoToGoldPage();
            Gloval.Database.CurrentView = Data.SITE_VIEW.GOLD_PAGE;
        }
        #endregion

        #region auto - scenario request
        public static void AutoRequestEmpireOverview()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestEmpireOverview start");
            //get res all city
            int nCities = Gloval.Database.Account.Cities.Count();
            for (int i = 0; i < nCities; i++)
            {
                BUSCity.GetResourceCity(i);
                BUSCity.requestTownHall(i);
            }

            Gloval.bEmpireOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestEmpireOverview Done");
        }

        public static void AutoRequestBuildings()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestBuildings start");
            //force update building
            int nCities = Gloval.Database.Account.Cities.Count();

            //giảm dc 1 request
            if (Gloval.Database.CurrentCity == 0)
            {
                for (int i = 0; i < nCities; i++)
                {
                    BUSBuilding.requestBuilding(i);
                }
            }
            else
            {
                for (int i = nCities - 1; i >= 0; i--)
                {
                    BUSBuilding.requestBuilding(i);
                }
            }

            Gloval.bBuildingsOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestBuildings start");
        }

        public static void AutoRequestTroops()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestTroops - Units start");
            //force unit
            int nCities = Gloval.Database.Account.Cities.Count();
            //giảm dc 1 request
            if (Gloval.Database.CurrentCity == 0)
            {
                for (int i = 0; i < nCities; i++)
                {
                    BUSTroops.requestUnits(i);
                }
            }
            else
            {
                for (int i = nCities - 1; i >= 0; i--)
                {
                    BUSTroops.requestUnits(i);
                }
            }

            DebuggingAndTracking.Debug.Logging("AutoRequestTroops - Units done");
            DebuggingAndTracking.Debug.Logging("AutoRequestTroops - Ships start");

            //giảm dc 1 request
            if (Gloval.Database.CurrentCity == 0)
            {
                for (int i = 0; i < nCities; i++)
                {
                    BUSTroops.requestShips(i);
                }
            }
            else
            {
                for (int i = nCities - 1; i >= 0; i--)
                {
                    BUSTroops.requestShips(i);
                }
            }

            Gloval.bTroopsOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestTroops - Ships done");
        }

        public static void AutoRequestResearch()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestResearch start");
            BUSResearch.requestResearch();
            Gloval.bResearchOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestResearch done");
        }

        public static void AutoRequestDiplomat()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestDiplomat start");
            BUSMessage.requestMessage();
            Gloval.bDiplomatOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestDiplomat done");
        }

        public static void AutoRequestEvent()
        {
            DebuggingAndTracking.Debug.Logging("AutoRequestEvent start");
            BUSEvent.requestEvent();
            Gloval.bEventOverviewIsNewData = true;
            DebuggingAndTracking.Debug.Logging("AutoRequestEvent done");
        }
        #endregion 

        #region gadget request data
        public static string requestTownsFromGadget()
        {
            //if (Gloval.bEmpireOverviewIsNewData)
            //{
                ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien ta
                BUSCity.CalculateFromLocalData();

                //cap nhat lai thanh du lieu cu
                //de gadget khong lay lai lan nua
                Gloval.bEmpireOverviewIsNewData = false;

                //lay du lieu moi update cho gadget
                //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
                //return "new data";
            string str = JsonConvert.SerializeObject(Gadget.CityToEmpire(Gloval.Database.Account.Cities));
            DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }

        public static string requestBuildingsFromGadget()
        {
            //if (Gloval.bBuildingsOverviewIsNewData)
            //{
                ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien tai
                //hien gio chua xay dung colddown cho các nhà đang xây dựng
                //nen khong co lam phan nay
                //BUSCity.CalculateFromLocalData();

                //cap nhat lai thanh du lieu cu
                //de gadget khong lay lai lan nua
                Gloval.bBuildingsOverviewIsNewData = false;

                //lay du lieu moi update cho gadget
                //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
                //return "new data";
                string str = Gadget.GetTownOverviewUnits();
                DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }

        public static string requestTroopsFromGadget()
        {
            //if (Gloval.bBuildingsOverviewIsNewData)
            //{
            ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien tai
            //hien gio chua xay dung colddown cho các nhà đang xây dựng
            //nen khong co lam phan nay
            //BUSCity.CalculateFromLocalData();

            //cap nhat lai thanh du lieu cu
            //de gadget khong lay lai lan nua
            Gloval.bTroopsOverviewIsNewData = false;

            //lay du lieu moi update cho gadget
            //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
            //return "new data";
            string str = Gadget.GetTroopOverviewUnits();
            DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }

        public static string requestResearchFromGadget()
        {
            //if (Gloval.bBuildingsOverviewIsNewData)
            //{
            ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien tai
            //hien gio chua xay dung colddown cho các nhà đang xây dựng
            //nen khong co lam phan nay
            BUSResearch.CalculateFromLocalData();

            //cap nhat lai thanh du lieu cu
            //de gadget khong lay lai lan nua
            Gloval.bResearchOverviewIsNewData = false;

            //lay du lieu moi update cho gadget
            //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
            //return "new data";
            string str = Gadget.GetResearchOverviewUnit();
            DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }

        public static string requestDiplomatFromGadget()
        {
            //if (Gloval.bBuildingsOverviewIsNewData)
            //{
            ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien tai
            //hien gio chua xay dung colddown cho các nhà đang xây dựng
            //nen khong co lam phan nay
            //BUSResearch.CalculateFromLocalData();

            //cap nhat lai thanh du lieu cu
            //de gadget khong lay lai lan nua
            Gloval.bDiplomatOverviewIsNewData = false;

            //lay du lieu moi update cho gadget
            //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
            //return "new data";
            string str = Gadget.GetMessageOverviewUnits();
            DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }

        public static string requestEventFromGadget()
        {
            //if (Gloval.bBuildingsOverviewIsNewData)
            //{
            ////cap nhat thong tin dang luu tru phu` hop voi thoi diem hien tai
            //hien gio chua xay dung colddown cho các nhà đang xây dựng
            //nen khong co lam phan nay
            //BUSResearch.CalculateFromLocalData();

            //cap nhat lai thanh du lieu cu
            //de gadget khong lay lai lan nua
            Gloval.bEventOverviewIsNewData = false;

            //lay du lieu moi update cho gadget
            //return JsonConvert.SerializeObject(Gloval.Database.Account.Cities);
            //return "new data";
            string str = Gadget.GetEventOverviewUnits();
            DebuggingAndTracking.Debug.Logging(str);
            return str;
            //}

            //return "";
        }
        #endregion
    }
}
