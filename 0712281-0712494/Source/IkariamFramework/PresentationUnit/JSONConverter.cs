using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.BUSIkariamFramework;
using IkariamFramework.DTOIkariamFramework;

namespace IkariamFramework.PresentationUnit
{
    public class JSONConverter
    {
        public static string toEmpireOverviewUnitJSON()
        {
            //kiem tra co moi hay ko, ko moi thi khong lay
   



            StringBuilder sb = new StringBuilder(5000);
            sb.Append("{EmpireOverviewUnit:[");
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                sb.Append('{');
                DTOCity ct = BUSCity.GetResourceCity(i);
                sb.Append(string.Format("Name:{0},", ct.Name));
                sb.Append(string.Format("ID:{0},", ct.ID));
                sb.Append(string.Format("X:{0},", ct.X));
                sb.Append(string.Format("Y:{0},", ct.Y));
                sb.Append(string.Format("ActionPoint:{0},", ct.ActionPoint));

                sb.Append(string.Format("FreePopulation:{0},", ct.FreePopulation));
                //sb.Append(string.Format("Population:{0},", ct.Population));
                sb.Append(string.Format("PopulationLimit:{0},", ct.PopulationLimit));

                sb.Append(string.Format("Wood:{0},", ct.Wood));
                sb.Append(string.Format("WoodPerHour:{0},", ct.WoodPerHour));
                sb.Append(string.Format("WoodLimit:{0},", ct.WoodLimit));

                sb.Append(string.Format("Wine:{0},", ct.Wine));
                sb.Append(string.Format("WinePerHour:{0},", ct.WinePerHour));
                sb.Append(string.Format("WineLimit:{0},", ct.WineLimit));

                sb.Append(string.Format("Marble:{0},", ct.Marble));
                sb.Append(string.Format("MarblePerHour:{0},", ct.MarblePerHour));
                sb.Append(string.Format("MarbleLimit:{0},", ct.MarbleLimit));

                sb.Append(string.Format("Crystal:{0},", ct.Crystal));
                sb.Append(string.Format("CrystalPerHour:{0},", ct.CrystalPerHour));
                sb.Append(string.Format("CrystalLimit:{0},", ct.CrystalLimit));

                sb.Append(string.Format("Sulphur:{0},", ct.Sulphur));
                sb.Append(string.Format("SulphurPerHour:{0},", ct.SulphurPerHour));
                sb.Append(string.Format("SulphurLimit:{0},", ct.SulphurLimit));

                sb.Append(string.Format("ResearchPointPerHour:{0},", ct.ResearchPointPerHour));
                sb.Append(string.Format("GoldPerHour:{0}", ct.GoldPerHour));
                sb.Append('}');

                if (i != nCities - 1)
                {//dau phay giua moi bien trong mang
                    sb.Append(',');
                }
            }
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
