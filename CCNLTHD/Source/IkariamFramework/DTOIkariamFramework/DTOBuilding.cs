using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOBuilding
    {
        public DTOBuilding()
        {
            IsBuilding = false;
            Type = TYPE.Unknown;
        }
        public bool IsBuilding { get; set; }

        public enum TYPE
        {
            Unknown,

            Townhall,
            Museum,
            Palace,
            Winepress,
            Tavern,
            Hideout,
            Architect,
            Dump,
            Academy,
            Forester,
            Winegrower,
            Carpenter,
            Townwall,
            TradingPort,
            Shipyard,

            PalaceColony,
            Stonemason,
            Barracks,
            Firework,

            Warehouse,
            Temple,
            GlassBlower,
            Optician,
            Alchemist,
            Embassy,
            TradingPost,
            Workshop,

            Flag
        }
        public TYPE Type { get; set; }
        public int Lvl { get; set; }

        public string Time { get; set; }
    }
}
