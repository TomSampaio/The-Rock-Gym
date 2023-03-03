using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ginásio_inscrições
{
    public class UserData
    {
        public string username { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string weight { get;set; }
        public string height { get; set; }
        public string goal { get; set; }
        public int price { get; set; }
        public double classes { get; set; }
        public bool hasDiscont { get; set; }

        public UserData(string username, string age, string gender, string weight, string height, string goal, int price, double classes, bool hasDiscont)
        {
            this.username = username;
            this.age = age;
            this.gender = gender;
            this.weight = weight;
            this.height = height;
            this.goal = goal;
            this.price = price;
            this.classes = classes;
            this.hasDiscont = hasDiscont;
        }
        public UserData()
        {
            this.username = "";
            this.age = "";
            this.gender = "";
            this.weight = "";
            this.height = "";
            this.goal = "";
            this.price = 0;
            this.classes = 0;
            this.hasDiscont = false;
        }

        public static void Load(UserData[] arr)
        {
            string file = @"UserData.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(UserData[]));
            FileStream fStr = new FileStream(file, FileMode.Open);
            arr = (UserData[])fSrl.Deserialize(fStr);
            fStr.Close();
            Console.WriteLine("information loaded");
        }

        public static void Save(UserData[] arr)
        {
            string file = @"UserData.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(UserData[]));
            FileStream fStr = new FileStream(file, FileMode.Create);
            fSrl.Serialize(fStr, arr);
            fStr.Close();
            Console.WriteLine("information saved");
        }
    }
}
