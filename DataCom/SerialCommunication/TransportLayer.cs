using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.SerialCommunication
{
    public class TL_Packet
    {
        public const uint BufferSize = 80;

        public Byte Length { get; set; }
        public byte[] Buffer { get; set; }


        public TL_Packet()
        {
            Length = 0;
            Buffer = new byte[BufferSize];
        }
    }

    // Packet decoder constants
    public enum PacketCollectorStates
    {
        STATE_STX_1 = 0,
        STATE_STX_2,
        STATE_STX_3,
        STATE_STX_LEN,
        STATE_STX_DATA,
        STATE_STX_LRC
    };

    public class TransportLayer
    {        
        private const byte STX_1 = 0x02;
        private const byte STX_2 = 0x02;
        private const byte STX_3 = 0x02;

        //private static SerialPort _serialPort;
        private TL_Packet _rxPacket;
        private PacketCollectorStates _packetCollectorStates;
        private UInt16 _rxBufferWriteIdx;
        private byte _calLRC;
        private BlockingCollection<TL_Packet> _rxQueue;
        private readonly object comportLock = new object();
        //private static Thread readThread;
        private bool rxQueueIsDisposed;

        public TransportLayer()
        {
            _rxQueue = new BlockingCollection<TL_Packet>();
            var obj = new TL_Packet();
            while (_rxQueue.Count > 0)
            {
                _rxQueue.TryTake(out obj);
            }
            _rxPacket = new TL_Packet();
            rxQueueIsDisposed = false;            
        }

        public void setPacketCollectorStaus(PacketCollectorStates packetCollectorStates)
        {
            this._packetCollectorStates = packetCollectorStates;
        }

        //public static bool Connect(string comport)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        _serialPort = new SerialPort();
        //        _rxPacket = new TL_Packet();
        //        _rxQueue = new BlockingCollection<TL_Packet>();
        //        rxQueueIsDisposed = false;

        //        readThread = new Thread(PacketCollector);

        //        _serialPort.PortName = comport;
        //        _serialPort.BaudRate = 115200;
        //        _serialPort.Parity = Parity.None;
        //        _serialPort.DataBits = 8;
        //        _serialPort.StopBits = StopBits.One;
        //        _serialPort.Handshake = Handshake.None;

        //        // Set the read/write timeouts
        //        _serialPort.ReadTimeout = 500;
        //        _serialPort.WriteTimeout = 500;

        //        _serialPort.Open();
        //        _packetCollectorStates = PacketCollectorStates.STATE_STX_1;
        //        _Run = true;

        //        var obj = new TL_Packet();
        //        while (_rxQueue.Count > 0)
        //        {
        //            _rxQueue.TryTake(out obj);
        //        }

        //        readThread.Start();
        //        ret = true;
        //    }
        //    catch (System.UnauthorizedAccessException uex)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ret;
        //}

        //public static String[] GetAvailablePorts()
        //{
        //    return SerialPort.GetPortNames();
        //}

        public byte[] SendPacket(ref TL_Packet packet)
        {
            int idx = 0;
            byte LRC = 0;
            byte[] Buffer = new byte[TL_Packet.BufferSize + 10];

            Buffer[idx++] = STX_1;  // Copy start byte 1
            Buffer[idx++] = STX_2;  // Copy start byte 2
            Buffer[idx++] = STX_3;  // Copy start byte 3
            Buffer[idx++] = packet.Length;    // Copy lenght L
            LRC ^= packet.Length;
            for (int i = 0; i < packet.Length; i++)
            {
                LRC ^= packet.Buffer[i];
                Buffer[idx++] = packet.Buffer[i]; // Copy data
            }
            // Copy LRC
            Buffer[idx++] = LRC;

            byte[] sendBuffer = new byte[idx];
            Array.Copy(Buffer, sendBuffer, idx);
            return sendBuffer;
        }

        public bool GetPacket(ref TL_Packet packet)
        {
            //Console.WriteLine(_rxQueue.Count);
            if (!rxQueueIsDisposed)
            {
                return _rxQueue.TryTake(out packet);
            }
            else
                return false;
        }

        public void close()
        {
            if (_rxQueue != null)
            {
                _rxQueue.Dispose();
                rxQueueIsDisposed = true;
            }
        }

        //public static void Close()
        //{
        //    _Run = false;
        //    if (readThread != null)
        //    {
        //        if (readThread.IsAlive)
        //            readThread.Join();
        //    }
        //    if (_serialPort != null)
        //    {
        //        if (_serialPort.IsOpen)
        //        {
        //            _serialPort.Close();
        //            _serialPort.Dispose();
        //        }
        //    }
        //    if (_rxQueue != null)
        //    {
        //        _rxQueue.Dispose();
        //        rxQueueIsDisposed = true;
        //    }
        //}

        //---------------------- Private methods ------------------------------

        //public static void PacketCollector()
        //{
        //    while (_Run)
        //    {
        //        Thread.Sleep(1);
        //        if (_serialPort.IsOpen)
        //        {
        //            try
        //            {
        //                if (_serialPort.BytesToRead > 0)
        //                {
        //                    int received = _serialPort.ReadByte();
        //                    PacketDecoder((byte)received);
        //                }
        //            }
        //            catch (TimeoutException) { }
        //            catch (UnauthorizedAccessException) { }
        //        }
        //    }
        //}

        // Packet format is:
        // [STX1][STX2][STX3] [LEN] [DATA] [LRC]
        //  8bit  8bit  8bit  16bit N bit   8bit
        // LEN + DATA are used to calculate the LRC
        public void PacketDecoder(byte rxByte)
        {
            switch (_packetCollectorStates)
            {
                case PacketCollectorStates.STATE_STX_1:
                    if (rxByte == STX_1) _packetCollectorStates = PacketCollectorStates.STATE_STX_2;
                    break;

                case PacketCollectorStates.STATE_STX_2:
                    if (rxByte == STX_2) _packetCollectorStates = PacketCollectorStates.STATE_STX_3;
                    else _packetCollectorStates = PacketCollectorStates.STATE_STX_1;
                    break;

                case PacketCollectorStates.STATE_STX_3:
                    if (rxByte == STX_3) _packetCollectorStates = PacketCollectorStates.STATE_STX_LEN;
                    else _packetCollectorStates = PacketCollectorStates.STATE_STX_1;
                    break;

                case PacketCollectorStates.STATE_STX_LEN:
                    _calLRC = 0;
                    _rxBufferWriteIdx = 0;

                    _calLRC ^= rxByte;
                    _rxPacket.Length = rxByte;
                    _packetCollectorStates = PacketCollectorStates.STATE_STX_DATA;

                    break;

                case PacketCollectorStates.STATE_STX_DATA:
                    _calLRC ^= rxByte;
                    _rxPacket.Buffer[_rxBufferWriteIdx++] = rxByte;
                    if (_rxBufferWriteIdx == _rxPacket.Length)
                        _packetCollectorStates = PacketCollectorStates.STATE_STX_LRC;

                    break;

                case PacketCollectorStates.STATE_STX_LRC:
                    if (_calLRC == rxByte)
                    {
                        // Valid Packet received. Push it to queue
                        TL_Packet pct = new TL_Packet();
                        pct = _rxPacket;
                        _rxPacket = new TL_Packet();
                        _rxQueue.Add(pct);
                    }
                    _packetCollectorStates = PacketCollectorStates.STATE_STX_1;
                    break;

                default:
                    _packetCollectorStates = PacketCollectorStates.STATE_STX_1;
                    break;
            }// End of switch

        }//End of PacketDecoder method

    }//End of TransportLayer class
}
