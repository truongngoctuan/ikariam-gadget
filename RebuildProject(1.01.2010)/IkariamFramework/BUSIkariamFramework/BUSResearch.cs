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
    public class BUSResearch
    {
        //public static DTOResearch Get()
        //{
        //    if (Gloval.Database.Account.Research == null)
        //    {
        //        return ForceUpdate();
        //    }

        //    return Gloval.Database.Account.Research;
        //}


        public static void requestResearch()
        {
            //chuyen trang
            DAOAdvisor.GoToadvResearch();
            Gloval.Database.Account.DTResearch = DateTime.Now;
            DTOResearch rs = DAOResearch.GetResearchInformation();

            Gloval.Database.Account.Research = rs;
        }

        public static void CalculateFromLocalData()
        {
            DateTime dtnew = DateTime.Now;
            DTOResearch ct = Gloval.Database.Account.Research;
            TimeSpan tp = new TimeSpan(dtnew.Ticks - Gloval.Database.Account.DTResearch.Ticks);

            //cap nhat dan - townhall
            ct.ResearchPoints += (long)BaseFunction.updateValue(ct.ResearchPointsPerHour, (float)tp.TotalSeconds);

            //cap nhat lai thoi gian
            Gloval.Database.Account.DTResearch = dtnew;
            Gloval.Database.Account.Research = ct;
        }
    }
}
