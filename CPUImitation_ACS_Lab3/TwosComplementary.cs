using System;
using System.Collections.Generic;
using System.Text;

namespace CPUImitation_ACS_Lab3
{
    static class TwosComplementary
    {
        private const int REGISTRY_SIZE = 22;
        public static byte[] Convert(int number) 
        {
            byte[] result = new byte[REGISTRY_SIZE];

            if (number > Math.Pow(2, REGISTRY_SIZE - 1)) 
            {
                throw new Exception("Overflow happened my dudes....");
            }

            int sign = Math.Sign(number);
            if (sign == -1)
            { 
                number = -number;
            }
            // no matter the sign of the orig. number, it will be 0 due to conversion algorithm
            result[0] = 0;
            int i = 0;
            while (number > 0) 
            {
                result[22 - i - 1] = (byte)(number % 2);
                number /= 2;
                i++;
            }

            Console.WriteLine("In binary representation:");
            i = 0;
            foreach (byte registryByte in result)
            {
                Console.Write(registryByte);
            }
            Console.WriteLine();

            // inverting number representation
            for (int j = 0; j < 22; j++)
            {
                result[j] = (result[j] == 0)
                            ? (byte)1
                            : (byte)0;

            }


            Console.WriteLine("Invertion:");
            foreach (byte registryByte in result)
            {
                Console.Write(registryByte);
            }
            Console.WriteLine();


            // + 1 - getting two's complementary
            int additionalOne = 1;
            for (int j = 21; j >= 0; j--)
            {
                if (result[j] + additionalOne == 2)
                {
                    result[j] = (byte)((result[j] + additionalOne) % 2);
                }
                else 
                {
                    result[j] = (byte)(result[j] + additionalOne);
                    additionalOne = 0;
                }
            }



            Console.WriteLine("After +1:");
            foreach (byte registryByte in result)
            {
                Console.Write(registryByte);
            }
            Console.WriteLine();

            return result;
        }
    }
}
