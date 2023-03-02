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
                Array.Resize<UserData>(ref uData, uData.Length + 1);
                Array.Resize<LoginInfo>(ref lInfo, lInfo.Length + 1);
                lInfo[lInfo.Length - 1] = new LoginInfo(txtUSer.Text, txtPass.Text);
                uData[uData.Length - 1] = new UserData(txtUSer.Text, txtAge.Text, cmbGender.Text, txtWeight.Text, txtHeight.Text, cmbGoal.Text, cmbPlan.Text);
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
                FileStream uFStr = new FileStream(file, FileMode.Open);
                uData = (UserData[])uFSrl.Deserialize(uFStr);
                uFStr.Close();
                Console.WriteLine("information loaded");
            }
            

        }

        private void register_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
