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
        static int pos = 0;
        bool isPremium = false;
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
                    if (pHolder[i].hasDiscont)
                    {
                        if (pHolder[i].price % 2 == 0)
                        {
                            isPremium = true;
                        }
                        else
                        {
                            isPremium = false;
                        }
                    }
                    else
                    {
                        if (pHolder[i].price % 2 == 0)
                        {
                            isPremium = false;
                        }
                        else
                        {
                            isPremium = true;
                        }
                    }
                    break;
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
                {
                    userInfo.weight = peso.ToString();
                    break;
                }
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

            if (dataGridView1.RowCount > 0)
            {
                Array.Resize<DadosTabela>(ref dTable, dataGridView1.Rows.Count);

                for (int i = 0; i < dTable.Length; i++)
                {
                    dTable[i] = new DadosTabela(lblNome.Text, dataGridView1.Rows[i].HeaderCell.Value.ToString(), (double)dataGridView1.Rows[i].Cells[0].Value, (double)dataGridView1.Rows[i].Cells[1].Value);
                }
                DadosTabela.Save(dTable);
            }
            pHolder[pos] = userInfo;
            UserData.Save(pHolder);
            var form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Array.Resize<DadosTabela>(ref dTable, dataGridView1.Rows.Count);

                for (int i = 0; i < dTable.Length; i++)
                {
                    dTable[i] = new DadosTabela(lblNome.Text, dataGridView1.Rows[i].HeaderCell.Value.ToString(), (double)dataGridView1.Rows[i].Cells[0].Value, (double)dataGridView1.Rows[i].Cells[1].Value);
                }
                DadosTabela.Save(dTable);
            }
            pHolder[pos] = userInfo;
            UserData.Save(pHolder);
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
            else if (pic_planotreino.Visible == true)
            {
                pic_planotreino.Visible = false;
            }
            else
            {
                pic_planotreino.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isPremium)
            {
                if (pic_planotreino.Visible == true)
                {
                    pic_planotreino.Visible = false;
                    pic_planoalim.Visible = true;
                }
                else if (pic_planoalim.Visible == true)
                {
                    pic_planoalim.Visible = false;
                }
                else
                {
                    pic_planoalim.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Mude de plano para debloquear esta função");
                return;
            }
        }
    }
}
