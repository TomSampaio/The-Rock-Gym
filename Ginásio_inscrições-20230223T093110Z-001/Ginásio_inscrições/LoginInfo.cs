using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Ginásio_Code
{
    public class LoginInfo
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginInfo()
        {
            this.username = "";
            this.password = "";
        }
        public LoginInfo(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public static void Load(LoginInfo[] arr)
        {
            string file = @"LoginInfo.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(LoginInfo[]));
            FileStream fStr = new FileStream(file, FileMode.Open);
            arr = (LoginInfo[])fSrl.Deserialize(fStr);
            fStr.Close();
            Console.WriteLine("information loaded");
        }

        public static void Save(LoginInfo[] arr)
        {
            string file = @"LoginInfo.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(LoginInfo[]));
            FileStream fStr = new FileStream(file, FileMode.Create);
            fSrl.Serialize(fStr, arr);
            fStr.Close();
            Console.WriteLine("information saved");
        }
    }
}
