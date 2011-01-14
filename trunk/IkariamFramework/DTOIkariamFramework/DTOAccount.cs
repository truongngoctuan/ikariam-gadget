using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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
    }
}

