using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IkariamFramework.DAOIkariamFramework;
using HtmlAgilityPack;
using IkariamFramework.DTOIkariamFramework;
using IkariamFramework.PresentationUnit;

namespace IkariamFramework.BUSIkariamFramework
{
    [ComVisible(true)]
    public class BUSAction: IDisposable
    {
        public BUSAction()
        {
        }

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

        public String btadvCities_Click()
        {
            HtmlNode node = Gloval.Database.Document.DocumentNode.SelectNodes("html/body/div/div/div[13]/ul/li/a")[0];
            string strhref = node.GetAttributeValue("href", "err");
            BaseFunction.GetHtmlSite("http://s15.en.ikariam.com/index.php" + strhref);
            //cap nhat oldview
            Gloval.Database.strOldView = strhref.Substring(strhref.IndexOf("oldView=") + 8);
            return Gloval.Database.strOldView;
        }

        public string GetErrorMessage(int errMessageCode)
        {
            string errorMessage = null;
            if (errors.TryGetValue(errMessageCode, out errorMessage))
                return errorMessage;
            return null;
        }

        Dictionary<int, string> errors = new Dictionary<int, string>
        {
            {0, "No errors"},
            {1, "Unknown errors"},
            {2, "Username or password incorrect"},
            {3, "Connection timeout"},
            {4, "Username and password can't be empty"}
        };

        #region IDisposable Members

        public void Dispose()
        {
            // Nothing to do here.
        }

        #endregion

        //gold
        public static long GetTotalGold(bool bForceUpdate)
        {
            if (bForceUpdate)
            {
                DAOAccount.GoToGoldPage();
                DAOAccount.GetTotalGold();

                return Gloval.Database.Account.TotalGold;
            }
            else
            {
                return Gloval.Database.Account.TotalGold;
            }
        }

        public static long GetTotalGoldPerHour()
        {//mac dinh ham nay di sau gettotal nen ko can forceupdate
            return Gloval.Database.Account.TotalGoldPerHour;
        }

        //adv statuc
        public static int CheckAdvStatus()
        {
            return DAOAccount.CheckAdvStatus();
        }

        public static void AutoLoadDefaultPage()
        {
            DAOAccount.GoToGoldPage();
        }

        #region auto - scenario request
        public static void AutoRequestTowns()
        {
            //get res all city
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                BUSCity.GetResourceCity(i, true);
            }

            //get townhall inf all city
            for (int i = 0; i < nCities; i++)
            {
                BUSCity.GetTownHallInfomationInCity(i, true);
            }

            Gloval.bEmpireOverviewIsNewData = true;
        }

        public static void AutoRequestBuildings()
        {
            //force update building
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                BUSBuilding.ForceUpdate(i);
            }

            Gloval.bEmpireOverviewIsNewData = true;
        }

        public static void AutoRequestTroops()
        {
            //force unit
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                BUSTroops.ForceUpdateUnits(i);
            }

            //force update ships
            for (int i = 0; i < nCities; i++)
            {
                BUSTroops.ForceUpdateShips(i);
            }
        }

        public static void AutoRequestResearch()
        {
            BUSResearch.ForceUpdate();
        }

        public static void AutoRequestDiplomat()
        {
            BUSMessage.ForceUpdate();
        }
        #endregion 

        #region gadget request data
        public static string requestTownsFromGadget()
        {
            if (Gloval.bEmpireOverviewIsNewData)
            {
                //cap nhat thong tin dang luu tru phu` hop voi thoi diem hien ta
                BUSCity.CalculateResourceFromLocalData();

                //cap nhat lai thanh du lieu cu
                //de gadget khong lay lai lan nua
                Gloval.bEmpireOverviewIsNewData = false;

                //lay cho gadget
                return JSONConverter.toEmpireOverviewUnitJSON();
            }

            return "";
        }
        #endregion
    }
}
