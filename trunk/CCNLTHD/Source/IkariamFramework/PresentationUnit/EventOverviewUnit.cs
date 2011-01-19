using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IkariamFramework.PresentationUnit
{
    [Serializable, ComVisible(true)]
    class EventOverviewUnit
    {
        public string Town { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
