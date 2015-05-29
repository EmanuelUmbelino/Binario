using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binario
{
    class Calculator
    {
        public double sl;
        public string simbolinho;
        public double valor = 0;
        public double valor2 = 0;
        public double total = 0;
        // inverter string
        string Inverter(string Texto)
        {
            //Cria a partir do texto original um array de char
            char[] ArrayChar = Texto.ToCharArray();
            //Com o array criado invertemos a ordem do mesmo
            Array.Reverse(ArrayChar);
            //Agora basta criarmos uma nova String, ja com o array invertido.
            return new string(ArrayChar);
        }
        //funcao para somar binario
        private double soma(double valor, double valor2)
        {
            string atual = valor.ToString();
            string atual2 = valor2.ToString();
            StringBuilder genValue = new StringBuilder(atual);
            StringBuilder genValue2 = new StringBuilder(atual2);
            bool carregado = false;
            string result = "";
            if (genValue.Length > genValue2.Length)
            {
                for (int i = 1; i < genValue.Length ; i++)
                {
                    genValue2.Insert(0, '0');
                }
            }
            else if (genValue.Length < genValue2.Length)
            {
                for (int i = 1; i < genValue2.Length ; i++)
                {
                    genValue.Insert(0, '0');
                }
            }
            atual = Inverter(genValue.ToString());
            atual2 = Inverter(genValue2.ToString());
            for (int i = 0; i < atual.Length; i++)
            {
               if(atual[i]!= '0')
                   break;
               if (i.Equals(atual.Length -1))
                   result = atual2;

            }
            for (int i = 0; i < atual2.Length; i++)
            {
               if(atual2[i]!= '0')
                   break;
                if(i.Equals(atual2.Length - 1))
                    result = atual;
            }
            if(result.Equals(""))
            {
                for (int i = 0; i < atual.Length; i++)
                {
                    switch (atual[i])
                    {
                        case '1':
                            if (atual2[i].Equals('1'))
                            {
                                if (carregado)
                                    result += "1";
                                else
                                {
                                    result += "0";
                                    carregado = true;
                                }
                            }
                            else
                            {
                                if (carregado)
                                    result += "0";
                                else
                                    result += "1";
                            }
                            break;
                        case '0':
                            if (atual2[i].Equals('1'))
                            {
                                if (carregado)
                                    result += "0";
                                else
                                    result += "1";
                            }
                            else
                            {
                                if (carregado)
                                {
                                    result += "1";
                                    carregado = false;
                                }
                                else
                                    result += "0";
                            }
                            break;
                    }
                    if (carregado && i.Equals(atual.Length - 1))
                        result += "1";
                }
            }
            result = Inverter(result);
            return Convert.ToDouble(result);
        }
        //funcao para multiplicar binario
        private double mult(double valor, double valor2)
        {
            string atual = Inverter(valor.ToString());
            string atual2 = Inverter(valor2.ToString());
            string[] result = new string[atual.Length];
            string finalresult = "";
            for (int i = 0; i < atual.Length; i++)
            {
                for (int t = 0; t < i; t++)
                {
                    result[i] += "0";
                }
                for (int f = 0; f < atual2.Length; f++) 
                {
                    if (atual[i].Equals('1') && atual2[f].Equals('1'))
                        result[i] += "1";
                    else
                        result[i] += "0";
                }
                result[i] = Inverter(result[i]);
            }
            atual = result[0];
            for (int i = 1; i < result.Length; i++)
            {
                atual = soma(Convert.ToDouble(result[i]), Convert.ToDouble(atual)).ToString();
            }

            return Convert.ToDouble(atual);
        }
        //funcao para subtrair binario
        private double sub(double valor, double valor2)
        {
            string atual = valor.ToString();
            string atual2 = valor2.ToString();
            StringBuilder genValue = new StringBuilder(atual);
            StringBuilder genValue2 = new StringBuilder(atual2);
            bool carregado = false;
            string result = "";
            if (genValue.Length > genValue2.Length)
            {
                for (int i = 0; i < genValue.Length - 1; i++)
                {
                    genValue2.Insert(0, '0');
                }
            }
            else if (genValue.Length < genValue2.Length)
            {
                for (int i = 0; i < genValue2.Length - 1; i++)
                {
                    genValue.Insert(0, '0');
                }
            }
            atual = genValue.ToString();
            atual2 = genValue2.ToString();
            for (int i = 1; i < atual.Length; i++)
            {
                if (atual[i].Equals('1'))
                {

                    if (atual2[i].Equals('1')) result += "0";
                    else result += "1";
                }
                else
                {

                    if (atual2[i].Equals('1'))
                    {
                        MessageBox.Show("sda");
                       /* if (carregado)
                            result += "0";
                        else
                            result += "1";*/
                    }
                    else
                    {
                        if (carregado)
                        {
                            result += "1";
                            carregado = false;
                        }
                        else
                        {
                            result += "0";
                        }

                    }
                }
                if (carregado && i.Equals(atual.Length - 1))
                    result += "1";
            }
            result = Inverter(result);
            return Convert.ToDouble(result);
        }
        //funcao para converter binario para decimal
        public double dec(double valor)
        {
            string medido = valor.ToString();
            double result = 0;
            for (int i = 0; i < medido.Length; i++)
            {
                if (medido[i].ToString() != "0" && medido[i].ToString() != "1")
                {
                    MessageBox.Show("Apenas Valores 0 e 1"); result = 0;
                    break;
                }
                else
                    result = result * 2 + Convert.ToDouble(medido[i].ToString());
            }
            return result;
        }
        //funcao para converter  binario para hexadecimal
        public string hex(double valor)
        {
            string result = "";
            string atual = valor.ToString();
            StringBuilder genValue = new StringBuilder(atual);
            StringBuilder pedaco = new StringBuilder("0000");
            for (int i = 0, length = genValue.Length; i < (4 - length % 4) % 4; i++)
            {
                genValue.Insert(0, '0');
            }

            for (int i = 0; i < genValue.Length; i += 4)
            {
                for (int j = i; j < i + 4; j++)
                {
                    pedaco[j % 4] = genValue[j];
                }
                switch (pedaco.ToString())
                {
                    case "0000":
                        result += "0";
                        break;
                    case "0001":
                        result += "1";
                        break;
                    case "0010":
                        result += "2";
                        break;
                    case "0011":
                        result += "3";
                        break;
                    case "0100":
                        result += "4";
                        break;
                    case "0101":
                        result += "5";
                        break;
                    case "0110":
                        result += "6";
                        break;
                    case "0111":
                        result += "7";
                        break;
                    case "1000":
                        result += "8";
                        break;
                    case "1001":
                        result += "9";
                        break;
                    case "1010":
                        result += "A";
                        break;
                    case "1011":
                        result += "B";
                        break;
                    case "1100":
                        result += "C";
                        break;
                    case "1101":
                        result += "D";
                        break;
                    case "1110":
                        result += "E";
                        break;
                    case "1111":
                        result += "F";
                        break;
                }
            }
            return result;
        }

        // funcao para calcular
        public void calcular()
        {
            // pegar o valor atual
            valor2 = sl;
            // executar as contas no "total"
            switch (simbolinho)
            {
                case "-":
                    total = sub(valor, valor2);
                    break;
                case "+":
                    total = soma(valor, valor2);
                    break;
                case "x":
                    total = mult(valor, valor2);
                    break;
                case "/":
                    total = soma(valor, valor2);
                    break;
            }
        }
    }
}
