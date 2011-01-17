using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOCity
    {
        public DTOBuilding[] ListBuilding { get; set; }
        public DTOTroops[] ListTroopsUnits { get; set; }//quan bo
        public DTOTroops[] ListTroopsShips { get; set; }//quan thuy

        //townhall view
        public long GoldPerHour { get; set; } //(income - scientist)
        public int ResearchPointPerHour { get; set; }
        public long PopulationLimit { get; set; }
        public float PopulationGrow { get; set; }

        public int WoodLimit { get; set; }
        public int WineLimit { get; set; }
        public int MarbleLimit { get; set; }
        public int CrystalLimit { get; set; }
        public int SulphurLimit { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        public bool IsUpdatedResource = false;

        public float Wood { get; set; }
        public float Wine { get; set; }
        public float Marble { get; set; }
        public float Crystal { get; set; }
        public float Sulphur { get; set; }

        public int WoodPerHour { get; set; }
        public int WinePerHour { get; set; }
        public int MarblePerHour { get; set; }
        public int CrystalPerHour { get; set; }
        public int SulphurPerHour { get; set; }

        public float Population { get; set; }
        public float FreePopulation { get; set; }

        public int ActionPoint { get; set; }

        public int lvlWood { get; set; }
        public int lvlTradeGood { get; set; }
        public enum TRADE_GOOD_TYPE
        {
            WINE,
            MARBLE,
            CRYSTAL,
            SULPHUR
        }
        public TRADE_GOOD_TYPE TypeTradeGood { get; set; }

                //-------------------------------
        //quản lý thời gian update:
        // - gold
        public DateTime DTGold { get; set; }
        // - res cities
        // - building
        // - town hall
        public DateTime DTResourceCity { get; set; }
        public DateTime DTBuilding { get; set; }
        public DateTime DTTownHall { get; set; }

        public DateTime DTTroopUnits { get; set; }
        public DateTime DTTroopShips { get; set; }
    }
}
