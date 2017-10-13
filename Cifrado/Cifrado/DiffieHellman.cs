using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cifrado
{
    class DiffieHellman
    {

        private int bits = 256;
        Random generator;
        Utilities conversions;
        BigInteger prime;

        BigInteger g;

        BigInteger mine;

        byte[] key;

        string representation;

        public byte[] Key
        {
            get { return key; }
        }
        public DiffieHellman(int bits)
        {
            this.bits = bits;
            generator = new Random();
            conversions = new Utilities();

        }


        public DiffieHellman GenerateRequest()
        {

            prime = (BigInteger)734375557;

            mine = generator.Next(100000000, 734375556);
            g = (BigInteger)5;

            StringBuilder rep = new StringBuilder();
            rep.Append(conversions.ConvertToBase(prime, 36));
            rep.Append("|");
            rep.Append(conversions.ConvertToBase(g, 36));
            rep.Append("|");

            BigInteger send = BigInteger.ModPow(g, mine, prime);
            rep.Append(conversions.ConvertToBase(send, 36));

            representation = rep.ToString();
            return this;
        }

        public DiffieHellman GenerateRequest(int _mine)
        {

            prime = (BigInteger)734375557;
            mine = _mine;
            g = (BigInteger)5;

            StringBuilder rep = new StringBuilder();
            rep.Append(conversions.ConvertToBase(prime, 36));
            rep.Append("|");
            rep.Append(conversions.ConvertToBase(g, 36));
            rep.Append("|");

            BigInteger send = BigInteger.ModPow(g, mine, prime);
            rep.Append(conversions.ConvertToBase(send, 36));

            representation = rep.ToString();
            return this;
        }

        public DiffieHellman GenerateResponse(string request, int _mine)
        {
            string[] parts = request.Split('|');

            BigInteger prime = conversions.ToDecimal(parts[0]);
            BigInteger g = conversions.ToDecimal(parts[1]);
            BigInteger mine = _mine;
            BigInteger given = conversions.ToDecimal(parts[2]);
            BigInteger key = BigInteger.ModPow(given, mine, prime);
            this.key = key.ToByteArray();
            this.key = this.key.Reverse().ToArray();
            BigInteger send = BigInteger.ModPow(g, mine, prime);
            this.representation = conversions.ConvertToBase(send, 36);

            return this;
        }

        public DiffieHellman GenerateResponse(string request)
        {
            string[] parts = request.Split('|');

            BigInteger prime = conversions.ToDecimal(parts[0]);
            BigInteger g = conversions.ToDecimal(parts[1]);
            BigInteger mine = generator.Next(1000000, 734375557);
            BigInteger given = conversions.ToDecimal(parts[2]);
            BigInteger key = BigInteger.ModPow(given, mine, prime);
            this.key = key.ToByteArray();
            this.key = this.key.Reverse().ToArray();
            BigInteger send = BigInteger.ModPow(g, mine, prime);
            this.representation = conversions.ConvertToBase(send, 36);

            return this;
        }

        public void HandleResponse(string response)
        {
            BigInteger given = conversions.ToDecimal(response);
            BigInteger key = BigInteger.ModPow(given, mine, prime);
            this.key = key.ToByteArray();
            this.key = this.key.Reverse().ToArray();

        }

        public override string ToString()
        {
            return representation;
        }
    }
}
