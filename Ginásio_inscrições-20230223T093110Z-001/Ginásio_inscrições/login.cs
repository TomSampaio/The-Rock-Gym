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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        LoginInfo[] log;
        public static string nome;
        private void button1_Click(object sender, EventArgs e)
        {
            string file = @"LoginInfo.xml";

            if (File.Exists(file))
            {
                XmlSerializer fSrl = new XmlSerializer(typeof(LoginInfo[]));
                FileStream fStr = new FileStream(file, FileMode.Open);
                log = (LoginInfo[])fSrl.Deserialize(fStr);
                fStr.Close();
                Console.WriteLine("information loaded");

                for (int i = 0; i < log.Length; i++)
                {
                    if (log[i].username == textBox1.Text && log[i].password == textBox2.Text)
                    {
                        nome = log[i].username;
                        var form = new home();
                        form.Show();
                        this.Hide();
                        return;
                    }
                }
                MessageBox.Show("Username ou Password errada");
            }
            else
            {
                if (MessageBox.Show("Não tem nenhuma conta criada.\nQuer criar uma conta?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var form = new register();
                    form.Show();
                    this.Hide();
                    return;
                }
            }
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new register();
            form.Show();
            this.Hide();
        }
    }
}
