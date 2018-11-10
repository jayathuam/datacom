using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCom.SerialCommunication
{
    public class CANInterface
    {
        private TransportLayer transportLayer;
        private Thread threadSerialPacketReader;
        //private QueueProvider queProvider;
        private Serial serial;
        public enum CANError
        {
            CAN_ERROR_NONE = 0,
            CAN_ERROR_LENGTH,
            CAN_ERROR_NULL,
        }

        public CANInterface(TransportLayer transportLayer, Serial serial)
        {
            this.transportLayer = transportLayer;
            //this.queProvider = queProvider;
            this.serial = serial;
            threadSerialPacketReader = new Thread(ReadSerialPackets);
            threadSerialPacketReader.Start();
        }

        public void stopPacketReader()
        {
            try
            {
                if (threadSerialPacketReader.IsAlive)
                {
                    threadSerialPacketReader.Abort();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public CANError DecodePacket(byte[] buff, ref CANPacket packet)
        {
            CANError errorcode = CANError.CAN_ERROR_NONE;
            UInt32 extendedID = ((UInt32)buff[0] << 24) + ((UInt32)buff[1] << 16) + ((UInt32)buff[2] << 8) + (UInt32)buff[3];

            packet.Packet_type = (CANPacket.Pkt_Type_t)((extendedID >> 20) & 0x03);
            packet.Packet_id = ((extendedID >> 16) & 0x0F);
            packet.Source_address = (extendedID >> 8) & 0xFF;
            packet.Destination_address = extendedID & 0xFF;

            int idx = 4;
            packet.Length = buff[idx++];

            if (packet.Length > CANPacket.BufferSize)
            {
                return CANError.CAN_ERROR_LENGTH;
            }

            for (int i = 0; i < packet.Length; i++)
            {
                packet.Buffer[i] = buff[idx++];
            }

            return errorcode;
        }

        internal void EncodePacket(ref CANPacket can_packet, ref TL_Packet serialPacket)
        {
            UInt32 extId = (can_packet.Destination_address & 0xFF) |
                                        ((can_packet.Source_address & 0xFF) << 8) |
                                        (((UInt32)can_packet.Packet_id & 0xF) << 16) |
                                        ((UInt32)can_packet.Packet_type & 0x03) << 20;

            UInt16 buffIdx = 0;
            serialPacket.Buffer[buffIdx++] = (byte)(extId >> 24);
            serialPacket.Buffer[buffIdx++] = (byte)(extId >> 16);
            serialPacket.Buffer[buffIdx++] = (byte)(extId >> 8);
            serialPacket.Buffer[buffIdx++] = (byte)extId;
            serialPacket.Buffer[buffIdx++] = (byte)can_packet.Length;
            for (int i = 0; i < can_packet.Length; i++)
            {
                serialPacket.Buffer[buffIdx++] = can_packet.Buffer[i];
            }
            serialPacket.Length = (byte)buffIdx;
        }

        private void ReadSerialPackets()
        {
            while (true) // handle this
            {
                // Read packets form Serial port and put them in to relavant queues
                TL_Packet packet = new TL_Packet();
                try
                {

                    if (transportLayer.GetPacket(ref packet))
                    {
                        // Serial packet is available. Pass it to CAN papcket processor for decording.                    
                        CANPacket can_packet = new CANPacket();
                        if (DecodePacket(packet.Buffer, ref can_packet) == CANError.CAN_ERROR_NONE)
                        {
                            switch (can_packet.Packet_type)
                            {
                                case CANPacket.Pkt_Type_t.Pkt_Type_Command:
                                    Console.WriteLine("command");
                                    //InsertCommand(ref can_packet);
                                    break;

                                case CANPacket.Pkt_Type_t.Pkt_Type_Response:
                                    Console.WriteLine("response");
                                    serial.addResponse(ref can_packet);
                                    break;

                                case CANPacket.Pkt_Type_t.Pkt_Type_Event:
                                    Console.WriteLine("event");
                                    serial.addEvent(ref can_packet);
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                //if (!threadSerialPacketReaderRun) break;
                Thread.Sleep(200);
            }
        }
    }
}
