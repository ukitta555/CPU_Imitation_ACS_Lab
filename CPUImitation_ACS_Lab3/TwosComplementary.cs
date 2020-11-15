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
                result[REGISTRY_SIZE - i - 1] = (byte)(number % 2);
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
            for (int j = 0; j < REGISTRY_SIZE; j++)
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
            byte[] arrayForOne = new byte[REGISTRY_SIZE];
            for (int j = 0; j < REGISTRY_SIZE - 1; j++) 
            {
                arrayForOne[j] = 0;
            }
            arrayForOne[21] = 1;
            result = Add(result, arrayForOne);


            /*
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

            */

            Console.WriteLine("After +1:");
            foreach (byte registryByte in result)
            {
                Console.Write(registryByte);
            }
            Console.WriteLine();

            return result;
        }

        public static byte[] Add(byte[] number1, byte[] number2) 
        {
            byte[] result = new byte[REGISTRY_SIZE];
            int carry = 0;
            for (int j = REGISTRY_SIZE - 1; j >= 0; j--)
            {
                if (number1[j] + number2[j] + carry > 1)
                {
                    result[j] = (byte)((number1[j] + number2[j] + carry) % 2);
                    carry = 1;
                }
                else
                {
                    result[j] = (byte)(number1[j] + number2[j] + carry);
                    carry = 0;
                }
            }
            return result;
        }
    }
}
