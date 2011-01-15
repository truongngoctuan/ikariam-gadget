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
                Authenticated = true;
            else
                Authenticated = false;
            return errCode;
        }
        #endregion

        #region EmpireOverview
        EmpireOverviewUnit[] empireOverviewUnits = new EmpireOverviewUnit[] {
            new EmpireOverviewUnit{Name = "City1", ID = 1, X = 1, Y = 1, ActionPoint = 1,
                                    FreePopulation = 100, Population = 200, PopulationLimit = 1000,
                                    Wood = 1000, WoodPerHour = 3600, WoodLimit = 100000,
                                    Wine = 1000, WinePerHour = 3600, WineLimit = 100000,
                                    Marble = 1000, MarblePerHour = 3600, MarbleLimit = 100000,
                                    Crystal = 1000, CrystalPerHour = 3600, CrystalLimit = 100000,
                                    Sulphur = 1000, SulphurPerHour = 3600, SulphurLimit = 100000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600},
            new EmpireOverviewUnit{Name = "City2", ID = 2, X = 2, Y = 2, ActionPoint = 2,
                                    FreePopulation = 200, Population = 200, PopulationLimit = 2000,
                                    Wood = 2000, WoodPerHour = 3600, WoodLimit = 200000,
                                    Wine = 2000, WinePerHour = 3600, WineLimit = 200000,
                                    Marble = 2000, MarblePerHour = 3600, MarbleLimit = 200000,
                                    Crystal = 2000, CrystalPerHour = 3600, CrystalLimit = 200000,
                                    Sulphur = 2000, SulphurPerHour = 3600, SulphurLimit = 200000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600},
            new EmpireOverviewUnit{Name = "City3", ID = 3, X = 3, Y = 3, ActionPoint = 3,
                                    FreePopulation = 300, Population = 300, PopulationLimit = 3000,
                                    Wood = 3000, WoodPerHour = 3600, WoodLimit = 300000,
                                    Wine = 3000, WinePerHour = 3600, WineLimit = 300000,
                                    Marble = 3000, MarblePerHour = 3600, MarbleLimit = 300000,
                                    Crystal = 3000, CrystalPerHour = 3600, CrystalLimit = 300000,
                                    Sulphur = 3000, SulphurPerHour = 3600, SulphurLimit = 300000,
                                    GoldPerHour = 3600, ResearchPointPerHour = 3600}
        };

       
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
    }
}
