using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binario
{
    public partial class Form1 : Form
    {
        Calculator calculator = new Calculator();
        public Form1()
        {
            InitializeComponent();
        }
        public double negativando;
        public string pegar;
        public bool simbol = false;
        public bool somou = false;
        public bool algo = false;
        public bool batata = false;

        // add caracteres
        private void Add(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            // verificar se tem algum digito escrito na tela
            algo = true;
            // respectivamente: testando se vai colocar o primeiro digito; colocando mais um digito; negando o zero;
            if (Result.Text.Length <= 15)
            {
                if (simbol || somou) { Result.Text = clicked.Text; simbol = false; somou = false; }
                else if (Result.Text != "0") Result.Text += clicked.Text;
                else Result.Text = clicked.Text;
            }
        }

        // reiniciar tudo
        private void Reset(object sender, EventArgs e)
        {
            Result.Text = "0";
            calculator.simbolinho = null;
            calculator.valor = 0;
            calculator.valor2 = 0;
            calculator.total = 0;
            simbol = false;
            algo = false;
        }
        // executar a conta
        private void Total(object sender, EventArgs e)
        {
            // testar se existe algum simbolo ativado
            if (calculator.simbolinho != null)
            {
                // pegar o valor escrito
                calculator.sl = Convert.ToDouble(Result.Text);
                // usar a funçao da conta
                calculator.calcular();
                // digitar o total
                Result.Text = calculator.total.ToString();
                // pegar o valor escrito
                calculator.sl = Convert.ToDouble(Result.Text);
                // definir que valor sera o valor escrito
                calculator.valor = calculator.sl;
                // zerador
                calculator.simbolinho = null;
                batata = true;
                somou = true;
            }
        }
        // escolher a conta
        private void Add_simbolos(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            // ver se algo foi digitado
            if (algo) calculator.sl = Convert.ToDouble(Result.Text);
            if (batata && algo) { calculator.valor = calculator.sl; batata = false; }
            // caso aperte um botao de sinal ja tendo definido uma conta anterior
            if (calculator.simbolinho != null)
            {
                // mesmo que no total exceto por colocar o simbolo clicado para a prox conta
                calculator.calcular();
                Result.Text = calculator.total.ToString();
                calculator.sl = Convert.ToDouble(Result.Text);
                calculator.valor = calculator.sl;
                calculator.simbolinho = clicked.Text;
                somou = true;
            }
            // criar o simbolo
            if (calculator.simbolinho == null && algo) calculator.simbolinho = clicked.Text;
            // pegar segundo valor ou primeiro valor
            if (calculator.valor != 0) calculator.valor2 = calculator.sl;
            else calculator.valor = calculator.sl;
            if (algo && calculator.simbolinho.Equals("ConvertToDec") || algo && calculator.simbolinho.Equals("ConvertToHex"))
            {
                calculator.calcular();
                Result.Text = calculator.total.ToString();
                calculator.sl = Convert.ToDouble(Result.Text);
                calculator.valor = calculator.sl;
                calculator.simbolinho = null;
                somou = true;
            }
            simbol = true;
        }

        // deletar caracter final
        private void Backspace(object sender, EventArgs e)
        {
            if (Result.Text.Length > 1)
                Result.Text = Result.Text.Remove(Result.Text.Length - 1);
            else Result.Text = "0";
        }

        // verifica se o valor digitado é 0 ou 1
        private void Aprovador(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 48 && e.KeyChar != 8 && e.KeyChar != 49)
                e.Handled = true;
            if (e.KeyChar.Equals(8) && Result.Text.Length.Equals(1))
            {
                e.Handled = true;
            }
        }
        // inverter sinal
        private void Negativar(object sender, EventArgs e)
        {
            negativando = Convert.ToDouble(Result.Text) * -1;
            Result.Text = negativando.ToString();
        }
        // colocar valores nas outras barras
        private void AutoValues(object sender, EventArgs e)
        {
            Decimal.Text = (calculator.dec(Convert.ToDouble(Result.Text))).ToString();
            Hexadecimal.Text = calculator.hex(Convert.ToDouble(Result.Text));
        }
    }
}
