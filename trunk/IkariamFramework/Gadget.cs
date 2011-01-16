﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Threading;
using IkariamFramework.BUSIkariamFramework;
using IkariamFramework.DTOIkariamFramework;
using IkariamFramework.PresentationUnit;
using Newtonsoft.Json;

namespace IkariamFramework
{
    /// <summary>
    /// A Gateway for receiving player info
    /// </summary>
    [ComVisible(true)]
    public class Gadget : IDisposable
    {
        public bool Authenticated { get; private set; }
        #region Constructor
        public Gadget()
        {
            Authenticated = false;
        }
        #endregion

        #region Login
        public int Login(string username, string password, string server)
        {
            //int errCode = BUSAction.Login(username, password, server);
            int errCode = 0;
            if (errCode == 0)
            {
                Authenticated = true;
                Gloval.bEmpireOverviewIsNewData = false;
            }
            else
                Authenticated = false;
            return errCode;
        }
        #endregion

        #region EmpireOverview
        EmpireOverviewUnit[] empireOverviewUnits = new EmpireOverviewUnit[] {
            new EmpireOverviewUnit{TownName = "City1", ID = 1, X = 1, Y = 1, ActionPoint = 1,
                                    FreePopulation = 100, Population = 200, PopulationLimit = 1000,
                                    Wood = 1000, WoodPerHour = 3600, WoodLimit = 100000,
                                    Wine = 1000, WinePerHour = 3600, WineLimit = 100000,
                                    Marble = 1000, MarblePerHour = 3600, MarbleLimit = 100000,
                                    Crystal = 1000, CrystalPerHour = 3600, CrystalLimit = 100000,
                                    Sulphur = 1000, SulphurPerHour = 3600, SulphurLimit = 100000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600},
            new EmpireOverviewUnit{TownName = "City2", ID = 2, X = 2, Y = 2, ActionPoint = 2,
                                    FreePopulation = 200, Population = 200, PopulationLimit = 2000,
                                    Wood = 2000, WoodPerHour = 3600, WoodLimit = 200000,
                                    Wine = 2000, WinePerHour = 3600, WineLimit = 200000,
                                    Marble = 2000, MarblePerHour = 3600, MarbleLimit = 200000,
                                    Crystal = 2000, CrystalPerHour = 3600, CrystalLimit = 200000,
                                    Sulphur = 2000, SulphurPerHour = 3600, SulphurLimit = 200000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600},
            new EmpireOverviewUnit{TownName = "City3", ID = 3, X = 3, Y = 3, ActionPoint = 3,
                                    FreePopulation = 300, Population = 300, PopulationLimit = 3000,
                                    Wood = 3000, WoodPerHour = 3600, WoodLimit = 300000,
                                    Wine = 3000, WinePerHour = 3600, WineLimit = 300000,
                                    Marble = 3000, MarblePerHour = 3600, MarbleLimit = 300000,
                                    Crystal = 3000, CrystalPerHour = 3600, CrystalLimit = 300000,
                                    Sulphur = 3000, SulphurPerHour = 3600, SulphurLimit = 300000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600}
        };

		public EmpireOverviewUnit GetEmptyEmpireOverviewUnit()
        {
            return new EmpireOverviewUnit();
        }
		
		public string GetEmpireOverviewUnits()
        {
            return JsonConvert.SerializeObject(empireOverviewUnits, Formatting.Indented);
        }
		
        public int GetEmpireOverviewUnitNum()
        {
            return empireOverviewUnits.Length;
        }
        

        public EmpireOverviewUnit EmpireOverviewUnit(int index)
        {
            Thread.Sleep(1000);
            if (0 <= index && index <= empireOverviewUnits.Length)
            {
                return empireOverviewUnits[index];
            }
            return null;
        }
        #endregion

