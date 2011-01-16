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
            Type = TYPE.Unknown;
            Quality = 0;
        }
        public bool IsUnits { get; set; }

        public enum TYPE
        {
            Unknown,

            //unit type
            Hoplite,
            Steam_Giant,
            Spearman,
            Swordsman,
            Slinger,
            Archer,
            Sulphur_Carabineer,

            Ram,
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
            Ballista_Ship,

            Catapult_Ship,
            Mortar_Ship,
            Diving_boat
        }
        public TYPE Type { get; set; }
        public int Quality { get; set; }
    }
}
