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
        public string user { get; set; }
        public string date { get; set; }
        public double weight { get; set; }
        public double bodyFat { get; set; }

        public DadosTabela()
        {
            this.user = "";
            this.date = "";
            this.weight = 0;
            this.bodyFat = 0;
        }
        public DadosTabela(string nome, string data, double peso, double massaGorda)
        {
            this.user = nome;
            this.date = data;
            this.weight = peso;
            this.bodyFat = massaGorda;
        }

        public static void Save(DadosTabela[] arr)
        {
            string file = @"DadosTabela.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(DadosTabela[]));
            FileStream fStr = new FileStream(file, FileMode.Create);
            fSrl.Serialize(fStr, arr);
            fStr.Close();
            Console.WriteLine("information saved");
        }
    }
}
