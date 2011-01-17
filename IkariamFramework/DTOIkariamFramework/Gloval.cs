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

        public static int NRequestPerTask { get; set; }

        //-------------------------------
        //quản lý thời gian update:

        //quản lý dữ liệu có cần cập nhật khi gadget hỏi hay không
        public static object lockThis = new Object();
        private static bool _EmpireOverviewIsNewData;
        public static bool bEmpireOverviewIsNewData {
            get
            {
                lock (lockThis)
                {
                    return _EmpireOverviewIsNewData;
                }
            }
            set
            {
                lock (lockThis)
                {
                    _EmpireOverviewIsNewData = value;
                }
            }
        }
    }
}
