using System;
using NCalc;
using System.Collections.Generic;

namespace FindTautologyApp
{
    class Program
    {
        static String VariaveisBoleanas = "";
        static string ExpreçãoDigitada = "";
        static string Expreção1 = "";
        static string Expreção2 = "";
        static List<String> ListaDeExpreções = new List<string>();

        static void Main(string[] args)
        {


            ExpreçãoDigitada = Console.ReadLine();

            ExpreçãoDigitada = ExpreçãoDigitada.ToUpper();


            int start = ExpreçãoDigitada.IndexOfAny(new char[] { '<', '-', '>'});
            int end = ExpreçãoDigitada.LastIndexOfAny(new char[] { '<', '-', '>' });

            String primeiraExpresão = ExpreçãoDigitada.Substring(0, start);

            String SegExpresão = ExpreçãoDigitada.Substring(end+2);

            Console.WriteLine(primeiraExpresão);
            Console.WriteLine(SegExpresão);

            bool[] resposta1 = ObtemTabelaVerdade(primeiraExpresão);
            bool[] resposta2 = ObtemTabelaVerdade(SegExpresão);

            if(resposta1.Length != resposta2.Length)
            {
                Console.WriteLine("Não É taltologia");
                return;
            }
            else
            {
                for (int i = 0; i < resposta1.Length; i++)
                {
                    if(resposta1[i] != resposta2[i])
                    {
                        Console.WriteLine("Não É taltologia");
                        return;
                    }
                }
            }

            Console.WriteLine("É taltologia");


        }

        public static bool[] ObtemTabelaVerdade(string ExpreçãoDigitada)
        {
            VariaveisBoleanas = percorreListaEContaVariaveisBoleanas(ExpreçãoDigitada.Replace(" ", "").ToCharArray());

            int númeroDeLinhasDaMatriz = (int)Math.Pow(2, VariaveisBoleanas.Length);
            int númeroDecolunasDaMatriz = VariaveisBoleanas.Length;

            bool[,] ArrayDeValoresVerdades = new bool[númeroDeLinhasDaMatriz, númeroDecolunasDaMatriz];

            PreencherTabelaVerdade(ref ArrayDeValoresVerdades, númeroDeLinhasDaMatriz, númeroDecolunasDaMatriz);

            string ExpreçãoProcessada = ExpreçãoDigitada;

            ExpreçãoProcessada = ExpreçãoProcessada.Replace("~", "not");
            ExpreçãoProcessada = ExpreçãoProcessada.Replace("^", "and");
            ExpreçãoProcessada = ExpreçãoProcessada.Replace("V", "or");

            bool[] Respostas = new bool[númeroDeLinhasDaMatriz];

            for (int i = 0; i < númeroDeLinhasDaMatriz; i++)
            {
                string ExpreçãoCopia = ExpreçãoProcessada;
                char[] valores = VariaveisBoleanas.ToCharArray();

                for (int j = 0; j < númeroDecolunasDaMatriz; j++)
                {
                    ExpreçãoCopia = ExpreçãoCopia.Replace(valores[j].ToString(), ArrayDeValoresVerdades[i, j].ToString().ToLower());
                }

                Respostas[i] = (bool)(new Expression(ExpreçãoCopia).Evaluate());
                Console.WriteLine(Respostas[i]);
            }

            return Respostas;
        }


        public static void PreencherTabelaVerdade( ref bool[,] ArrayDeValoresVerdades, int númeroDeLinhasDaMatriz, int númeroDecolunasDaMatriz)
        {
            int VariaçãoDeValoresVerdades = númeroDeLinhasDaMatriz;
            bool valor = true;

            for(int j=0; j < númeroDecolunasDaMatriz; j++)
            {
                VariaçãoDeValoresVerdades /= 2;
                valor = true;
                int k = 0;

                for (int i=0; i < númeroDeLinhasDaMatriz; i++, k++)
                {
                    if(k== VariaçãoDeValoresVerdades)
                    {
                        k = 0;
                        valor = !valor;
                    }

                    ArrayDeValoresVerdades[i, j] = valor;
                }
            }

        }
 

        public static string ContaExpreções()
        {
            char[] expreçãoCharArray = ExpreçãoDigitada.ToCharArray();
            string expreçãoCopia = ExpreçãoDigitada;
            string ExpreçãoCapturada = "";

            for (int i=0; i < expreçãoCharArray.Length; i++)
            {
                
            }

            return null;
        }
        public static void AddExpreçãoPrioritaria(char[] expreção)
        {
            int i = 0;
            string Expreção = "";

            while(i < expreção.Length)
            {
                if(expreção[i] == ')')
                {
                    AddExpreçãoPrioritaria(expreção);
                }

                Expreção += expreção[i];
                i++;

                if (expreção[i] == ')')
                    break;
            }

            ListaDeExpreções.Add(Expreção);
        }

        public static string percorreListaEContaVariaveisBoleanas(char[] expreção)
        {
            string VariaveisExistentes = "";

            foreach (var caracter in expreção)
            {
                if (caracter >= 'A' && caracter <= 'Z' && caracter != 'V')
                {
                    if (!VariaveisExistentes.Contains(caracter))
                    {
                        VariaveisExistentes += caracter;
                    }
                }
            }

            return VariaveisExistentes;
        }
    }
}
