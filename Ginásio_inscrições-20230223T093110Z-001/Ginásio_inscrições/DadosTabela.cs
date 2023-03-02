using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ginásio_inscrições
{
    internal class DadosTabela
    {
        public string date { get; set; }
        public double weight { get; set; }
        public double bodyFat { get; set; }

        public DadosTabela()
        {
            date = "";
            weight = 0;
            bodyFat = 0;
        }
        public DadosTabela(string data, double peso, double massaGorda)
        {
            date = data;
            weight = peso;
            bodyFat = massaGorda;
        }

        public static void Save(UserData[] arr)
        {
            string file = @"DadosTabela.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(UserData[]));
            FileStream fStr = new FileStream(file, FileMode.Create);
            fSrl.Serialize(fStr, arr);
            fStr.Close();
            Console.WriteLine("information saved");
        }
    }
}
