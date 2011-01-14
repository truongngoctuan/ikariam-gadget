using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DAOIkariamFramework;
using IkariamFramework.DTOIkariamFramework;
using System.Runtime.InteropServices;

namespace IkariamFramework.BUSIkariamFramework
{
    [ComVisible(true)]
    public class BUSEvent
    {
        public static int Count()
        {
            if (Database.accInf.Event == null)
            {
                DAOEvent.GetEventEntry();
            }

            return Database.accInf.Event.Count();
        }

        public static DTOEvent Get(int iIndex)
        {
            //tu dong cap nhat danh sach neu chua co
            if (Database.accInf.Event == null)
            {
                DAOEvent.GetEventEntry();
            }

            if (0 <= iIndex && iIndex < Database.accInf.Event.Count())
            {
                return Database.accInf.Event[iIndex];
            }

            //thong bao loi~
            return null;
        }

        public static void Update()
        {
            DAOEvent.GetEventEntry();
        }
    }
}
