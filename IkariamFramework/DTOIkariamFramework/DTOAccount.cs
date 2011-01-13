using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework.DTOIkariamFramework
{
    public class DTOAccount
    {
        DTOCity[] _Cities = null;

        public DTOCity[] Cities
        {
            get { return _Cities; }
            set { _Cities = value; }
        }

        public DTOEvent[] Event { get; set; }
    }
}
