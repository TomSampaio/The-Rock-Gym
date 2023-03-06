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
        UserData[] pHolder;
        UserData userInfo;
        int pos = 0;
        private void home_Load(object sender, EventArgs e)
        {
            string file = @"UserData.xml";

            XmlSerializer fSrl = new XmlSerializer(typeof(UserData[]));
            FileStream fStr = new FileStream(file, FileMode.Open);
            pHolder = (UserData[])fSrl.Deserialize(fStr);
            fStr.Close();
            Console.WriteLine("information loadedUser");

            for (int i = 0; i < pHolder.Length; i++)
            {
                if (pHolder[i].username == login.nome)
                {
                    pos = i;
                    userInfo = new UserData(pHolder[i].username, pHolder[i].age, pHolder[i].gender, pHolder[i].weight, pHolder[i].height, pHolder[i].goal, pHolder[i].price, pHolder[i].classes, pHolder[i].hasDiscont);
                    lblNome.Text = pHolder[i].username;
                    lblValor.Text = pHolder[i].price.ToString("0€");
                    return;
                }
            }

            string fileTable = @"DadosTabela.xml";

            if (File.Exists(fileTable))
            {
                XmlSerializer fSrlT = new XmlSerializer(typeof(DadosTabela[]));
                FileStream fStrT = new FileStream(fileTable, FileMode.Open);
                dTable = (DadosTabela[])fSrlT.Deserialize(fStrT);
                fStrT.Close();
                Console.WriteLine("information loadedTable");
                
                for (int i = 0; i < dTable.Length; i++)
                {
                    if (dTable[i].user == userInfo.username)
                    {
                        dataGridView1.Rows.Add(dTable[i].weight, dTable[i].bodyFat);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = dTable[i].date;
                        Console.WriteLine("Confirm");
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
                dTable[i] = new DadosTabela(lblNome.Text, dataGridView1.Rows[i].HeaderCell.Value.ToString(), (double)dataGridView1.Rows[i].Cells[0].Value, (double)dataGridView1.Rows[i].Cells[1].Value);
            }
            Console.WriteLine(dataGridView1.Rows[0].HeaderCell.Value.ToString());
            DadosTabela.Save(dTable);
            var form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new login_edit();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pic_planoalim.Visible == true)
            {
                pic_planoalim.Visible = false;
                pic_planotreino.Visible = true;
            }
            else
            {
                pic_planotreino.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pic_planotreino.Visible == true)
            {
                pic_planotreino.Visible = false;
                pic_planoalim.Visible = true;
            }
            else
            {
                pic_planoalim.Visible = true; 
            }
        }
    }
}
