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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Utilities conversions = new Utilities();
            SDES des = new SDES();
            DiffieHellman emitter = new DiffieHellman(56).GenerateRequest();
            DiffieHellman receiver = new DiffieHellman(56).GenerateResponse(emitter.ToString());
            emitter.HandleResponse(receiver.ToString());
            var path = "";
            var option = "";
            instructions();
            Console.WriteLine(Convert.ToBase64String(emitter.Key));
            Console.WriteLine(Convert.ToBase64String(receiver.Key));
            while (option.Trim() != "-s")
            {
                Console.Write("<Sistema de cifrado>");
                option = Console.ReadLine();
                if (option == "-f")
                {

                    Console.WriteLine("Ingrese la ruta del archivo");
                    path = Console.ReadLine();                 
                }
                else if(option == "-c")
                {
                    string llavebinanira = "";
                    for (int i = 0; i < 2; i++)
                    {
                        llavebinanira += Convert.ToString(emitter.Key[i], 2).PadLeft(8,'0');
                    }
                    des.Cipher(path, llavebinanira);
                    Console.WriteLine("Cifrado exitoso");
                }
                else if(option == "-d")
                {
                    string llavebinanira = "";
                    for (int i = 0; i < 2; i++)
                    {
                        llavebinanira += Convert.ToString(receiver.Key[i], 2).PadLeft(8, '0');
                    }
                    des.DesCipher(path, llavebinanira);
                    Console.WriteLine("Descifrado exitoso");
                }
                else
                {
                    Console.WriteLine("{0}  no se reconoce como un comando interno o externo, programa o archivo por lotes ejecutable.", option);
                }
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void instructions()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Ingrese -f para ingresar la ruta del archivo");
            Console.WriteLine("Ingrese -c para cifrar");
            Console.WriteLine("Ingrese -d para descifrar");
            Console.WriteLine("Ingrese -s para salir");
        }
    }
}
