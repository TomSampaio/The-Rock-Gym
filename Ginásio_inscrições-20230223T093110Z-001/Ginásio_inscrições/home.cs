using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace Ginásio_inscrições
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }
        UserData[] pHolder = new UserData[0];
        UserData userInfo;
        int pos = 0;
        private void home_Load(object sender, EventArgs e)
        {
            string file = @"UserData.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(UserData[]));
            FileStream fStr = new FileStream(file, FileMode.Open);
            pHolder = (UserData[])fSrl.Deserialize(fStr);
            fStr.Close();
            Console.WriteLine("information loaded");

            for (int i = 0; i < pHolder.Length; i++)
            {
                if (pHolder[i].username == login.nome)
                {
                    pos = i;
                    userInfo = new UserData(pHolder[i].username, pHolder[i].age, pHolder[i].gender, pHolder[i].weight, pHolder[i].height, pHolder[i].goal, pHolder[i].plan);
                    lblNome.Text = pHolder[i].username;
                    if (pHolder[i].plan == "The Rock Silver(30€)")
                    {
                        lblValor.Text = "30€";
                    }
                    else if (pHolder[i].plan == "The Rock Gold (35€)")
                    {
                        lblValor.Text = "35€";
                    }
                    return;
                }
            }
        }

        private void home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dia = 0, mes = 0, ano = 0;
            double peso = 0, bodyFat = 0;
            DateTime date;
            while (true)
            {
                if (double.TryParse(Interaction.InputBox("Introduza o peso atual"), out peso))
                    break;
                else
                    continue;
            }
            while (true)
            {
                if (double.TryParse(Interaction.InputBox("Introduza a percentagem de massa gorda"), out bodyFat))
                    break;
                else
                    continue;
            }
            /*while (true)
            {
                if ()
                    break;
                else
                    continue;
            }*/
            userInfo.weight = peso.ToString();
            dataGridView1.Rows.Add(peso,bodyFat);
            dataGridView1.Rows[dataGridView1.Rows.Count].HeaderCell.Value = DateTime.Now.ToString("dd/MM/yyyy");


        }
    }
}
