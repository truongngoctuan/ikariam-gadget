using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkariamFramework.DTOIkariamFramework;
using IkariamFramework.BUSIkariamFramework;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IkariamFramework.InterfaceToGadget
{
    [ComVisible(true)]
    public class Gadget
    {
        public Gadget()
        {
            
        }

        public int Login(string username, string password, string server)
        {
            // Login : trả về bool, thành công - thất bại
            // Hoặc tra về errorCode
            if (BUSAction.Login(username, password, server) == 0)
            {
                //xac dinh lang dua vao server
                string[] split = server.Split('.');

                Gloval.Dict = XmlHelper.LoadFile(string.Format("Lang\\{0}.xml", split[1]));

                //test 
                //string strout = "";
                //Gloval.Dict.TryGetValue("test3", out strout);
                //return strout;
                //MessageBox.Show(strout);
                return 0;
            }
            return 1;


        }

        public string Test()
        {
            Gloval.Dict = XmlHelper.LoadFile("Lang\\en.xml");

            //test 
            string strout = "";
            Gloval.Dict.TryGetValue("test3", out strout);
            return strout;
        }
    }
}
