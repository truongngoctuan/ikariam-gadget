using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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

        //public string SeafaringName { get; set; }
        //public string SeafaringDescription { get; set; }
        //public long SeafaringNeed { get; set; }

        //public string EconomicName { get; set; }
        //public string EconomicDescription { get; set; }
        //public long EconomicNeed { get; set; }

        //public string ScientificName { get; set; }
        //public string ScientificDescription { get; set; }
        //public long ScientificNeed { get; set; }

        //public string MilitaristicName { get; set; }
        //public string MilitaristicDescription { get; set; }
        //public long MilitaristicNeed { get; set; }        
    }
}
