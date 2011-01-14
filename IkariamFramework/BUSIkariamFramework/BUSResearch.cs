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
        public static DTOResearch Get()
        {
            if (Database.accInf.Research == null)
            {
                return ForceUpdate();
            }

            return Database.accInf.Research;
        }


        public static DTOResearch ForceUpdate()
        {
            //chuyen trang
            DAOAdvisor.GoToadvResearch();

            //lay sc point
            DAOResearch.GetCurrentResearchScientists();

            //lay cac thong tin 4 truong phai kia
            DAOResearch.GetInfomation4BrandOfResearch();

            return Database.accInf.Research;
        }
    }
}
