using System;
using System.Windows.Forms;

namespace ForcaEmCSharp
{
    public partial class Inicio : Form
    {
        public static string palavra, dica;
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            palavra = txtPalavra.Text;
            dica = txtDica.Text;

            if(!string.IsNullOrEmpty(txtPalavra.Text) && !string.IsNullOrEmpty(txtDica.Text))
            {
                Form1 form = new Form1();
                form.Show();
                this.Visible = this.Enabled = false;
                
            }
            else
            {
                MessageBox.Show("Informe uma palavra e a sua dica.","ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtPalavra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar))
                && (!char.IsWhiteSpace(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }


        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
