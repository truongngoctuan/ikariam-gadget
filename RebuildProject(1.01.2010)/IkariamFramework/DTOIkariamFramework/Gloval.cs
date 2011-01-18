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

        private static bool _BuildingsOverviewIsNewData;
        public static bool bBuildingsOverviewIsNewData
        {
            get
            {
                lock (lockThis)
                {
                    return _BuildingsOverviewIsNewData;
                }
            }
            set
            {
                lock (lockThis)
                {
                    _BuildingsOverviewIsNewData = value;
                }
            }
        }

        private static bool _TroopsOverviewIsNewData;
        public static bool bTroopsOverviewIsNewData
        {
            get
            {
                lock (lockThis)
                {
                    return _TroopsOverviewIsNewData;
                }
            }
            set
            {
                lock (lockThis)
                {
                    _TroopsOverviewIsNewData = value;
                }
            }
        }

        private static bool _ResearchOverviewIsNewData;
        public static bool bResearchOverviewIsNewData
        {
            get
            {
                lock (lockThis)
                {
                    return _ResearchOverviewIsNewData;
                }
            }
            set
            {
                lock (lockThis)
                {
                    _ResearchOverviewIsNewData = value;
                }
            }
        }

        private static bool _DiplomatOverviewIsNewData;
        public static bool bDiplomatOverviewIsNewData
        {
            get
            {
                lock (lockThis)
                {
                    return _DiplomatOverviewIsNewData;
                }
            }
            set
            {
                lock (lockThis)
                {
                    _DiplomatOverviewIsNewData = value;
                }
            }
        }

    }
}
