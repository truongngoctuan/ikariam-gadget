using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOTroops
    {
        public DTOTroops()
        {
            Type = TROOPS_TYPE.UNKNOW;
            Quality = 0;
        }
        public bool IsUnits { get; set; }

        public enum TROOPS_TYPE
        {
            UNKNOW,

            //unit type
            Hoplite,
            Steam_Giant,
            Spearmen,
            Swordsman,
            Slinger,
            Archer,
            Sulphur_Carabineers,

            Battering_ram,
            Catapult,
            Mortar,
            Gyrocopter,
            Balloon_Bombardier,
            Cook,
            Doctor,

            //ships type
            Ram_Ship,
            Fire_Ship,
            Paddle_Wheel_Ram,
            Ballista_ship,

            Catapult_Ship,
            Mortar_Ship,
            Diving_boat
        }
        public TROOPS_TYPE Type { get; set; }
        public int Quality { get; set; }
    }
}
