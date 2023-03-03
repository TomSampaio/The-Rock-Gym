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
        DadosTabela[] dTable = new DadosTabela[0];
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
                    userInfo = new UserData(pHolder[i].username, pHolder[i].age, pHolder[i].gender, pHolder[i].weight, pHolder[i].height, pHolder[i].goal, pHolder[i].price, pHolder[i].classes);
                    lblNome.Text = pHolder[i].username;
                    lblValor.Text = pHolder[i].price.ToString("0€");
                    return;
                }
            }

            string fileT = @"UserData.xml";

            if (File.Exists(fileT))
            {
                XmlSerializer fSrlT = new XmlSerializer(typeof(UserData[]));
                FileStream fStrT = new FileStream(file, FileMode.Open);
                pHolder = (UserData[])fSrlT.Deserialize(fStrT);
                fStrT.Close();
                Console.WriteLine("information loaded");
                
                for (int i = 0; i < dTable.Length; i++)
                {
                    if (dTable[i].user == lblNome.Text)
                    {
                        dataGridView1.Rows.Add(dTable[i].weight, dTable[i].bodyFat);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].HeaderCell.Value = dTable[i].date;
                    }
                }
            }
        }

        private void home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double peso = 0, bodyFat = 0;
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
            dataGridView1.Rows[dataGridView1.Rows.Count-1].HeaderCell.Value = DateTime.Now.ToString("dd/MM/yyyy");


        }

        private void button5_Click(object sender, EventArgs e)
        {
            //terminar sessao

            Array.Resize<DadosTabela>(ref dTable, dataGridView1.Rows.Count);
            
            for (int i = 0; i < dTable.Length; i++)
            {
                dTable[i] = new DadosTabela(lblNome.Text, dataGridView1.Rows[i].HeaderCell.ToString(), (double)dataGridView1.Rows[i].Cells[0].Value, (double)dataGridView1.Rows[i].Cells[1].Value);
            }

            DadosTabela.Save(dTable);
        }
    }
}
