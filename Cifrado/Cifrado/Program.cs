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
            var keyA = 0;
            var keyB = 0;

            do
            {
                Console.Clear();
                if(keyA == 0 && keyA < 734375557)
                {
                    keyA = AskKey(true);
                }
                if (keyB == 0 && keyB < 734375557 || keyA == keyB)
                {
                    keyB = AskKey(false);
                }              
                if (keyA != keyB && keyA != 0 && keyB != 0)
                {
                    flag = true;
                    emitter = new DiffieHellman(56).GenerateRequest(keyA);
                    receiver = new DiffieHellman(56).GenerateResponse(emitter.ToString(), keyB);
                    emitter.HandleResponse(receiver.ToString());
                    Console.WriteLine("La llave es: {0}", conversions.ToDecimal(Convert.ToBase64String(emitter.Key)));
                }
            } while (!flag);


            instructions();
           
            while (option.Trim() != "-s")
            {
                Console.Write("<Sistema de cifrado>");
                option = Console.ReadLine();
                if (option.Contains("-c -f"))
                {
                    path = option.Substring(5, option.Length - 5).Trim();

                    string llavebinanira = "";
                    for (int i = 0; i < 2; i++)
                    {
                        llavebinanira += Convert.ToString(emitter.Key[i], 2).PadLeft(8, '0');
                    }
                    des.Cipher(path, llavebinanira);
                    Console.WriteLine("Cifrado exitoso");
                }
                else if (option.Contains("-d -f"))
                {
                    path = option.Substring(5, option.Length - 5).Trim();
                    string llavebinanira = "";
                    for (int i = 0; i < 2; i++)
                    {
                        llavebinanira += Convert.ToString(receiver.Key[i], 2).PadLeft(8, '0');
                    }
                    des.DesCipher(path, llavebinanira);
                    Console.WriteLine("Descifrado exitoso");
                }
                else if(option == "-n")
                {
                    flag = false;
                    keyA = 0;
                    keyB = 0;
                    do
                    {
                        Console.Clear();
                        if (keyA == 0 && keyA < 734375557)
                        {
                            keyA = AskKey(true);
                        }
                        if (keyB == 0 && keyB < 734375557 || keyA == keyB)
                        {
                            keyB = AskKey(false);
                        }
                        if (keyA != keyB && keyA != 0 && keyB != 0)
                        {
                            flag = true;
                            emitter = new DiffieHellman(56).GenerateRequest(keyA);
                            receiver = new DiffieHellman(56).GenerateResponse(emitter.ToString(), keyB);
                            emitter.HandleResponse(receiver.ToString());
                            Console.WriteLine("La llave es: {0}", conversions.ToDecimal(Convert.ToBase64String(emitter.Key)));
                        }
                    } while (!flag);
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
            Console.WriteLine("Ingrese -n para agregar nueva llave");
            Console.WriteLine("Ingrese -s para salir");
        }

        static int AskKey(bool flag)
        {
         
            var key = 0;
            if (flag)
            {
                Console.WriteLine("Ingrese su llave emisor");
                var validNumber = Int32.TryParse(Console.ReadLine(), out key);
                if (validNumber && key > 0)
                    return key;
            }
            else
            {
                Console.WriteLine("Ingrese llave receptor");
                var validNumr = Int32.TryParse(Console.ReadLine(), out key);
                if (validNumr && key > 0)
                    return key;
            }
            Console.WriteLine("Ingrese un valor valido");
            return 0;
        }
    }
}
