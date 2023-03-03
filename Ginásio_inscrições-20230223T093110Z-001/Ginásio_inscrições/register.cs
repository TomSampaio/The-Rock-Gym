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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }
        UserData[] uData = new UserData[0];
        LoginInfo[] lInfo = new LoginInfo[0];
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUSer.Text == "" || txtPass.Text == "" || txtConfirm.Text == "" || txtAge.Text == "" || txtWeight.Text == "" || txtHeight.Text == "" || cmbGender.Text == "" || cmbGoal.Text == "" || cmbPlan.Text == "")
            {
                MessageBox.Show("Tem que preencher tudo");
                return;
            }
            else if (txtPass.Text != txtConfirm.Text)
            {
                MessageBox.Show("As passwords não são iguais");
                return;
            }
            else if (int.TryParse(txtAge.Text, out int a) == false || a <= 0 || double.TryParse(txtWeight.Text, out double b) == false || b <= 0 || double.TryParse(txtHeight.Text, out double c) == false || c <= 0)
            {
                MessageBox.Show("Tem que introduzir números");
                return;
            }
            else
            {
                bool check = false, test = false ;
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
                    if (checkedListBox1.SelectedItem == "Cycling" && checkedListBox1.SelectedItem == "Pilates" && checkedListBox1.SelectedItem == "KickBox")
                    {
                        classes = 3;
                    }
                    else if (checkedListBox1.SelectedItem == "Pilates" && checkedListBox1.SelectedItem == "KickBox")
                    {
                        classes = 2.2;
                    }
                    else if (checkedListBox1.SelectedItem == "Cycling" && checkedListBox1.SelectedItem == "Pilates")
                    {
                        classes = 2.1;
                    }
                    else if (checkedListBox1.SelectedItem == "Cycling" && checkedListBox1.SelectedItem == "KickBox")
                    {
                        classes = 2.3;
                    }
                    else if (checkedListBox1.SelectedItem == "KickBox")
                    {
                        classes = 1.3;
                    }
                    else if (checkedListBox1.SelectedItem == "Pilates")
                    {
                        classes = 1.2;
                    }
                    else if (checkedListBox1.SelectedItem == "Cycling")
                    {
                        classes = 1.1;
                    }
                }

                for (int i = 0; i < lInfo.Length; i++)
                {
                    if (lInfo[i].username == txtBoxConv.Text)
                    {
                        test = true;
                        break;
                    }
                    else
                    {
                        test = false;
                    }
                }
                if (test == true)
                {
                    price -= 5;
                    check = true;
                }
                else
                {
                    check = false;
                    MessageBox.Show("Usuário inexistente");
                    return;
                }
                Console.WriteLine(classes);
                Array.Resize<UserData>(ref uData, uData.Length + 1);
                Array.Resize<LoginInfo>(ref lInfo, lInfo.Length + 1);
                lInfo[lInfo.Length - 1] = new LoginInfo(txtUSer.Text, txtPass.Text);
                uData[uData.Length - 1] = new UserData(txtUSer.Text, txtAge.Text, cmbGender.Text, txtWeight.Text, txtHeight.Text, cmbGoal.Text, price, classes, check);
                LoginInfo.Save(lInfo);
                UserData.Save(uData);
                if (MessageBox.Show("Conta criada. \nQuer ir para a página de login?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var form = new login();
                    form.Show();
                    this.Close();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new login();
            form.Show();
            this.Close();
        }

        private void register_Load(object sender, EventArgs e)
        {
            string file = @"LoginInfo.xml";
            string uFile = @"UserData.xml";
            if (File.Exists(file))
            {
                XmlSerializer fSrl = new XmlSerializer(typeof(LoginInfo[]));
                FileStream fStr = new FileStream(file, FileMode.Open);
                lInfo = (LoginInfo[])fSrl.Deserialize(fStr);
                fStr.Close();
                Console.WriteLine("information loaded");

                XmlSerializer uFSrl = new XmlSerializer(typeof(UserData[]));
                FileStream uFStr = new FileStream(uFile, FileMode.Open);
                uData = (UserData[])uFSrl.Deserialize(uFStr);
                uFStr.Close();
                Console.WriteLine("information loaded");
            }
            

        }

        private void register_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void cmbDiscount_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbDiscount.Text == "Convidado")
            {
                txtBoxConv.Visible = true;
            }
            else
            {
                txtBoxConv.Visible = false;
            }
        }
    }
}
