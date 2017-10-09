using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifrado
{
    public class SDES
    {

        private int[] P10 = new int[] { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
        private int[] P8 = new int[] { 6, 3, 7, 4, 8, 5, 10, 9 };
        private int[] P4 = new int[] { 2, 4, 3, 1 };
        private int[] EP = new int[] { 4, 1, 2, 3, 2, 3, 4, 1 };
        private int[] PI = new int[] { 2, 6, 3, 1, 4, 8, 5, 7 };
        private int[] PIN = new int[] { 4, 1, 3, 5, 7, 2, 8, 6 };


        public string Leftshift(string valor, int n)
        {

            var ss = valor + valor; // "ABCDEFGHABCDEFGH"77

            return ss.Substring(n, valor.Length);

        }
        public string prueba(string valor)
        {

            var ss = valor + valor; // "ABCDEFGHABCDEFGH"77

            return SalidaP10(Leftshift(SalidaP10(valor, P10), 1), P8);

        }
        public string SalidaP10(string valor, int[] permutacion)
        {
            string salida = "";
            for (int i = 0; i < permutacion.Length; i++)
            {
                salida += valor[permutacion[i] - 1];
            }
            return salida;
        }

    }
}
