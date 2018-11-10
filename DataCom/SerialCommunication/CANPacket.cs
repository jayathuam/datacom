using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.SerialCommunication
{
    public class CANPacket
    {
        public const UInt32 BufferSize = 64;

        public enum Pkt_Type_t
        {
            Pkt_Type_Command = 0,
            Pkt_Type_Response,
            Pkt_Type_Event,
        }


        public enum Device_Types_t
        {
            Device_Type_Mainboard = 0,
            Device_Type_Keypad,
        }


        public enum Notification_t
        {
            Notification_Button = 1,
            Notification_DeviceInfo,
        }

        public enum Pkt_ID_t
        {
            Cmd_LED_Control = 1,
            Cmd_Buzzer_Control,
            Cmd_Request_Dev_Info,
            Cmd_Read_Board_Sensors,
            Cmd_Read_Pos_Output_Configs,
            Cmd_Write_Pos_Output_Configs,
            Cmd_Read_Pos_Out_Current,
            Cmd_Read_Neg_Output_Configs,
            Cmd_Write_Neg_Output_Configs,
            Cmd_Read_Neg_Out_Voltage,
            Cmd_Read_Analog_In_Settings,
            Cmd_Write_Analog_In_Settings,
        }

        public UInt32 Source_address { get; set; }
        public UInt32 Destination_address { get; set; }
        public Pkt_Type_t Packet_type { get; set; }
        public UInt32 Packet_id { get; set; }
        public UInt32 Length { get; set; }
        public byte[] Buffer { get; set; }

        public CANPacket()
        {
            Length = 0;
            Buffer = new byte[BufferSize];
        }
    }
}
