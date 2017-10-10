using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifrado
{
    class Program
    {
        static void Main(string[] args)
        {
            byte un = 75;
            SDES des = new SDES();
            Console.WriteLine("Ingrese la ruta del archivo");
            string path = Console.ReadLine();
            des.Cipher(path, "1010110010");
            Console.WriteLine("Ingrese la ruta del archivo");
            path = Console.ReadLine();
            des.DesCipher(path, "1010110010");
            //string j = Convert.ToString(un, 2).PadLeft(8, '0');
            //des.Cipher(path,"1010110010");
            //byte unn = Convert.ToByte(j, 2);
            //j[0].ToString();
            //Console.WriteLine(un.ToString() + " " + j + " " + k);
            Console.ReadKey();
        }
    }
}
