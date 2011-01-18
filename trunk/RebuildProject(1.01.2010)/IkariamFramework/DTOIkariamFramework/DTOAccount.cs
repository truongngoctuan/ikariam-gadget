using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOAccount
    {
        DTOCity[] _Cities = null;

        public DTOCity[] Cities
        {
            get { return _Cities; }
            set { _Cities = value; }
        }

        public DTOEvent[] Event { get; set; }
        public DTOResearch Research { get; set; }
        public DTOMessage[] Message { get; set; }

        public long TotalGold { get; set; }
        public long TotalGoldPerHour { get; set; }

        public int AdvActive { get; set; }

        public enum ADV_ACTIVE
        {
            MAYOR = 1,
            GENERAL = 2,
            SCIENTIST = 4,
            DIPLOMAT = 8
        }

        //-------------------------------
        //quản lý thời gian update:
        // - gold
        public DateTime DTGold { get; set; }

        // - event
        //public DateTime DTEvent { get; set; }

        // - research
        public DateTime DTResearch { get; set; }

        // - message
        //public DateTime DTMessage { get; set; }
    }
}


