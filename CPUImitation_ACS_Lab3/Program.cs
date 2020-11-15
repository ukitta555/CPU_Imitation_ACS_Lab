using System;
using System.Collections.Generic;

namespace CPUImitation_ACS_Lab3
{
    class Program
    {
        enum Sign 
        {
            PLUS,
            MINUS
        }
        
        private int tacts = 0;
        private bool isOverflow = false;
        private int commands = 0;
        private static Stack<Register> CPU = new Stack<Register>();

        public static void Main(string[] args)
        {
            string number = Console.ReadLine();
            try
            {
                byte[] complementary = TwosComplementary.Convert(Convert.ToInt32(number));
                Register register = new Register(complementary);
                CPU.Push(register);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
