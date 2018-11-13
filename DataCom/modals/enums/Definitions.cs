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

        public enum OUTPUT_ACTIVATOR
        {
            Always_Off = 0,
            Always_On,
            Button,
            Ext_Input,
            Analog_Input,
            Combine_Input,
            Events
        }

        public enum CONDITION_SOURCE
        {
            None = 0,
            ExtInput,
            AnalogInput,
            CombineInput,
            Event
        }

        public enum SOURCE_CONDITIONS
        {
            Low = 0,
            High
        }

        public enum LOAD_SHADING
        {
            Loadeshading_Disabled = 0,
            Loadshading_Level_1,
            Loadshading_Level_2
        }

        public enum TARGET_ACTION
        {
            Off = 0,
            On,
            Toggle,
            Push_On_Release_On,
            Push_Off_Release_On
        }
    }
}
