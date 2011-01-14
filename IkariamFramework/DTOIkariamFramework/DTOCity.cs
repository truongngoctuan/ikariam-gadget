using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IkariamFramework.DTOIkariamFramework
{
    [Serializable, ComVisible(true)]
    public class DTOCity
    {
        public DTOBuilding[] ListBuilding { get; set; }
        public DTOTroops[] ListTroopsUnits { get; set; }//quan bo
        public DTOTroops[] ListTroopsShips { get; set; }//quan thuy

        int _ix;
        int _iy;
        int _iID;
        string _strName;

        public bool IsUpdatedResource = false;

        int _iWood;
        int _iWine;
        int _iMarble;
        int _iCrystal;
        int _iSulphur;

        int _iWoodPerHour;
        int _iWinePerHour;
        int _iMarblePerHour;
        int _iCrystalPerHour;
        int _iSulphurPerHour;

        int _iPopulation;
        int _iPopulationLimit;

        int _iActionPoint;

        int _ilvlWood;
        int _ilvlTradeGood;
        public enum TRADE_GOOD_TYPE
        {
            WINE,
            MARBLE,
            CRYSTAL,
            SULPHUR
        }
        TRADE_GOOD_TYPE _typeTradeGood;

        //int _iGoldPerHour;

        //house

        //trops

        #region
        public int WoodPerHour
        {
            get { return _iWoodPerHour; }
            set { _iWoodPerHour = value; }
        }
        public int Wood
        {
            get { return _iWood; }
            set { _iWood = value; }
        }
        public int ActionPoint
        {
            get { return _iActionPoint; }
            set { _iActionPoint = value; }
        }
        public int PopulationLimit
        {
            get { return _iPopulationLimit; }
            set { _iPopulationLimit = value; }
        }
        public int Population
        {
            get { return _iPopulation; }
            set { _iPopulation = value; }
        }
        public int SulphurPerHour
        {
            get { return _iSulphurPerHour; }
            set { _iSulphurPerHour = value; }
        }
        public int CrystalPerHour
        {
            get { return _iCrystalPerHour; }
            set { _iCrystalPerHour = value; }
        }
        public int MarblePerHour
        {
            get { return _iMarblePerHour; }
            set { _iMarblePerHour = value; }
        }
        public int WinePerHour
        {
            get { return _iWinePerHour; }
            set { _iWinePerHour = value; }
        }
        public int X
        {
            get { return _ix; }
            set { _ix = value; }
        }
        
        public int Y
        {
            get { return _iy; }
            set { _iy = value; }
        }
        
        public string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }        

        public int ID
        {
            get { return _iID; }
            set { _iID = value; }
        } 

        public int Marble
        {
            get { return _iMarble; }
            set { _iMarble = value; }
        }
        
        public int Wine
        {
            get { return _iWine; }
            set { _iWine = value; }
        }
        
        public int Crystal
        {
            get { return _iCrystal; }
            set { _iCrystal = value; }
        }        

        public int Sulphur
        {
            get { return _iSulphur; }
            set { _iSulphur = value; }
        }     

        public int LvlWood
        {
            get { return _ilvlWood; }
            set { _ilvlWood = value; }
        }

        public int LvlTradeGood
        {
            get { return _ilvlTradeGood; }
            set { _ilvlTradeGood = value; }
        }

        public TRADE_GOOD_TYPE TypeTradeGood
        {
            get { return _typeTradeGood; }
            set { _typeTradeGood = value; }
        }
        #endregion
    }
}
