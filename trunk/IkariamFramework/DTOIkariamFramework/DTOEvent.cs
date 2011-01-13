using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework.DTOIkariamFramework
{
    public class DTOEvent
    {
        public enum TYPE
        {
            ALL,
            NEW,
            OLD
        }
        public TYPE Type { get; set; }
        public string Town { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}
