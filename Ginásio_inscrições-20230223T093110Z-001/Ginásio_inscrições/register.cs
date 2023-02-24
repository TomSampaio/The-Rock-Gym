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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUSer.Text.Length < 3 || txtPass.Text.Length < 3 || txtConfirm.Text.Length < 3 || txtAge.Text == "" || txtWeight.Text.Length < 3 || txtHeight.Text.Length < 3 || cmbGender.Text == "" || cmbGoal.Text == "" || cmbPlan.Text == "")
            {
                MessageBox.Show("Tem que preencher tudo");
                return;
            }
            else if (txtPass.Text != txtConfirm.Text)
            {
                MessageBox.Show("As passwords não são iguais");
                return;
            }
            else if (int.TryParse(txtAge.Text, out int a) == false || double.TryParse(txtWeight.Text, out double b) == false || double.TryParse(txtHeight.Text, out double c) == false)
            {

            }
        }
    }
}
