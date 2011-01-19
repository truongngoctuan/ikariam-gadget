using System;
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
using System.IO;
using IkariamFramework.InterfaceToGadget;
using IkariamFramework.DebuggingAndTracking;

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
            // Login : trả về bool, thành công - thất bại
            // Hoặc tra về errorCode
            int errMsg = -1;
            Authenticated = false;
            try
            {
                errMsg = BUSAction.Login(username, password, server);
                if (errMsg == 0)
                {
                    string[] split = server.Split('.');
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    path = Path.GetDirectoryName(path);
                    Gloval.Dict = XmlHelper.LoadFile(string.Format(path + "\\Lang\\{0}.xml", split[1]));
                    Authenticated = true;

                    InitAutoRequest();

                    //Gloval.bEmpireOverviewIsNewData = false;
                    return 0;
                }
            }
            catch (Exception ex)
            {
            }
            if (errMsg == 0)
            {
                //xac dinh lang dua vao server                
            }

            Authenticated = false;
            return 1;
        }
        #endregion

        #region EmpireOverview
        EmpireOverviewUnit[] emptyEmpireOverviewUnits = new EmpireOverviewUnit[] {
            new EmpireOverviewUnit{TownName = "-", ID = 0, X = 0, Y = 0, ActionPoint = 0,
                                    FreePopulation = 0, Population = 0, PopulationLimit = 0,
                                    Wood = 0, WoodPerHour = 0, WoodLimit = 0,
                                    Wine = 0, WinePerHour = 0, WineLimit = 0,
                                    Marble = 0, MarblePerHour = 0, MarbleLimit = 0,
                                    Crystal = 0, CrystalPerHour = 0, CrystalLimit = 0,
                                    Sulphur = 0, SulphurPerHour = 0, SulphurLimit = 0,
                                    GoldPerHour = 0, ResearchPointPerHour = 0}
        };

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

        public static EmpireOverviewUnit[] CityToEmpire(DTOCity[] cities)
        {            
            List<EmpireOverviewUnit> EmpireOverviewUnitTemps = new List<EmpireOverviewUnit>();
            foreach (DTOCity city in cities)
            {
                EmpireOverviewUnit empireOverviewUnit = new EmpireOverviewUnit();
                empireOverviewUnit.TownName = city.Name;
                empireOverviewUnit.ID = city.ID;
                empireOverviewUnit.X = city.X;
                empireOverviewUnit.Y = city.Y;
                empireOverviewUnit.ActionPoint = city.ActionPoint;
                empireOverviewUnit.FreePopulation = (int)city.FreePopulation;
                empireOverviewUnit.Population = (int)city.Population;
                empireOverviewUnit.PopulationLimit = (int)city.PopulationLimit;
                empireOverviewUnit.Wood = (int)city.Wood;
                empireOverviewUnit.WoodPerHour = city.WoodPerHour; 
                empireOverviewUnit.WoodLimit = city.WoodLimit;
                empireOverviewUnit.Wine = (int)city.Wine; 
                empireOverviewUnit.WinePerHour = city.WinePerHour; 
                empireOverviewUnit.WineLimit = city.WineLimit;
                empireOverviewUnit.Marble = (int)city.Marble; 
                empireOverviewUnit.MarblePerHour = city.MarblePerHour; 
                empireOverviewUnit.MarbleLimit = city.MarbleLimit;
                empireOverviewUnit.Crystal = (int)city.Crystal; 
                empireOverviewUnit.CrystalPerHour = city.CrystalPerHour; 
                empireOverviewUnit.CrystalLimit = city.CrystalLimit;
                empireOverviewUnit.Sulphur = (int)city.Sulphur; 
                empireOverviewUnit.SulphurPerHour = city.SulphurPerHour; 
                empireOverviewUnit.SulphurLimit = city.SulphurLimit;
                empireOverviewUnit.GoldPerHour = (int)city.GoldPerHour;
                empireOverviewUnit.ResearchPointPerHour = city.ResearchPointPerHour;

                EmpireOverviewUnitTemps.Add(empireOverviewUnit);
            }
            return EmpireOverviewUnitTemps.ToArray();
        }


		public EmpireOverviewUnit GetEmptyEmpireOverviewUnit()
        {
            return new EmpireOverviewUnit();
        }
		
		public string GetEmpireOverviewUnits()
        {            
            return requestEmpireOverview();
            //return JsonConvert.SerializeObject(empireOverviewUnits, Formatting.Indented);
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
        List<TownOverviewUnit> emptyTownOverviewUnits = new List<TownOverviewUnit>
        {
            new TownOverviewUnit{TownName = "-", X = 0, Y = 0, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"Townhall", new DTOBuilding{Lvl = 0, Type = DTOBuilding.TYPE.Townhall}}
            }}
        };

        List<TownOverviewUnit> townOverviewUnits = new List<TownOverviewUnit> {
            new TownOverviewUnit{TownName = "Town1", X = 1, Y = 1, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"Townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"Townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"Academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"Warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"TradingPort", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"Museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
            new TownOverviewUnit{TownName = "Town2", X = 2, Y = 2, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"Townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"Townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"Academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"Warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"TradingPort", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"Museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
            new TownOverviewUnit{TownName = "Town3", X = 3, Y = 3, 
                Buildings = new Dictionary<string,DTOBuilding>{
                    {"Townhall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townhall}},
                    {"Townwall", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Townwall}},
                    {"Academy", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Academy}},
                    {"Warehouse", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Warehouse}},
                    {"TradingPort", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.TradingPort}},
                    {"Museum", new DTOBuilding{Lvl = 1, Type = DTOBuilding.TYPE.Museum}}
                }},
        };

        public static string GetTownOverviewUnits()
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
                    try
                    {
                        townOverviewUnit.Buildings.Add(building.Type.ToString(), building);
                    }
                    catch (Exception ex)
                    {
                        Debug.ErrorLogging(ex.Message);
                    }
                }

                townOverviewUnitsTemp.Add(townOverviewUnit);
            }
            string str = JsonConvert.SerializeObject(townOverviewUnitsTemp);
            return str;
        }

        #endregion

        #region TroopOverview
        List<TroopOverviewUnit> emptyTroopOverviewUnits = new List<TroopOverviewUnit> {
            new TroopOverviewUnit{
                TownName = "-", 
                X = 0, 
                Y = 0, 
                Troops = new Dictionary<string, DTOTroops>{                    
                },
                Ships = new Dictionary<string, DTOTroops>{
                }
            }
        };

        List<TroopOverviewUnit> troopOverviewUnits = new List<TroopOverviewUnit> {
            new TroopOverviewUnit{
                TownName = "Town1", 
                X = 1, 
                Y = 1, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"Balloon_Bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"Archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"Catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"Hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"Swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"Steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"Paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"Mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"Diving_Boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_Boat, Quality = 1}},
                    {"Ram_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ram_Ship, Quality = 1}},
                    {"Ballista_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"Catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
            new TroopOverviewUnit{
                TownName = "Town2", 
                X = 2, 
                Y = 2, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"Balloon_Bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"Archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"Catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"Hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"Swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"Steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"Paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"Mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"Diving_Boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_Boat, Quality = 1}},
                    {"Ram_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ram_Ship, Quality = 1}},
                    {"Ballista_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"Catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
            new TroopOverviewUnit{
                TownName = "Town3", 
                X = 3, 
                Y = 3, 
                Troops = new Dictionary<string, DTOTroops>{
                    {"Balloon_Bombardier", new DTOTroops{Type = DTOTroops.TYPE.Balloon_Bombardier, Quality = 2}},
                    {"Archer", new DTOTroops{Type = DTOTroops.TYPE.Archer, Quality = 1}},
                    {"Catapult", new DTOTroops{Type = DTOTroops.TYPE.Catapult, Quality = 1}},
                    {"Hoplite", new DTOTroops{Type = DTOTroops.TYPE.Hoplite, Quality = 1}},
                    {"Swordsman", new DTOTroops{Type = DTOTroops.TYPE.Swordsman, Quality = 1}},
                    {"Steam_Giant", new DTOTroops{Type = DTOTroops.TYPE.Steam_Giant, Quality = 1}},
                },
                Ships = new Dictionary<string, DTOTroops>{
                    {"Paddle_Wheel_Ram", new DTOTroops{Type = DTOTroops.TYPE.Paddle_Wheel_Ram, Quality = 2}},
                    {"Mortar_Ship", new DTOTroops{Type = DTOTroops.TYPE.Mortar_Ship, Quality = 1}},
                    {"Diving_Boat", new DTOTroops{Type = DTOTroops.TYPE.Diving_Boat, Quality = 1}},
                    {"Ram_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ram_Ship, Quality = 1}},
                    {"Ballista_Ship", new DTOTroops{Type = DTOTroops.TYPE.Ballista_Ship, Quality = 1}},
                    {"Catapult_Ship", new DTOTroops{Type = DTOTroops.TYPE.Catapult_Ship, Quality = 1}},
                }
            },
        };

        public static string GetTroopOverviewUnits()
        {
            List<TroopOverviewUnit> militaryOverviewUnitsTemp = new List<TroopOverviewUnit>();
            foreach (DTOCity dtoCity in Gloval.Database.Account.Cities)
            {
                TroopOverviewUnit militaryOverviewUnit = new TroopOverviewUnit();
                militaryOverviewUnit.TownName = dtoCity.Name;
                militaryOverviewUnit.X = dtoCity.X;
                militaryOverviewUnit.Y = dtoCity.Y;
                militaryOverviewUnit.Troops = new Dictionary<string, DTOTroops>();
                foreach (DTOTroops troop in dtoCity.ListTroopsUnits)
                {
                    militaryOverviewUnit.Troops.Add(troop.Type.ToString(), troop);
                }
                foreach (DTOTroops ship in dtoCity.ListTroopsShips)
                {
                    militaryOverviewUnit.Troops.Add(ship.Type.ToString(), ship);
                }

                militaryOverviewUnitsTemp.Add(militaryOverviewUnit);
            }
            string str = JsonConvert.SerializeObject(militaryOverviewUnitsTemp);
            return str;
        }

        #endregion

        #region Research
        static DTOResearch EmptyresearchOverviewUnit = new DTOResearch
        {
            Scientists = 100,
            ResearchPoints = 10000,
            ResearchPointsPerHour = 3600,
            Seafaring = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000, NeedDescription = "" },
            Economic = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 0, NeedDescription = "Ít nhất một yêu cầu chưa được nghiên cứu (công trình có thể tiếp theo: Xưởng mộc)" },
            Scientific = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000, NeedDescription = "" },
            Militaristic = new DTOResearchBranch { Name = "GoneWithTheWind", Description = "Bay cao bay xa", Need = 5000, NeedDescription = "" }
        };

        public static string GetResearchOverviewUnit()
        {
            //return JsonConvert.SerializeObject(EmptyresearchOverviewUnit);
            return JsonConvert.SerializeObject(Gloval.Database.Account.Research);
        }

        //public string GetResearchOverviewUnit()
        //{
        //    return JsonConvert.SerializeObject(researchOverviewUnit);
        //}
        #endregion

        #region Events
        static EventOverviewUnit[] emptyEventOverviewUnits = new EventOverviewUnit[]
        {
            new EventOverviewUnit{Town = "City1", Date = "5/5/2008", Message = "Please help me!!!", Type = "NEW"},
            new EventOverviewUnit{Town = "City1", Date = "5/5/2008", Message = "Please help me!!!", Type = "OLD"}
        };

        public static string GetEventOverviewUnits()
        {
            List<EventOverviewUnit> eventOverviewUnits = new List<EventOverviewUnit>();
            foreach(DTOEvent eventT in Gloval.Database.Account.Event)
            {
                EventOverviewUnit eventOverview = new EventOverviewUnit();
                eventOverview.Town = eventT.Town;
                eventOverview.Date = eventT.Date;
                eventOverview.Message = eventT.Message;
                eventOverview.Type = eventT.Type.ToString();

                eventOverviewUnits.Add(eventOverview);
            }
            return JsonConvert.SerializeObject(eventOverviewUnits);
        }
        #endregion

        #region Movements
        /*public string GetMovementOverviewUnits()
        {
            return JsonConvert.SerializeObject(Gloval.Database.Account.???);
        }*/
        #endregion

        #region Messages
        List<DTOMessage> EmptyDiplomatOverviewUnit = new List<DTOMessage> {
            new DTOMessage{Message = "hey, are you ready? ", Sender = "Ikariam Gadget"},
            new DTOMessage{Message = "fighting ...? ", Sender = "Ikariam Gadget"}};

        public static string GetMessageOverviewUnits()
        {
            return JsonConvert.SerializeObject(Gloval.Database.Account.Message);
        }
        #endregion

        #region AutoRequest
        Thread autoRequestThread = null;
        // nen de bien' nay` len GlobarVar luon cho tien
        public volatile bool bStopAutoRequest = true;
        public void InitAutoRequest()
        {
            bStopAutoRequest = true;
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
                try
                {
                    autoRequestThread.Abort();
                }
                catch (Exception ex)
                {
                    Debug.ErrorLogging(ex.Message);
                }
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
            Event = 16,
            Gold_page = 32,
            Building = 64,
            };
        RequestTarget requestTarget = RequestTarget.None;
        int DefaultAutoRequestTime = 15000; //1 minutes
        // ===============================
        private void ThreadWorker()
        {
            try
            {
                //init vai ham truoc khi khoi tao
                BUSCity.requestCities();
                //cap nhat danh sach

                requestTarget = 0;
                requestTarget |= RequestTarget.Towns;
                requestTarget |= RequestTarget.Building;
                requestTarget |= RequestTarget.Troops;

                requestTarget |= RequestTarget.Diplomacy;
                requestTarget |= RequestTarget.Research;
                requestTarget |= RequestTarget.Event;

                makeRequest();
                //dang starting nen ko cho client tiep can
                //xem nhu server down

                bStopAutoRequest = false;
                requestTarget = 0;
                while (!bStopAutoRequest)
                {
                    //DEBUG("starting request...");
                    //Request requestTarget 
                    //BUS.RequestSomething();
                    //Update requestTarget
                    //requestTarget = RequestTarget.Towns;

                    makeRequest();

                    requestTarget = (RequestTarget)GetNextRequest();
                    //DEBUG("done request...");
                    Thread.Sleep(RequestTime); //Sua thanh wait de
                }
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
            }
        }
        int RequestTime = 15000;
        int GetNextRequest()
        {
            RequestTime = DefaultAutoRequestTime; //
            //binh thuong se la trang vang
            //neu de goto city se dc loi 1 lan request khi can vao townhall
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
            try
            {
                //go to gold_page 
                if ((requestTarget & RequestTarget.Gold_page) != 0)
                {
                    //kiem tra xem co adv nao active hay ko, 
                    //neu co bo sung vao requestTarget de cap nhat
                    //ngay lap tuc, khong doi lan request sau
                    BUSAction.AutoLoadDefaultPage();
                }

                int iAdvstatus = BUSAction.CheckAdvStatus();
                if ((iAdvstatus & (int)DTOAccount.ADV_ACTIVE.MAYOR) != 0)
                {
                    requestTarget |= RequestTarget.Event;
                    requestTarget |= RequestTarget.Towns;
                    
                    //requestTarget |= RequestTarget.Building;
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

                //if then else request tung cai' trong request target
                //hakuna
                if ((requestTarget & RequestTarget.Towns) != 0)
                {//res + town hall
                    BUSAction.AutoRequestEmpireOverview();
                }
                if ((requestTarget & RequestTarget.Building) != 0)
                {
                    BUSAction.AutoRequestBuildings();
                }
                if ((requestTarget & RequestTarget.Troops) != 0)
                {
                    BUSAction.AutoRequestTroops();
                }
                if ((requestTarget & RequestTarget.Research) != 0)
                {
                    BUSAction.AutoRequestResearch();
                }
                if ((requestTarget & RequestTarget.Diplomacy) != 0)
                {
                    BUSAction.AutoRequestDiplomat();
                }
                if ((requestTarget & RequestTarget.Event) != 0)
                {
                    BUSAction.AutoRequestEvent();
                }

                //-----------------------------------------
                //debug
                DBnRequestServer++;
                DEBUG("request server: " + DBnRequestServer.ToString() + " " + requestTarget.ToString());
                Debug.Logging("request server: " + DBnRequestServer.ToString() + " " + requestTarget.ToString());
                //-----------------------------------------
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
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

        //thu tu request thong thuong:
        //requestcode --> requestempireoverview

        //test
        //requestcode --> requestempireoverview --> requestDEBUG

        public int requestCode()
        {
            //debug
            DBnRequestClient++;

            //server down !!!
            if (bStopAutoRequest)
            {
                DEBUG("server down !!!");
                Debug.Logging("request server: " + DBnRequestServer.ToString() + " client request: " + DBnRequestClient.ToString() + " result: -1");
                return -1;
            }

            int iCode = 0;
            if (Gloval.bEmpireOverviewIsNewData) iCode |= 1;
            if (Gloval.bBuildingsOverviewIsNewData) iCode |= 2;
            if (Gloval.bTroopsOverviewIsNewData) iCode |= 4;
            if (Gloval.bResearchOverviewIsNewData) iCode |= 8;
            if (Gloval.bDiplomatOverviewIsNewData) iCode |= 16;
            if (Gloval.bEventOverviewIsNewData) iCode |= 32;

            DEBUG("request server: " + DBnRequestServer.ToString() + " client request: " + DBnRequestClient.ToString() + " result: " + iCode.ToString());
            Debug.Logging("request server: " + DBnRequestServer.ToString() + " client request: " + DBnRequestClient.ToString() + " result: " + iCode.ToString());
            return iCode;
        }

        public string requestDEBUGString()
        {
            return DBcmd.ToString();
        }

        public string requestEmpireOverview()
        {
            try
            {
                return BUSAction.requestTownsFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(emptyEmpireOverviewUnits); 
            }
        }

        public string requestBuildingsOverview()
        {
            try
            {
                return BUSAction.requestBuildingsFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(emptyTownOverviewUnits);
            }
        }

        public string requestTroopsOverview()
        {
            try
            {
                return BUSAction.requestTroopsFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(emptyTroopOverviewUnits);
            }
        }

        public string requestResearchOverview()
        {
            try
            {
                return BUSAction.requestResearchFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(EmptyresearchOverviewUnit);
            }
        }

        public string requestDiplomatOverview()
        {
            try
            {
                return BUSAction.requestDiplomatFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(EmptyDiplomatOverviewUnit);
            }
        }

        public string requestEventOverview()
        {
            try
            {
                return BUSAction.requestEventFromGadget();
            }
            catch (Exception ex)
            {
                Debug.ErrorLogging(ex.Message);
                return JsonConvert.SerializeObject(emptyEventOverviewUnits);
            }
        }
        #endregion

        #region variable for debug

        static StringBuilder DBcmd = new StringBuilder(5000);
        public static int DBnRequestServer = 0;
        public static int DBnRequestClient = 0;

        void DEBUG(string str)
        {
            DBcmd.Insert(0, str + "\r\n");
        }
        #endregion

        
    }
}
