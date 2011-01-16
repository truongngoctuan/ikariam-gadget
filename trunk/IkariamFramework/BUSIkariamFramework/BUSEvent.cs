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
            if (Gloval.Database.Account.Event == null)
            {
                DAOEvent.GetEventEntry();
                Gloval.Database.Account.DTEvent = DateTime.Now;
            }

            return Gloval.Database.Account.Event.Count();
        }

        public static DTOEvent Get(int iIndex)
        {
            //tu dong cap nhat danh sach neu chua co
            if (Gloval.Database.Account.Event == null)
            {
                DAOEvent.GetEventEntry();
                Gloval.Database.Account.DTEvent = DateTime.Now;
            }

            if (0 <= iIndex && iIndex < Gloval.Database.Account.Event.Count())
            {
                return Gloval.Database.Account.Event[iIndex];
            }

            //thong bao loi~
            return null;
        }

        public static void ForceUpdate()
        {
            DAOEvent.GetEventEntry();
            Gloval.Database.Account.DTEvent = DateTime.Now;
        }
    }
}
