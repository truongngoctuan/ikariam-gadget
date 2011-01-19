using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.PresentationUnit
{
    [Serializable, ComVisible(true)]
    public class TroopOverviewUnit
    {
        public string TownName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Dictionary<string, DTOTroops> Troops {get; set;}
        public Dictionary<string, DTOTroops> Ships { get; set; }
    }
}
