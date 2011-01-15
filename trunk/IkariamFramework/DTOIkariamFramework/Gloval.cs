using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamFramework.DTOIkariamFramework
{
    public class Gloval
    {
        public static Data Database { 
            get {
                return Data.getInstance();
            }
        }

        public static Dictionary<string, string> Dict { get; set; }
    }
}
