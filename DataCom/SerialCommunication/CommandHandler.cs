using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.SerialCommunication
{
    public class CommandHandler
    {
        private CANInterface canInterface;
        private TransportLayer transportLayer;
        private Serial serial;
        private Logger logger;

        public CommandHandler(CANInterface canInterface, TransportLayer transportLayer, Serial serial)
        {
            this.canInterface = canInterface;
            this.transportLayer = transportLayer;
            this.serial = serial;
            logger = LogManager.GetCurrentClassLogger();
        }
        public void cmdRequestDevInfo()
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Request_Dev_Info;
            packet.Source_address = 0;
            packet.Destination_address = 0xff;
            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            serial.sendMessage(transportLayer.SendPacket(ref tl_packet));
            logger.Info("Request device Info: ", tl_packet);
        }

        public void cmdReadBoardSensors(uint shortAddress)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Read_Board_Sensors;
            packet.Source_address = 0;
            packet.Destination_address = shortAddress;
            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            transportLayer.SendPacket(ref tl_packet);
            logger.Info("Read board sensors", tl_packet);
        }

        public void cmdReadAnalogInputs(uint seleectedShortAddess, int analogInputIndx)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Read_Analog_In_Settings;
            packet.Source_address = 0;
            packet.Destination_address = seleectedShortAddess;
            packet.Length = 1;
            packet.Buffer[0] = (byte)analogInputIndx;
            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            transportLayer.SendPacket(ref tl_packet);
            logger.Info("Read analog inputs", tl_packet);
        }

        public void cmdWriteAnalogInputs(uint seleectedShortAddess, int analogInputIndx, int minVoltage, int maxVoltage)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Write_Analog_In_Settings;
            packet.Source_address = 0;
            packet.Destination_address = seleectedShortAddess;
            packet.Length = 0;
            packet.Buffer[packet.Length++] = (byte)analogInputIndx;
            packet.Buffer[packet.Length++] = (byte)((minVoltage >> 8) & 0xFF);
            packet.Buffer[packet.Length++] = (byte)(minVoltage & 0xFF);
            packet.Buffer[packet.Length++] = (byte)((maxVoltage >> 8) & 0xFF);
            packet.Buffer[packet.Length++] = (byte)(maxVoltage & 0xFF);

            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            transportLayer.SendPacket(ref tl_packet);
            logger.Info("write analog inputs", tl_packet);
        }

        public void cmdReadNegativeOutputConfigs(uint seleectedShortAddess, int negativeOutIdx)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Read_Neg_Output_Configs;
            packet.Source_address = 0;
            packet.Destination_address = seleectedShortAddess;
            packet.Length = 0;
            packet.Buffer[packet.Length++] = (byte)negativeOutIdx;

            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            transportLayer.SendPacket(ref tl_packet);
            logger.Info("read negative output configs", tl_packet);
        }

        public void cmdWriteNegativeOutputConfigs(uint seleectedShortAddess, int negativeOutIdx,
                                                    uint activaotr, uint keypadId, uint buttonId,
                                                    uint action_for_activator, uint under_voltage,
                                                    uint UV_frequency, uint UV_blink_count, int UVbrightness,
                                                    uint poweSource
                                                 )
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Write_Neg_Output_Configs;
            packet.Source_address = 0;
            packet.Destination_address = seleectedShortAddess;
            packet.Length = 0;
            packet.Buffer[packet.Length++] = (byte)negativeOutIdx;
            packet.Buffer[packet.Length++] = (byte)activaotr;
            packet.Buffer[packet.Length++] = (byte)keypadId;
            packet.Buffer[packet.Length++] = (byte)buttonId;
            packet.Buffer[packet.Length++] = (byte)action_for_activator;
            packet.Buffer[packet.Length++] = (byte)(under_voltage >> 8);
            packet.Buffer[packet.Length++] = (byte)(under_voltage & 0xFF);
            packet.Buffer[packet.Length++] = (byte)UV_frequency;
            packet.Buffer[packet.Length++] = (byte)UV_blink_count;
            packet.Buffer[packet.Length++] = (byte)UVbrightness;
            packet.Buffer[packet.Length++] = (byte)poweSource;


            TL_Packet tl_packet = new TL_Packet();
            canInterface.EncodePacket(ref packet, ref tl_packet);
            transportLayer.SendPacket(ref tl_packet);
            logger.Info("write negative output configs", tl_packet);
        }

        public void cmdReadPosOutCurrent(uint seleectedShortAddess, int analogInputIndx)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Read_Pos_Out_Current;
            //kasun complete the rest
        }

        public void cmdReadPosOutputConfigs(uint seleectedShortAddess, int negativeOutIdx)
        {
            CANPacket packet = new CANPacket();
            packet.Packet_type = CANPacket.Pkt_Type_t.Pkt_Type_Command;
            packet.Packet_id = (UInt32)CANPacket.Pkt_ID_t.Cmd_Read_Pos_Output_Configs;
            //kasun complete the rest
        }

        public void cmdWritePosOutputConfigs(uint seleectedShortAddess, int negativeOutIdx)
        {
            //kasun to complete
        }


    }
}
