using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cifrado
{
    public class SDES
    {
        private string path;
        private string data;
        private string extencionArchivo;
        public string _data { get; set; }
        public string _path { get; set; }
        public string[] keys = new string[2];
        public string key = "";
        private int[] P10 = new int[] { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
        private int[] P8 = new int[] { 6, 3, 7, 4, 8, 5, 10, 9 };
        private int[] P4 = new int[] { 2, 4, 3, 1 };
        private int[] EP = new int[] { 4, 1, 2, 3, 2, 3, 4, 1 };
        private int[] PI = new int[] { 2, 6, 3, 1, 4, 8, 5, 7 };
        private int[] PIn = new int[] { 4, 1, 3, 5, 7, 2, 8, 6 };
        private string[,] s0 = new string[4,4] { { "01", "00", "11", "10" }, { "11", "10", "01", "00" }, { "00", "10", "01", "11" }, { "11", "01", "11", "10" } };
        private string[,] s1 = new string[4, 4] { { "00", "01", "10", "11" }, { "10", "00", "01", "11" }, { "11", "00", "01", "00" }, { "10", "01", "00", "11" } };


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
        public string SW(string input)
        {
            return input.Substring((input.Length / 2), input.Length/2) + input.Substring(0, input.Length/2);
        }
        public void GenerateKeys(string unallave)
        {
            string aux = Salida(unallave, P10);
            string lsl = aux.Substring(0, 5);
            string lsr = aux.Substring(5, 5);
            for (int i = 0; i < keys.Length; i++)
            {
                lsl = Leftshift(lsl, 1);
                lsr = Leftshift(lsr, 1);
                keys[i]=Salida(lsl+lsr,P8);                
            }
        }
        public string xor(string input,string _keys)
        {
            string salida = "";
            for (int i = 0; i < input.Length; i++)
            {
                salida += (input[i] == _keys[i]) ? 0 : 1;
            }
            return salida;
        }
        private byte[] LecturaArchivo(string path)
        {
            FileStream Source = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader lecturaBytes = new BinaryReader(Source);
            byte[] bytesLeidos = new byte[Source.Length];
            bytesLeidos = lecturaBytes.ReadBytes(Convert.ToInt32(Source.Length));
            Source.Flush();
            Source.Close();
            return bytesLeidos;

        }
        private void escribirArchivo(byte[] bytesComprimidos,string ext)
        {
            string folderName = @"c:\Archivos Cifrados";
            Directory.CreateDirectory(folderName);
            DirectoryInfo archivo = new DirectoryInfo(_path);
            string nombrenuearchivo = archivo.Name.Substring(0, (archivo.Name.Length - archivo.Extension.Length));
            string pathNew = Path.Combine(folderName, (nombrenuearchivo + ext));
            byte[] bytes = bytesComprimidos;
            FileStream fsNew = new FileStream(pathNew, FileMode.Create, FileAccess.Write);
            fsNew.Write(bytes, 0, bytes.Length);
            fsNew.Flush();
            fsNew.Close();
        }
        public void Cipher(string path, string _key)
        {
            _path = path;
            key = _key;
            // Guardar extencion
            DirectoryInfo archivo = new DirectoryInfo(_path);
            List<byte> lista = new List<byte>();
            byte bytesCantidad;
            byte[] byteExtencion = ConvertirBinarioYTexto(archivo.Extension);
            bytesCantidad = Convert.ToByte(archivo.Extension.Length);
            lista.Add(bytesCantidad);
            for (int i = 0; i < byteExtencion.Length; i++)
            {
                lista.Add(CipherByte(byteExtencion[i]));
            }
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            for (int i = 0; i < bytes.Length; i++)
            {
                lista.Add(CipherByte(bytes[i]));
            }

            //Escritura del archivo Comprimido
            escribirArchivo(lista.ToArray(), ".Cif");
        }
        public void DesCipher(string path, string _key)
        {
            _path = path;
            key = _key;
            List<byte> listaDescomprimida = new List<byte>();     
            //Lectura de Bytes        
            byte[] bytes = LecturaArchivo(_path);
            int tamañoExtencion = Convert.ToInt32(bytes[0]);
            byte[] bytesExtencion = new byte[tamañoExtencion];
            for (int i = 0; i < tamañoExtencion; i++)
            {
                bytesExtencion[i] = DesCipherByte(bytes[i + 1]);
            }
            extencionArchivo = ConvertirBinarioYTexto(bytesExtencion);
            for (int i = tamañoExtencion+1; i < bytes.Length; i++)
            {
                listaDescomprimida.Add(DesCipherByte(bytes[i]));
            }

            //Escritura del archivo 
            escribirArchivo(listaDescomprimida.ToArray(), extencionArchivo);
        }

        public byte CipherByte(byte input)
        {
            
            string entradabinaria=Convert.ToString(input, 2).PadLeft(8, '0');
            string switchinput = "";
            byte byteCifrdo = 0;
            GenerateKeys(key);
            entradabinaria = Salida(entradabinaria, PI);
            entradabinaria = feistel(entradabinaria, keys[0]);
            switchinput = SW(entradabinaria);
            switchinput= feistel(switchinput, keys[1]);
            switchinput = Salida(switchinput, PIn);
            byteCifrdo = Convert.ToByte(switchinput, 2);
            return byteCifrdo;
        }
        public byte DesCipherByte(byte input)
        {

            string entradabinaria = Convert.ToString(input, 2).PadLeft(8, '0');
            string switchinput = "";
            byte byteCifrdo = 0;
            GenerateKeys(key);
            entradabinaria = Salida(entradabinaria, PI);
            entradabinaria = feistel(entradabinaria, keys[1]);
            switchinput = SW(entradabinaria);
            switchinput = feistel(switchinput, keys[0]);
            switchinput = Salida(switchinput, PIn);
            byteCifrdo = Convert.ToByte(switchinput, 2);
            return byteCifrdo;
        }
        public string feistel(string input,string _key)
        {
            string left = "";
            string right = "";
            string expyper = "";
            string per4 = "";
            left = input.Substring(0, input.Length / 2);
            right = input.Substring((input.Length / 2) , input.Length / 2);
            expyper = Salida(right, EP);
            expyper = xor(expyper, _key);
            per4 = Sbox(expyper.Substring(0, expyper.Length / 2), s0) + Sbox(expyper.Substring((expyper.Length / 2) , expyper.Length / 2), s1);
            per4 = Salida(per4, P4);
            per4 = xor(left, per4);
            return per4 + right;
        }
        public string Sbox(string input, string[,] si)
        {
            int row=Convert.ToInt32(input.Substring(0,1)+input.Substring(3,1),2);
            int column=Convert.ToInt32(input.Substring(1, 1) + input.Substring(2, 1), 2); ;
            return si[row, column];
        }

        private string ConvertirBinarioYTexto(byte[] datosBinario)
        {
            return Encoding.ASCII.GetString(datosBinario);
        }
        private byte[] ConvertirBinarioYTexto(string datosTexto)
        {
            return Encoding.ASCII.GetBytes(datosTexto);
        }
    }
}
