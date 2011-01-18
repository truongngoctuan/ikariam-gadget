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
    public class BUSMessage
    {
        public static void requestMessage()
        {
            DAOAdvisor.GoToadvDiplomacy();
            //Gloval.Database.Account.DTMessage = DateTime.Now;
            Gloval.Database.Account.Message = DAOMessage.Get10LastMessage();
        }

        #region old code
        //public static int Count()
        //{
        //    if (Gloval.Database.Account.Message == null)
        //    {
        //        ForceUpdate();
        //    }

        //    return Gloval.Database.Account.Message.Count();
        //}

        //public static DTOMessage Get(int iIndex)
        //{
        //    //tu dong cap nhat danh sach neu chua co
        //    if (Gloval.Database.Account.Message == null)
        //    {
        //        ForceUpdate();
        //    }

        //    if (0 <= iIndex && iIndex < Gloval.Database.Account.Message.Count())
        //    {
        //        return Gloval.Database.Account.Message[iIndex];
        //    }

        //    //thong bao loi~
        //    return null;
        //}
        #endregion
    }
}
