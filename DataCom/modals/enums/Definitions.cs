using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.modals.enums
{
    public class Definitions
    {
        public enum ECU_TYPE
        {
            MainboardCatogory_System_1 = 0,
            MainboardCatogory_System_2,
            MainboardCatogory_System_3,
            MainboardCatogory_System_4,
        }

        public enum KEYPAD_TYPE
        {
            KeypadCatogory_Keypad_10_plus_2 = 0,
            KeypadCatogory_Keypad_8_plus_2,
            KeypadCatogory_Keypad_5_plus_1,
        }

        public enum POWER_SOURCE
        {
            value_1 = 1,
            value_2,
            value_3
        }

        public enum EXTERNAL_CONFIGS
        {
            Positive_Input = 1,
            Negative_Input            
        }
    }
}
