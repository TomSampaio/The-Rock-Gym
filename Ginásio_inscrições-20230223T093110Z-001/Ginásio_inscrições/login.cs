using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ginásio_inscrições
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        LoginInfo[] log;
        private void button1_Click(object sender, EventArgs e)
        {
            LoginInfo.Load(log);

            for (int i = 0; i < log.Length; i++)
            {
                if (log[i].username == textBox1.Text && log[i].password == textBox2.Text)
                {
                    var form = new home();
                    form.Show();
                    this.Hide();
                    return;
                }
            }
            MessageBox.Show("Username ou Password errada");
        }
    }
}
