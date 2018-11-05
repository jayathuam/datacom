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
            type_1 = 1,
            type_2,
            type_3,
            type_4
        }

        public enum KEYPAD_TYPE
        {
            type_1 = 1,
            type_2,
            type_3            
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
