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
            Type = BUILDING_TYPE.UNKNOW;
        }
        public bool IsBuilding { get; set; }

        public enum BUILDING_TYPE
        {
            UNKNOW,
            townHall,
            museum,
            palace,
            vineyard,
            tavern,
            safehouse,
            architect,
            dump,
            academy,
            forester,
            winegrower,
            carpentering,
            wall,
            port,
            shipyard,

            palaceColony,
            stonemason,
            barracks,
            fireworker,

            warehouse,
            temple,
            glassblowing,
            optician,
            alchemist,
            embassy,
            branchOffice,
            workshop,

            flag

        }
        public BUILDING_TYPE Type { get; set; }
        public int Lvl { get; set; }

        public string Time { get; set; }
    }
}
