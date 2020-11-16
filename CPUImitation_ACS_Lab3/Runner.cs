using System;
using System.Collections.Generic;
using System.Text;

namespace CPUImitation_ACS_Lab3
{
    class Runner
    {
        enum Sign
        {
            PLUS = 0,
            MINUS = 1
        }
        private Sign sign = Sign.PLUS;
        private const int REGISTRY_SIZE = 22;
        private int tacts = 0;
        private  bool isOverflow = false;
        private int commands = 0;
        private static Stack<Register> CPU = new Stack<Register>();
        public void Run() 
        {
            string number = Console.ReadLine();
            string number2 = Console.ReadLine();

            try
            {
                byte[] complementary = ComplementaryConvert(Convert.ToInt32(number));
                Register register = new Register(complementary);
                CPU.Push(register);
                byte[] complementary1 = ComplementaryConvert(Convert.ToInt32(number2));
                register = new Register(complementary1);
                CPU.Push(register);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            byte[] tmp = new byte[REGISTRY_SIZE];
            foreach (Register r in CPU)
            {
                tmp = Add(tmp, r.RegisterArray);
            }


            foreach (byte registryByte in tmp)
            {
                Console.Write(registryByte);
            }
        }

        public byte[] ComplementaryConvert(int number)
        {
            byte[] result = new byte[REGISTRY_SIZE];

            if (number > Math.Pow(2, REGISTRY_SIZE - 1))
            {
                isOverflow = true;
                throw new Exception("Overflow happened my dudes....");
            }

            int sign = Math.Sign(number);
            if (sign == -1)
            {
                this.sign = Sign.MINUS;
                number = -number;
            }
            else 
            {
                this.sign = Sign.PLUS;
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

        public byte[] Add(byte[] number1, byte[] number2)
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
