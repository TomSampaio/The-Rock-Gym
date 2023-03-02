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
            int pos;
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
                    pHolder[i] = new UserData(login.nome, txtAge.Text, cmbGender.Text, txtWeight.Text, txtHeight.Text, cmbGoal.Text, cmbPlan.Text);
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
    }
}
