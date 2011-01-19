using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.PresentationUnit;
using IkariamFramework.DTOIkariamFramework;
using System.IO;

namespace IkariamFramework.Chart
{
    public class ChartCreator
    {
        public static void CreateChartResource(DTOCity[] cities,
            string strFileName,
            int Width,
            int Height)
        {
            //example
            //data.addColumn('string', 'Công Việc');
            //data.addColumn('number', 'Giờ trên Ngày');
            //data.addColumn('number', 'Giờ trên Ngày2');
            

            StringBuilder sbData = new StringBuilder(1000);
            //tao data cho cot
            sbData.AppendFormat("data.addColumn('string', '{0}');", "tài nguyên");
            for (int i = 0; i < cities.Count(); i++)
            {
                sbData.AppendFormat("data.addColumn('number', '{0}' );", cities[i].Name);
            }
            

            sbData.Append("data.addRows([");

            StringBuilder strWood = new StringBuilder(20);
            StringBuilder strWine = new StringBuilder(20);
            StringBuilder strMarble = new StringBuilder(20);
            StringBuilder strCrystalGlass = new StringBuilder(20);
            StringBuilder strSulphur = new StringBuilder(20);

            //data.addRows([
            //                  ['Coi Phim', 11, 2],
            //                  ['Ăn', 2, 3],
            //                  ['Sinh Hoạt', 2, 4],
            //                  ['Học', 2, 5],
            //                  ['Ngủ', { v: 7, f: '7.000'}, 6]
            //                ]);
            for (int i = 0; i < cities.Count(); i++)
            {
                if (cities.Count() == 1)
                {
                    strWood.AppendFormat("['Wood', {0}], ", (int)cities[i].Wood);
                    strWine.AppendFormat("['Wine', {0}], ", (int)cities[i].Wine);
                    strMarble.AppendFormat("['Marble', {0}], ", (int)cities[i].Marble);
                    strCrystalGlass.AppendFormat("['Crystal Glass', {0}], ", (int)cities[i].Crystal);
                    strSulphur.AppendFormat("['Sulphur', {0}]", (int)cities[i].Sulphur);
                    continue;
                }

                if (i == 0)
                {
                    strWood.AppendFormat("['Wood', {0}, ", (int)cities[i].Wood);
                    strWine.AppendFormat("['Wine', {0}, ", (int)cities[i].Wine);
                    strMarble.AppendFormat("['Marble', {0}, ", (int)cities[i].Marble);
                    strCrystalGlass.AppendFormat("['Crystal Glass', {0}, ", (int)cities[i].Crystal);
                    strSulphur.AppendFormat("['Sulphur', {0}, ", (int)cities[i].Sulphur);
                    continue;
                }


                if (i == cities.Count() - 1)
                {
                    strWood.AppendFormat("{0}],", (int)cities[i].Wood);
                    strWine.AppendFormat("{0}],", (int)cities[i].Wine);
                    strMarble.AppendFormat("{0}],", (int)cities[i].Marble);
                    strCrystalGlass.AppendFormat("{0}],", (int)cities[i].Crystal);
                    strSulphur.AppendFormat("{0}]", (int)cities[i].Sulphur);
                    continue;
                }

                strWood.AppendFormat("{0}, ", (int)cities[i].Wood);
                strWine.AppendFormat("{0}, ", (int)cities[i].Wine);
                strMarble.AppendFormat("{0}, ", (int)cities[i].Marble);
                strCrystalGlass.AppendFormat("{0}, ", (int)cities[i].Crystal);
                strSulphur.AppendFormat("{0}, ", (int)cities[i].Sulphur);
            }

            sbData.Append(strWood);
            sbData.Append(strWine);
            sbData.Append(strMarble);
            sbData.Append(strCrystalGlass);
            sbData.Append(strSulphur);

            sbData.Append("]);");

            WriteToHtmlFile(sbData, strFileName, Width, Height);
        }

        public static void WriteToHtmlFile(StringBuilder sbContent,
            string strFileName,
            int Width,
            int Height)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            string strFullPath = path + "\\" + strFileName;

            if (!File.Exists(strFullPath))
            {
                FileStream fs = File.Create(strFullPath);
                fs.Close();
            }

            //tao khung chua du lieu
            StringBuilder sb = new StringBuilder(5000);
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Chart</title>");
            sb.AppendLine("<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>");
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("google.load('visualization', '1', { 'packages': ['corechart'] });");
            sb.AppendLine("google.setOnLoadCallback(drawChart);");
            sb.AppendLine("function drawChart() {");
            sb.AppendLine("var data = new google.visualization.DataTable();");

            sb.AppendLine(sbContent.ToString());
            
            sb.AppendLine("var chart = new google.visualization.BarChart(document.getElementById('chart_div'));");
            sb.AppendLine("chart.draw(data, { width: " + Width + ", height: " + Height + ", is3D: true, title: 'Sum Resource' });");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div id=\"chart_div\">");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            TextWriter tsw = new StreamWriter(strFullPath);
            tsw.Write(sb.ToString());
            tsw.Close();
        }
    }
}
