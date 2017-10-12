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
            DiffieHellman emitter = new DiffieHellman(56);
            DiffieHellman receiver = new DiffieHellman(56);
         
            var path = "";
            var option = "";
            bool flag = false;


            while(!flag)
            {
                Console.Clear();
                Console.WriteLine("¿Desea cifrar con una llave conocida?");
                Console.WriteLine("1.Si / 2.No");
                option = Console.ReadLine();
                if (option == "1")
                {
                    var keyA = AskKey(true);
                    var keyB = AskKey(false);
                    emitter = new DiffieHellman(56).GenerateRequest(keyA);
                    receiver = new DiffieHellman(56).GenerateResponse(emitter.ToString(), keyB);
                    emitter.HandleResponse(receiver.ToString());
                    flag = true;
                }
                else if(option == "2")
                {
                    emitter = new DiffieHellman(56).GenerateRequest();
                    receiver = new DiffieHellman(56).GenerateResponse(emitter.ToString());
                    emitter.HandleResponse(receiver.ToString());
                    flag = true;
                }
            }


            instructions();
           
            while (option.Trim() != "-s")
            {

                Console.Write("<Sistema de cifrado>");
                option = Console.ReadLine();
                if (option == "-c -f" )
                {
                    Console.WriteLine("Ingrese la ruta del archivo");
                    path = Console.ReadLine();

                    string llavebinanira = "";
                    for (int i = 0; i < 2; i++)
                    {
                       llavebinanira += Convert.ToString(emitter.Key[i], 2).PadLeft(8, '0');
                    }
                    des.Cipher(path, llavebinanira);
                    Console.WriteLine("Cifrado exitoso");
                }
                else if(option == "-d -f")
                {                    
                    Console.WriteLine("Ingrese la ruta del archivo");
                    path = Console.ReadLine();
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

        static int AskKey(bool flag)
        {
            var key = 0;
            if (flag)
            {
                Console.WriteLine("Ingrese su llave emisor");
                try
                {
                    key = int.Parse(Console.ReadLine());
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return key;
            }

            Console.WriteLine("Ingrese llave receptor");
            try
            {
                key = int.Parse(Console.ReadLine());
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return key;

        }
    }
}
