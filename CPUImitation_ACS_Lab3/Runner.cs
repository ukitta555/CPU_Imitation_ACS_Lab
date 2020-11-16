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
        private static Stack<Registry> CPU = new Stack<Registry>();
        public void Run() 
        {
            string[] lines = System.IO.File.ReadAllLines(@"../../../operations.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Console.WriteLine("___________________________________");
                Parse(line);
            }
        }
        public void PrintStuff() 
        {
            Console.WriteLine("Current command count: {0}", commands);
            Console.WriteLine("Sign: {0}", sign);
            Console.WriteLine("Overflow: {0}", isOverflow);
            Console.WriteLine("Current tact: {0}", ++tacts);
            if (tacts == 2) tacts = 0;
            int regIndex = 0;
            if (CPU.Count == 0)
            {
                Console.WriteLine("CPU stack is empty!");
            }
            else
            {
                foreach (Registry reg in CPU)
                {
                    regIndex++;
                    Console.Write("Register {0}: ", regIndex);
                    foreach (byte bit in reg.RegisterArray)
                    {
                        Console.Write(bit);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("___________________________________");
        }
        public void Parse (string s)
        {
            string[] tokens = s.Split(' ');
            if (tokens[0] == "push")
            {
                PrintStuff();
                try
                {
                    byte[] complementary = ComplementaryConvert(Convert.ToInt32(tokens[1]));
                    Registry registry = new Registry(complementary);
                    CPU.Push(registry);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("___________________________________");
                    throw new Exception();
                }
                PrintStuff();
                commands++;
            }
            else if (tokens[0] == "add")
            {

                PrintStuff();
                try 
                {

                    if (CPU.Count >= 2)
                    {
                        byte[] number1 = CPU.Pop().RegisterArray;
                        byte[] number2 = CPU.Pop().RegisterArray;
                        byte[] result = Add(number1, number2);

                        //create new registry for the result;
                        Registry registry = new Registry(result);
                        CPU.Push(registry);

                        PrintStuff();

                        commands++;
                    }
                    else throw new Exception("Not enough registries on stack!");
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("___________________________________");
                    if (e.Message == "Not enough registries on stack!") throw new Exception();
                }
            }
            else 
            {
                Console.WriteLine("Wrong commands were passed to processor. Terminating....");
                throw new Exception();
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

            /*
            Console.WriteLine("In binary representation:");
            i = 0;
            foreach (byte registryByte in result)
            {
                Console.Write(registryByte);
            }
            Console.WriteLine();
            */

            if (this.sign == Sign.MINUS) 
            {
            
                // inverting number representation
                for (int j = 0; j < REGISTRY_SIZE; j++)
                {
                    result[j] = (result[j] == 0)
                                ? (byte)1
                                : (byte)0;

                }

                /*
                Console.WriteLine("Invertion:");
                foreach (byte registryByte in result)
                {
                    Console.Write(registryByte);
                }
                Console.WriteLine();
                */

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
                /*
                Console.WriteLine("After +1:");
                foreach (byte registryByte in result)
                {
                    Console.Write(registryByte);
                }
                Console.WriteLine();
                */
            }
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

            int correspondingNumber = 0;
            foreach (byte bit in result) 
            {
                
            }
            
            // change sign if needed
            if (result[0] == 1)
            {
                sign = Sign.MINUS;
            }
            else
            {
                sign = Sign.PLUS;
            }

            return result;
        }
    }
}
