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
        public static void requestEvent()
        {
            DAOAdvisor.GoToadvCities();
            //Gloval.Database.Account.DTEvent = DateTime.Now;
            Gloval.Database.Account.Event = DAOEvent.GetEventEntry();
        }

        #region old code
        //public static int Count()
        //{
        //    if (Gloval.Database.Account.Event == null)
        //    {
        //        DAOAdvisor.GoToadvCities();
        //        Gloval.Database.Account.Event = DAOEvent.GetEventEntry();
        //        Gloval.Database.Account.DTEvent = DateTime.Now;
        //    }

        //    return Gloval.Database.Account.Event.Count();
        //}

        //public static DTOEvent Get(int iIndex)
        //{
        //    //tu dong cap nhat danh sach neu chua co
        //    if (Gloval.Database.Account.Event == null)
        //    {
        //        DAOAdvisor.GoToadvCities();
        //        Gloval.Database.Account.Event = DAOEvent.GetEventEntry();
        //        Gloval.Database.Account.DTEvent = DateTime.Now;
        //    }

        //    if (0 <= iIndex && iIndex < Gloval.Database.Account.Event.Count())
        //    {
        //        return Gloval.Database.Account.Event[iIndex];
        //    }

        //    //thong bao loi~
        //    return null;
        //}

        #endregion

    }
}
