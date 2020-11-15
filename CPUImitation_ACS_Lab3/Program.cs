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
        Stack<Register> CPU = new Stack<Register>();

        static void Main(string[] args)
        {
            string number = Console.ReadLine();
            try
            {
                foreach (byte tmp in TwosComplementary.Convert(Convert.ToInt32(number)))
                {
                    Console.Write(tmp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
