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
using IkariamFramework.InterfaceToGadget;

namespace Test_BUS_DAO
{
    public partial class Form1 : Form
    {
        IkariamFramework.InterfaceToGadget.Gadget gg = new IkariamFramework.InterfaceToGadget.Gadget();
        enum EEE { ABC = 0, NDD = 5 };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gg2.Login(tbUsername.Text,
                tbPassword.Text,
                //"s15.en.ikariam.com") == 0)
                //"s5.vn.ikariam.com") == 0)
                tbServer.Text) == 0)
            {
                gg2.bStopAutoRequest = false;
                gg2.InitAutoRequest();

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

            DTOCity cityFF = BUSCity.GetCity(0);


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
            DTOCity city = BUSCity.GetResourceCity(0);
            //DTOCity city = BUSCity.GetResourceCity(BUSCity.GetCurrentCity());
            strResult += " X: " + city.X;
            strResult += " Y: " + city.Y;
            strResult += " ID: " + city.ID;
            strResult += " Name: " + city.Name;
            strResult += " Type: " + city.TypeTradeGood;

            strResult += " po: " + city.FreePopulation;
            strResult += "(" + city.Population + ")";
            strResult += " action: " + city.ActionPoint;

            strResult += " wood: " + city.Wood;
            strResult += "+" + city.WoodPerHour;
            strResult += " limit: " + city.WoodLimit;
            strResult += " wine: " + city.Wine;
            strResult += "+" + city.WinePerHour;
            strResult += " limit: " + city.WineLimit;
            strResult += " mar: " + city.Marble;
            strResult += "+" + city.MarblePerHour;
            strResult += " limit: " + city.MarbleLimit;
            strResult += " crys: " + city.Crystal;
            strResult += "+" + city.CrystalPerHour;
            strResult += " limit: " + city.CrystalLimit;
            strResult += " sul: " + city.Sulphur;
            strResult += "+" + city.SulphurPerHour;
            strResult += " limit: " + city.SulphurLimit;
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

                strResult += " po: " + city.FreePopulation;
                strResult += "(" + city.Population + ")";
                strResult += " action: " + city.ActionPoint;

                strResult += " wood: " + city.Wood;
                strResult += "+" + city.WoodPerHour;
                strResult += " limit: " + city.WoodLimit;
                strResult += " wine: " + city.Wine;
                strResult += "+" + city.WinePerHour;
                strResult += " limit: " + city.WineLimit;
                strResult += " mar: " + city.Marble;
                strResult += "+" + city.MarblePerHour;
                strResult += " limit: " + city.MarbleLimit;
                strResult += " crys: " + city.Crystal;
                strResult += "+" + city.CrystalPerHour;
                strResult += " limit: " + city.CrystalLimit;
                strResult += " sul: " + city.Sulphur;
                strResult += "+" + city.SulphurPerHour;
                strResult += " limit: " + city.SulphurLimit;
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

        private void button25_Click(object sender, EventArgs e)
        {
            if (gg.Login("green",
                "22551325",
                "s5.vn.ikariam.com") == 0)
            {
                gg2.bStopAutoRequest = false;
                gg2.InitAutoRequest();

                tbResult.Text = BUSAction.InnerHTML();
            }
            else
            {
                MessageBox.Show("đăng nhập thất bại!");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
             StringBuilder strResult = new StringBuilder(50);
             strResult.Append("total gold: " + BUSAction.GetTotalGold(true));
             strResult.Append("per hour: " + BUSAction.GetTotalGoldPerHour());
             strResult.Append("\r\n");
             tbResult.Text = strResult.ToString();
        }

        private void button28_Click(object sender, EventArgs e)
        {//town hall inf
            StringBuilder strResult = new StringBuilder(1000);

            DTOCity ct = BUSCity.GetTownHallInfomationInCity(Gloval.Database.CurrentCity, true);

            strResult.Append("---Town: " +
                    BUSCity.GetCity(Gloval.Database.CurrentCity).Name
                    + "---\r\n");

            strResult.Append("polimit: " + ct.PopulationLimit);
            strResult.Append(" pogrow: " + ct.PopulationGrow.ToString());
            strResult.Append(" net gold: " + ct.GoldPerHour);
            strResult.Append(" scientist point per hour: " + ct.ResearchPointPerHour.ToString());

            strResult.Append("\r\n");

            tbResult.Text = strResult.ToString();
        }

        private void button26_Click(object sender, EventArgs e)
        {//adv statuc
            StringBuilder strResult = new StringBuilder(50);
            strResult.Append("adv active: " + BUSAction.CheckAdvStatus());
            strResult.Append("\r\n");
            tbResult.Text = strResult.ToString();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder(5000);

            int nCities = BUSCity.Count();
            for (int i = 0; i < nCities; i++)
            {
                DTOCity ct = BUSCity.GetTownHallInfomationInCity(i, true);

                strResult.Append("---Town: " +
                        BUSCity.GetCity(i).Name
                        + "---\r\n");

                strResult.Append("polimit: " + ct.PopulationLimit);
                strResult.Append(" pogrow: " + ct.PopulationGrow.ToString());
                strResult.Append(" net gold: " + ct.GoldPerHour);
                strResult.Append(" scientist point per hour: " + ct.ResearchPointPerHour.ToString());

                strResult.Append("\r\n");
            }

            tbResult.Text = strResult.ToString();
        }

        IkariamFramework.Gadget gg2 = new IkariamFramework.Gadget();
        Timer tm = null;
        private void button30_Click(object sender, EventArgs e)
        {//run ika thread
            gg2.bStopAutoRequest = false;
            gg2.InitAutoRequest();
        }

        void tm_Tick(object sender, EventArgs e)
        {
            int iCode = gg2.requestCode();

            if ((iCode & (int)1) != 0) tbResult.Text = gg2.requestEmpireOverview() + tbResult.Text;
            //if if if...

            tbThreaddebug.Text = gg2.requestDEBUGString();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tm = new Timer();
            tm.Interval = 2000;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tm.Stop();
            gg2.StopAutoRequest();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tm != null)
            {
                button32_Click(null, null);
            }
        }

        private void button32_Click_1(object sender, EventArgs e)
        {
            gg2.StopAutoRequest();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Data dt = Gloval.Database;

        }
    }
}
