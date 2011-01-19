using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOResearch 
    {
        public int Scientists { get; set; }
        public long ResearchPoints { get; set; }
        public int ResearchPointsPerHour { get; set; }

        public DTOResearchBranch Seafaring { get; set; }
        public DTOResearchBranch Economic { get; set; }
        public DTOResearchBranch Scientific { get; set; }
        public DTOResearchBranch Militaristic { get; set; }
    }
}
