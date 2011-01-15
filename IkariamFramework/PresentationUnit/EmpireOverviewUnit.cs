using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IkariamFramework.PresentationUnit
{
    [Serializable, ComVisible(true)]
    public class EmpireOverviewUnit
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ActionPoint { get; set; }

        public int FreePopulation { get; set; }
        public int Population { get; set; }
        public int PopulationLimit { get; set; }

        public int Wood { get; set; }
        public int WoodPerHour { get; set; }
        public int WoodLimit { get; set; }

        public int Wine { get; set; }
        public int WinePerHour { get; set; }
        public int WineLimit { get; set; }

        public int Marble { get; set; }
        public int MarblePerHour { get; set; }
        public int MarbleLimit { get; set; }

        public int Crystal { get; set; }
        public int CrystalPerHour { get; set; }
        public int CrystalLimit { get; set; }
        
        public int Sulphur { get; set; }
        public int SulphurPerHour { get; set; }
        public int SulphurLimit { get; set; }

        //public int LvlWood { get; set; }
        //public int LvlTradeGood { get; set; }
            
        //public TRADE_GOOD_TYPE TypeTradeGood { get; set; }

        public int ResearchPointPerHour { get; set; }
        public int GoldPerHour { get; set; }        
    }
}
