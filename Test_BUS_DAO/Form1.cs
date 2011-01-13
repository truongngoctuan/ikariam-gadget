using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IkariamFramework;
using IkariamFramework.BUSIkariamFramework;
using IkariamFramework.DTOIkariamFramework;

namespace Test_BUS_DAO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BUSAction.Login(tbUsername.Text,
                tbPassword.Text,
                "s15.en.ikariam.com");

            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button2_Click(object sender, EventArgs e)
        {//get list city
            string strResult = "";
            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                DTOCity city = BUSCity.GetCity(i);
                strResult += " X: " + city.X;
                strResult += " Y: " + city.Y;
                strResult += " ID: " + city.ID;
                strResult += " Name: " + city.Name;
                strResult += " Type: " + city.TypeTradeGood;
                strResult += "\r\n";
            }

            tbResult.Text = strResult;
        }

        private void button3_Click(object sender, EventArgs e)
        {//change city to #2
            BUSCity.ChangeCityTo(1);
            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button4_Click(object sender, EventArgs e)
        {//get inf current city
            string strResult = "";

            DTOCity city = BUSCity.GetResourceCity(BUSCity.GetCurrentCity());
            strResult += " X: " + city.X;
            strResult += " Y: " + city.Y;
            strResult += " ID: " + city.ID;
            strResult += " Name: " + city.Name;
            strResult += " Type: " + city.TypeTradeGood;

            strResult += " po: " + city.Population;
            strResult += "(" + city.PopulationLimit + ")";
            strResult += " action: " + city.ActionPoint;

            strResult += " wood: " + city.Wood;
            strResult += "+" + city.WoodPerHour;
            strResult += " wine: " + city.Wine;
            strResult += "+" + city.WinePerHour;
            strResult += " mar: " + city.Marble;
            strResult += "+" + city.MarblePerHour;
            strResult += " crys: " + city.Crystal;
            strResult += "+" + city.CrystalPerHour;
            strResult += " sul: " + city.Sulphur;
            strResult += "+" + city.SulphurPerHour;
            strResult += "\r\n";

            tbResult.Text = strResult;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string strResult = "";

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                DTOCity city = BUSCity.GetResourceCity(i);
                strResult += " X: " + city.X;
                strResult += " Y: " + city.Y;
                strResult += " ID: " + city.ID;
                strResult += " Name: " + city.Name;
                strResult += " Type: " + city.TypeTradeGood;

                strResult += " po: " + city.Population;
                strResult += "(" + city.PopulationLimit + ")";
                strResult += " action: " + city.ActionPoint;

                strResult += " wood: " + city.Wood;
                strResult += "+" + city.WoodPerHour;
                strResult += " wine: " + city.Wine;
                strResult += "+" + city.WinePerHour;
                strResult += " mar: " + city.Marble;
                strResult += "+" + city.MarblePerHour;
                strResult += " crys: " + city.Crystal;
                strResult += "+" + city.CrystalPerHour;
                strResult += " sul: " + city.Sulphur;
                strResult += "+" + city.SulphurPerHour;
                strResult += "\r\n";
            }

            tbResult.Text = strResult;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder(1000);

            int nEvents = BUSEvent.Count();
            for (int i = 0; i < nEvents; i++)
            {
                DTOEvent ev = BUSEvent.Get(i);
                strResult.Append("Type: " + ev.Type);
                strResult.Append(" town: " + ev.Town);
                strResult.Append(" date: " + ev.Date);
                strResult.Append(" message: " + ev.Message);
                
                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }
    }
}
