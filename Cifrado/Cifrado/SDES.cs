using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifrado
{
    public class SDES
    {
        public string[] llaves = new string[2];
        private int[] P10 = new int[] { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
        private int[] P8 = new int[] { 6, 3, 7, 4, 8, 5, 10, 9 };
        private int[] P4 = new int[] { 2, 4, 3, 1 };
        private int[] EP = new int[] { 4, 1, 2, 3, 2, 3, 4, 1 };
        private int[] PI = new int[] { 2, 6, 3, 1, 4, 8, 5, 7 };
        private int[] PIN = new int[] { 4, 1, 3, 5, 7, 2, 8, 6 };


        public string Leftshift(string valor, int n)
        {

            var ls = valor + valor; // "ABCDEFGHABCDEFGH"

            return ls.Substring(n, valor.Length);//BCDEFGHA

        }
        public string Salida(string valor, int[] permutacion)
        {
            string salida = "";
            for (int i = 0; i < permutacion.Length; i++)
            {
                salida += valor[permutacion[i] - 1];
            }
            return salida;
        }
        public string SW(string entrada)
        {
            return entrada.Substring((entrada.Length / 2) - 1, entrada.Length/2) + entrada.Substring(0, entrada.Length/2);
        }
        public void GenerarLlaves(string llave)
        {
            string aux = Salida(llave,P10);
            string lsl = aux.Substring(0, 5);
            string lsr = aux.Substring(5, 5);
            for (int i = 0; i < llaves.Length; i++)
            {
                lsl = Leftshift(lsl, 1);
                lsr = Leftshift(lsr, 1);
                llaves[i]=Salida(lsl+lsr,P8);                
            }
        }
        public byte CipherText(byte entrada)
        {
            string entradabinaria=Convert.ToString(entrada, 2).PadLeft(8, '0');

            byte byteCifrdo=0;
            return byteCifrdo;
        }
        //public string xor(string E/p)
        //{
        //    return E/p;
        //}
    }
}
