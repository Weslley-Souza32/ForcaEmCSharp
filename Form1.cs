using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ForcaEmCSharp
{
    public partial class Form1 : Form
    {
        const int X = 73, Y = 303;
        string palavraDigitada = Inicio.palavra.ToUpper(), dicaDigitada = Inicio.dica;
        Label[] labels;
        Image[] forca = new Image[8];
        char[] acentos = new char[6];
        int tamanhoVetor, erros = 0;
        public Form1()
        {
            IniciarLabels();
            IniciarImagem();
            InitializeComponent();

            lblDica.Text = $"Dica: {dicaDigitada}";
        }

        private void IniciarImagem()
        {
            forca[0] = Properties.Resources.forca0;
            forca[1] = Properties.Resources.forca1;
            forca[2] = Properties.Resources.forca2;
            forca[3] = Properties.Resources.forca3;
            forca[4] = Properties.Resources.forca4;
            forca[5] = Properties.Resources.forca5;
            forca[6] = Properties.Resources.forca6;
            forca[7] = Properties.Resources.forca7;
        }

        private void IniciarLabels()
        {
            tamanhoVetor = palavraDigitada.Length;
            labels = new Label[tamanhoVetor];

            for (int i = 0; i < palavraDigitada.Length; i++)
            {
                labels[i] = new Label();
                labels[i].Font = new Font("Rockwell", 35F, FontStyle.Bold);
                labels[i].ForeColor = Color.Black;
                labels[i].BackColor = Color.Transparent;
                labels[i].Size = new Size(68, 90);
                labels[i].Text = palavraDigitada[i] == ' ' ? " " : "_";
                labels[i].Location = i == 0 ? new Point(X, Y) : new Point((labels[i - 1].Location.X + 56), Y);
                this.Controls.Add(labels[i]);
            }
        }

        private void btnConsoantes(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (palavraDigitada.Contains(Convert.ToChar(btn.Text)))
            {
                for (int i = 0; i < palavraDigitada.Length; i++)
                {
                    if (palavraDigitada[i].ToString() == btn.Text)
                        labels[i].Text = btn.Text;
                }
            }
            else
            {
                erros++;
                pbForca.Image = forca[erros];
            }
            btn.Enabled = false;
            ChegarFinal();
        }

        private void btnVogais(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var caractere = ' ';

            for (int i = 0; i < palavraDigitada.Length; i++)
            {
                acentos = btn.Text.VerificarVetor();
                caractere = palavraDigitada[i].VerificarLetra(acentos);

                if (caractere == palavraDigitada[i])
                {
                    labels[i].Text = caractere.ToString();
                }
            }
            acentos = btn.Text.VerificarVetor();
            VerificarErros(acentos);
            btn.Enabled = false;
            ChegarFinal();
        }

        private void VerificarErros(char[] acento)
        {
            var verificar = acento.Count(v => palavraDigitada.Contains(v));
            if (verificar == 0)
            {
                erros++;
                pbForca.Image = forca[erros];
            }
        }

        private void ChegarFinal()
        {
            if (erros == 7)
            {
                MessageBox.Show($"Fim De Jogo! A Resposta Correta era: {palavraDigitada}");
                JogarNovamente();
            }
            else
            {
                var acertos = labels.Count(l => l.Text != "_");
                if (acertos == palavraDigitada.Length)
                {
                    MessageBox.Show($"Parabéns Você Descobriu a Palavra");
                    JogarNovamente();
                }
            }
        }

        private void JogarNovamente()
        {
            DialogResult msg = MessageBox.Show("Deseja Jogar Novamente?", "Dialogo", MessageBoxButtons.YesNo);

            if (msg == DialogResult.Yes)
            {
                Inicio inicio = new Inicio();
                inicio.Show();
                this.Enabled = this.Visible = false;
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