        #region TownOverview
        List<TownOverviewUnit> townOverviewUnits = new List<TownOverviewUnit> {
            new TownOverviewUnit{TownName = "Town1", X = 1, Y = 1, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"tradingport", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
            new TownOverviewUnit{TownName = "Town2", X = 2, Y = 2, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"tradingport", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
            new TownOverviewUnit{TownName = "Town3", X = 3, Y = 3, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"tradingport", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
        };

        public string GetTownOverviewUnits()
        {
            // Tham khao ham` nay` de convert tu` dang list sang Dictionary
            List<TownOverviewUnit> townOverviewUnitsTemp = new List<TownOverviewUnit>();
            foreach(DTOCity dtoCity in Gloval.Database.Account.Cities)
            {
                TownOverviewUnit townOverviewUnit = new TownOverviewUnit();
                townOverviewUnit.TownName = dtoCity.Name;
                townOverviewUnit.X = dtoCity.X;
                townOverviewUnit.Y = dtoCity.Y;
                foreach (DTOBuilding building in dtoCity.ListBuilding)
                {
                    townOverviewUnit.Buildings.Add(building.Type.ToString(), building);
                }                
            }
            return JsonConvert.SerializeObject(townOverviewUnitsTemp);
        }

        public string GetTownOverviewUnitsOld()
        {
            return JsonConvert.SerializeObject(townOverviewUnits);
        }
        #endregion

        #region MilitaryOverview
        List<MilitaryOverviewUnit> militaryOverviewUnits = new List<MilitaryOverviewUnit> {
            new MilitaryOverviewUnit{
                TownName = "Town1", 
                X = 1, 
                Y = 1, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"diving_boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_boat, Quality = 1}},
                    {"battering_ram", new DTOTroops{Type = DTOTroops.TYPE.Ram, Quality = 1}},
                    {"ballista_ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
            new MilitaryOverviewUnit{
                TownName = "Town2", 
                X = 2, 
                Y = 2, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"diving_boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_boat, Quality = 1}},
                    {"battering_ram", new DTOTroops{Type = DTOTroops.TYPE.Ram, Quality = 1}},
                    {"ballista_ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
            new MilitaryOverviewUnit{
                TownName = "Town3", 
                X = 3, 
                Y = 3, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"diving_boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_boat, Quality = 1}},
                    {"battering_ram", new DTOTroops{Type = DTOTroops.TYPE.Ram, Quality = 1}},
                    {"ballista_ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
        };

        public string GetMilitaryOverviewUnit()
        {
            List<MilitaryOverviewUnit> militaryOverviewUnitsTemp = new List<MilitaryOverviewUnit>();
            foreach (DTOCity dtoCity in Gloval.Database.Account.Cities)
            {
                MilitaryOverviewUnit militaryOverviewUnit = new MilitaryOverviewUnit();
                militaryOverviewUnit.TownName = dtoCity.Name;
                militaryOverviewUnit.X = dtoCity.X;
                militaryOverviewUnit.Y = dtoCity.Y;
                foreach (DTOTroops troop in dtoCity.ListTroopsUnits)
                {
                    militaryOverviewUnit.Troops.Add(troop.Type.ToString(), troop);
                }
                foreach (DTOTroops ship in dtoCity.ListTroopsShips)
                {
                    militaryOverviewUnit.Troops.Add(ship.Type.ToString(), ship);
                }
            }
            return JsonConvert.SerializeObject(militaryOverviewUnits);
        }

        public string GetMilitaryOverviewUnitOld()
        {
            return JsonConvert.SerializeObject(militaryOverviewUnits);
        }
        #endregion

        #region Research
        DTOResearch researchOverviewUnit = new DTOResearch
        {
            Scientists = 100,
            ResearchPoints = 10000,
            ResearchPointsPerHour = 3600,
            Seafaring = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000},
            Economic = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000 },
            Scientific = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000 },
            Militaristic = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000 }            
        };

        public string GetResearchOverviewUnit()
        {
            return JsonConvert.SerializeObject(researchOverviewUnit);
        }
        #endregion

        #region AutoRequest
        Thread autoRequestThread = null;
        // nen de bien' nay` len GlobarVar luon cho tien
        public volatile bool bStopAutoRequest = false;
        public void InitAutoRequest()
        {
            if (autoRequestThread == null)
            {
                autoRequestThread = new Thread(new ThreadStart(ThreadWorker));
                autoRequestThread.Start();
            }
        }        
        public void StopAutoRequest()
        {
            if (autoRequestThread != null)
            {
                bStopAutoRequest = true;
                autoRequestThread.Abort();
                autoRequestThread = null;
            }
        }

        // ===============================
        // tach cai' cuc. nay` ra ham` cua mai`
        enum RequestTarget { None = 0, 
            Towns = 1, 
            Troops = 2, 
            Research = 4, 
            Diplomacy = 8, 
            //All = 16,
            Gold_page = 32,
            Building = 64
            };
        RequestTarget requestTarget = RequestTarget.None;
        int nextRequestIn = 3600000; //1 minutes
        // ===============================
        private void ThreadWorker()
        {
            // ===============================
            // tack cuc nay` ra ham` cua mai` ben lop' nao` do'
            requestTarget = (RequestTarget)64;

            while (!bStopAutoRequest)
            {
                //Request requestTarget 
                //BUS.RequestSomething();
                //Update requestTarget
                //requestTarget = RequestTarget.Towns;

                makeRequest();

                requestTarget = (RequestTarget)GetNextRequest(out nextRequestIn);
                Thread.Sleep(nextRequestIn); //Sua thanh wait de
            }
            // ===============================
        }

        int GetNextRequest(out int tNextRequest)
        {
            tNextRequest = 60000;
            int iNextRequest = (int)RequestTarget.Gold_page;

            //kiem tra adv xem co can request trong lan tiep theo hay khong
            //khong can nua, mac dinh la request gold_page rùi

            //kiem tra cac su kien move, xay xong nha, co su kien nao nho hon 
            //tNexRequest mac dinh hay ko, 
            //neu co add them vao iNextRequest

            return iNextRequest;
        }

        void makeRequest()
        {
            //go to gold_page 
            if ((requestTarget & RequestTarget.Gold_page) != 0)
            {
                //kiem tra xem co adv nao active hay ko, 
                //neu co bo sung vao requestTarget de cap nhat
                //ngay lap tuc, khong doi lan request sau
                BUSAction.AutoLoadDefaultPage();
                int iAdvstatus = BUSAction.CheckAdvStatus();
                if ((iAdvstatus & (int)DTOAccount.ADV_ACTIVE.MAYOR) != 0)
                {
                    requestTarget |= RequestTarget.Towns;
                }

                if ((iAdvstatus & (int)DTOAccount.ADV_ACTIVE.GENERAL) != 0)
                {
                    requestTarget |= RequestTarget.Troops;
                    //check thêm move
                }
                if ((iAdvstatus & (int)DTOAccount.ADV_ACTIVE.SCIENTIST) != 0)
                {
                    requestTarget |= RequestTarget.Research;
                }
                if ((iAdvstatus & (int)DTOAccount.ADV_ACTIVE.DIPLOMAT) != 0)
                {
                    requestTarget |= RequestTarget.Diplomacy;
                }
            }
            
            //if then else request tung cai' trong request target
            if ((requestTarget & RequestTarget.Towns) != 0)
            {
                BUSAction.AutoRequestTowns();
                Gloval.bEmpireOverviewIsNewData = true;
            }
            if ((requestTarget & RequestTarget.Building) != 0)
            {
                BUSAction.AutoRequestBuildings();
                Gloval.bEmpireOverviewIsNewData = true;
            }
            if ((requestTarget & RequestTarget.Research) != 0)
            {
            }
            if ((requestTarget & RequestTarget.Troops) != 0)
            {
            }
            if ((requestTarget & RequestTarget.Diplomacy) != 0)
            {
            }
        }
        #endregion

        #region ErrorMessages
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
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Nothing to do here.
        }

        #endregion

        #region interface method gadget use

        public string requestEmpireOverview()
        {
            return BUSAction.requestTownsFromGadget();
        }
        #endregion
    }
}