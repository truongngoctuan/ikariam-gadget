﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOResearchBranch
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Need { get; set; }
    }
}
