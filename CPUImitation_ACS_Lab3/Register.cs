using System;
using System.Collections.Generic;
using System.Text;

namespace CPUImitation_ACS_Lab3
{
    class Register
    {
        private const int REGISTER_SIZE = 22;
        public byte[] RegisterArray 
        {
            get;
            set;
        }

        public Register(byte[] registerArray) 
        {
            RegisterArray = registerArray;
        }
    }
}
