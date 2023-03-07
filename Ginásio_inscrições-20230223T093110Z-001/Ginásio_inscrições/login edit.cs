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

namespace Ginásio_inscrições
{
    public partial class login_edit : Form
    {
        public login_edit()
        {
            InitializeComponent();
        }
        UserData[] pHolder = new UserData[0];
        private void button1_Click(object sender, EventArgs e)
        {
            
            double classes = 0;
            int price = 0;
            if (cmbPlan.Text == "The Rock Gold (35€)")
                price = 35;
            else if (cmbPlan.Text == "The Rock Silver (30€)")
                price = 30;

            if (checkedListBox1.CheckedItems.Count > 0)
            {
                if (price == 35)
                {
                    price += (checkedListBox1.CheckedItems.Count - 1) * 5;
                }
                else
                {
                    price += checkedListBox1.CheckedItems.Count * 5;
                }
                if (checkedListBox1.GetItemChecked(0) && checkedListBox1.GetItemChecked(1) && checkedListBox1.GetItemChecked(2))
                {
                    classes = 3;
                }
                else if (checkedListBox1.GetItemChecked(1) && checkedListBox1.GetItemChecked(2))
                {
                    classes = 2.2;
                }
                else if (checkedListBox1.GetItemChecked(0) && checkedListBox1.GetItemChecked(1))
                {
                    classes = 2.1;
                }
                else if (checkedListBox1.GetItemChecked(0) && checkedListBox1.GetItemChecked(2))
                {
                    classes = 2.3;
                }
                else if (checkedListBox1.GetItemChecked(2))
                {
                    classes = 1.3;
                }
                else if (checkedListBox1.GetItemChecked(1))
                {
                    classes = 1.2;
                }
                else if (checkedListBox1.GetItemChecked(0))
                {
                    classes = 1.1;
                }
            }
            for (int i = 0; i < pHolder.Length; i++)
            {
                if (pHolder[i].username == login.nome)
                {
                    pHolder[i] = new UserData(login.nome, txtAge.Text, cmbGender.Text, pHolder[i].weight, txtHeight.Text, cmbGoal.Text, price, classes, pHolder[i].hasDiscont);
                    break;
                }
            }
            UserData.Save(pHolder);

            if (MessageBox.Show("Dados alterados. \nQuer voltar para o menu?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                var form = new home();
                form.Show();
                this.Hide();
            }
        }

        private void login_edit_Load(object sender, EventArgs e)
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
                    txtAge.Text = pHolder[i].age;
                    txtHeight.Text = pHolder[i].height;
                    cmbGender.Text = pHolder[i].gender;
                    cmbGoal.Text = pHolder[i].goal;
                    if (pHolder[i].hasDiscont)
                    {
                        if (pHolder[i].classes == 0)
                        {
                            if (pHolder[i].price + 5 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price + 5 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes > 1 && pHolder[i].classes < 2)
                        {
                            if (pHolder[i].price + 5 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price + 5 - 5 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes > 2 && pHolder[i].classes < 3)
                        {
                            if (pHolder[i].price + 5 - 5 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price + 5 - 10 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes == 3)
                        {
                            if (pHolder[i].price + 5 - 10 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price + 5 - 15 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                    }
                    else
                    {
                        if (pHolder[i].classes == 0)
                        {
                            if (pHolder[i].price == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes < 2)
                        {
                            if (pHolder[i].price == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price - 5 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes > 2 && pHolder[i].classes < 3)
                        {
                            if (pHolder[i].price - 5 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price - 10 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                        else if (pHolder[i].classes == 3)
                        {
                            if (pHolder[i].price - 10 == 35)
                            {
                                cmbPlan.Text = "The Rock Gold (35€)";
                            }
                            else if (pHolder[i].price - 15 == 30)
                            {
                                cmbPlan.Text = "The Rock Silver (30€)";
                            }
                        }
                    }

                    if (pHolder[i].classes == 1.1)
                        checkedListBox1.SetItemChecked(0, true);
                    else if (pHolder[i].classes == 1.2)
                        checkedListBox1.SetItemChecked(1, true);
                    else if (pHolder[i].classes == 1.3)
                        checkedListBox1.SetItemChecked(2, true);
                    else if (pHolder[i].classes == 2.1)
                    {
                        checkedListBox1.SetItemChecked(0, true);
                        checkedListBox1.SetItemChecked(1, true);
                    }
                    else if (pHolder[i].classes == 2.2)
                    {
                        checkedListBox1.SetItemChecked(1, true);
                        checkedListBox1.SetItemChecked(2, true);
                    }
                    else if (pHolder[i].classes == 2.2)
                    {
                        checkedListBox1.SetItemChecked(0, true);
                        checkedListBox1.SetItemChecked(2, true);
                    }
                    else if (pHolder[i].classes == 3)
                    {
                        checkedListBox1.SetItemChecked(0, true);
                        checkedListBox1.SetItemChecked(1, true);
                        checkedListBox1.SetItemChecked(2, true);
                    }
                }
            }
        }
    }
}
