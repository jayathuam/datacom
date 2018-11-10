using DataCom.globalDataStore;
using DataCom.modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataCom.modals.enums.Definitions;

namespace DataCom.SerialCommunication
{
    public static class ResultConvertor
    {
        public static void getDeviceInfor(CANPacket e, GlobalData globalData)
        {
            string UUID = null;
            int short_address;

            int packet_idx = 0;
            CANPacket.Device_Types_t deviceType = (CANPacket.Device_Types_t)e.Buffer[packet_idx++];
            int dev_catogory = e.Buffer[packet_idx++];
            for (int i = 0; i < 16; i++)
            {
                UUID += e.Buffer[packet_idx++].ToString("X2");
            }
            short_address = e.Buffer[packet_idx++];


            if (deviceType == CANPacket.Device_Types_t.Device_Type_Keypad)
            {
                KeyPad keypad = new KeyPad();
                keypad.uuid = UUID;
                keypad.shortAddress = short_address;
                keypad.numOfKeys = e.Buffer[packet_idx++];
                keypad.type = (KEYPAD_TYPE)dev_catogory;
                int index = globalData.deviceInfo.keyPads.FindIndex(x => x.uuid == UUID);
                if (index == -1)
                {
                    globalData.deviceInfo.keyPads.Add(keypad);
                }
                else
                {
                    globalData.deviceInfo.keyPads[index] = keypad;
                }
                Console.WriteLine("key pad found");

            }
            else if (deviceType == CANPacket.Device_Types_t.Device_Type_Mainboard)
            {
                ECU ecu = new ECU();
                ecu.uuid = UUID;
                ecu.shortAddress = short_address;
                ecu.numberOFPOsitiveOutputs = e.Buffer[packet_idx++];
                ecu.numberOFNEgativeOutputs = e.Buffer[packet_idx++];
                ecu.numberOFAnalogInput = e.Buffer[packet_idx++];
                ecu.numberOFExternalOutputs = e.Buffer[packet_idx++];
                ecu.type = (ECU_TYPE)dev_catogory;
                int index = globalData.deviceInfo.ecus.FindIndex(x => x.uuid == UUID);
                if (index == -1)
                {
                    globalData.deviceInfo.ecus.Add(ecu);
                }
                else
                {
                    globalData.deviceInfo.ecus[index] = ecu;
                }
                Console.WriteLine("mainboard found");
            }

            Console.Write("done");
        }
    }
}
