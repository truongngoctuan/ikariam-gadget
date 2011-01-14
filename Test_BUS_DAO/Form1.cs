﻿using System;
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
            if (BUSAction.Login(tbUsername.Text,
                tbPassword.Text,
                "s15.en.ikariam.com") == 0)
            {
                tbResult.Text = BUSAction.InnerHTML();
            }
            else
            {
                MessageBox.Show("đăng nhập thất bại!");
            }

            
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

            BUSEvent.ForceUpdate();
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

        private void button7_Click(object sender, EventArgs e)
        {//research
            StringBuilder strResult = new StringBuilder(2000);
            DTOResearch re = BUSResearch.Get();

            strResult.Append("Scientist: " + re.Scientists);
            strResult.Append(" ResearchPoints: " + re.ResearchPoints);
            strResult.Append("+" + re.ResearchPointsPerHour);
            strResult.Append("\r\n");

            strResult.Append("1: " + re.Seafaring.Name);
            strResult.Append("; " + re.Seafaring.Description);
            strResult.Append("; " + re.Seafaring.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("2: " + re.Economic.Name);
            strResult.Append("; " + re.Economic.Description);
            strResult.Append("; " + re.Economic.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("3: " + re.Scientific.Name);
            strResult.Append("; " + re.Scientific.Description);
            strResult.Append("; " + re.Scientific.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("4: " + re.Militaristic.Name);
            strResult.Append("; " + re.Militaristic.Description);
            strResult.Append("; " + re.Militaristic.Need.ToString());
            strResult.Append("\r\n");

            tbResult.Text = strResult.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder(1000);

            BUSMessage.ForceUpdate();
            int nMessages = BUSMessage.Count();
            for (int i = 0; i < nMessages; i++)
            {
                DTOMessage mess = BUSMessage.Get(i);
                strResult.Append("Message: " + mess.Message);
                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder(1000);

            int nBuildings = BUSBuilding.Count(BUSCity.GetCurrentCity());
            for (int i = 0; i < nBuildings; i++)
            {
                DTOBuilding building = BUSBuilding.GetHouseInfomationInCity(BUSCity.GetCurrentCity(), i);

                strResult.Append("Building: " + building.Type);
                strResult.Append(" " + building.Lvl);

                if (building.IsBuilding)
                {
                    strResult.Append(" " + building.IsBuilding);
                    strResult.Append(" " + building.Time);
                }

                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //change city to #3
            BUSCity.ChangeCityTo(2);
            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button10_Click(object sender, EventArgs e)
        {//get building all town
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " + 
                    BUSCity.GetCity(i).Name
                    + "---\r\n");

                int nBuildings = BUSBuilding.Count(i);
                for (int j = 0; j < nBuildings; j++)
                {
                    DTOBuilding building = BUSBuilding.GetHouseInfomationInCity(i, j);

                    strResult.Append("  Building " + (j + 1).ToString() + ": " + building.Type);
                    strResult.Append(" " + building.Lvl);

                    if (building.IsBuilding)
                    {
                        strResult.Append(" " + building.IsBuilding);
                        strResult.Append(" " + building.Time);
                    }

                    strResult.Append("\r\n");
                }
            }
            
            tbResult.Text = strResult.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //gte troops unit in town
            StringBuilder strResult = new StringBuilder(1000);

            int nTroops = BUSTroops.CountUnits(BUSCity.GetCurrentCity());
            for (int i = 0; i < nTroops; i++)
            {
                DTOTroops troop = BUSTroops.GetUnitsInCity(BUSCity.GetCurrentCity(), i);

                strResult.Append("  " + troop.Type);
                strResult.Append(" " + troop.Quality);
                strResult.Append(" is unit: " + troop.IsUnits);
                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            BUSCity.ChangeCityTo(3);
            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            BUSCity.ChangeCityTo(4);
            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            BUSCity.ChangeCityTo(99);
            tbResult.Text = BUSAction.InnerHTML();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //all units
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " +
                    BUSCity.GetCity(i).Name
                    + "---\r\n");

                int nTroops = BUSTroops.CountUnits(BUSCity.GetCurrentCity());
                for (int j = 0; j < nTroops; j++)
                {
                    DTOTroops troop = BUSTroops.GetUnitsInCity(i, j);

                    strResult.Append("  " + troop.Type);
                    strResult.Append(" " + troop.Quality);
                    strResult.Append(" is unit: " + troop.IsUnits);
                    strResult.Append("\r\n");
                }
            }

            tbResult.Text = strResult.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //get ship
            StringBuilder strResult = new StringBuilder(1000);

            int nTroops = BUSTroops.CountShips(BUSCity.GetCurrentCity());
            for (int i = 0; i < nTroops; i++)
            {
                DTOTroops troop = BUSTroops.GetShipsInCity(BUSCity.GetCurrentCity(), i);

                strResult.Append("  " + troop.Type);
                strResult.Append(" " + troop.Quality);
                strResult.Append(" is unit: " + troop.IsUnits);
                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //all ships
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " +
                    BUSCity.GetCity(i).Name
                    + "---\r\n");

                int nTroops = BUSTroops.CountShips(BUSCity.GetCurrentCity());
                for (int j = 0; j < nTroops; j++)
                {
                    DTOTroops troop = BUSTroops.GetShipsInCity(i, j);

                    strResult.Append("  " + troop.Type);
                    strResult.Append(" " + troop.Quality);
                    strResult.Append(" is unit: " + troop.IsUnits);
                    strResult.Append("\r\n");
                }
            }

            tbResult.Text = strResult.ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string strResult = "";

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                DTOCity city = BUSCity.GetResourceCity(i, true);
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

        private void button20_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder(2000);
            DTOResearch re = BUSResearch.ForceUpdate();

            strResult.Append("Scientist: " + re.Scientists);
            strResult.Append(" ResearchPoints: " + re.ResearchPoints);
            strResult.Append("+" + re.ResearchPointsPerHour);
            strResult.Append("\r\n");

            strResult.Append("1: " + re.Seafaring.Name);
            strResult.Append("; " + re.Seafaring.Description);
            strResult.Append("; " + re.Seafaring.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("2: " + re.Economic.Name);
            strResult.Append("; " + re.Economic.Description);
            strResult.Append("; " + re.Economic.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("3: " + re.Scientific.Name);
            strResult.Append("; " + re.Scientific.Description);
            strResult.Append("; " + re.Scientific.Need.ToString());
            strResult.Append("\r\n");

            strResult.Append("4: " + re.Militaristic.Name);
            strResult.Append("; " + re.Militaristic.Description);
            strResult.Append("; " + re.Militaristic.Need.ToString());
            strResult.Append("\r\n");

            tbResult.Text = strResult.ToString();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //get building all town
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " +
                    BUSCity.GetCity(i).Name
                    + "---\r\n");

                BUSBuilding.ForceUpdate(i);
                int nBuildings = BUSBuilding.Count(i);
                for (int j = 0; j < nBuildings; j++)
                {
                    DTOBuilding building = BUSBuilding.GetHouseInfomationInCity(i, j);

                    strResult.Append("  Building " + (j + 1).ToString() + ": " + building.Type);
                    strResult.Append(" " + building.Lvl);

                    if (building.IsBuilding)
                    {
                        strResult.Append(" " + building.IsBuilding);
                        strResult.Append(" " + building.Time);
                    }

                    strResult.Append("\r\n");
                }
            }

            tbResult.Text = strResult.ToString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //all units
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " +
                    BUSCity.GetCity(i).Name
                    + "---\r\n");
                BUSTroops.ForceUpdateUnits(i);
                int nTroops = BUSTroops.CountUnits(BUSCity.GetCurrentCity());
                for (int j = 0; j < nTroops; j++)
                {
                    DTOTroops troop = BUSTroops.GetUnitsInCity(i, j);

                    strResult.Append("  " + troop.Type);
                    strResult.Append(" " + troop.Quality);
                    strResult.Append(" is unit: " + troop.IsUnits);
                    strResult.Append("\r\n");
                }
            }

            tbResult.Text = strResult.ToString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //all ships
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                strResult.Append("---Town: " +
                    BUSCity.GetCity(i).Name
                    + "---\r\n");
                BUSTroops.ForceUpdateShips(i);
                int nTroops = BUSTroops.CountShips(BUSCity.GetCurrentCity());
                for (int j = 0; j < nTroops; j++)
                {
                    DTOTroops troop = BUSTroops.GetShipsInCity(i, j);

                    strResult.Append("  " + troop.Type);
                    strResult.Append(" " + troop.Quality);
                    strResult.Append(" is unit: " + troop.IsUnits);
                    strResult.Append("\r\n");
                }
            }

            tbResult.Text = strResult.ToString();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            BUSAction.Logout();
            tbResult.Text = BUSAction.InnerHTML();
        }
    }
}
