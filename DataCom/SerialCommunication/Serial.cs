using DataCom.globalDataStore;
using SerialPortLib;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Timers;

namespace DataCom.SerialCommunication
{
    public class Serial
    {
        private SerialPortInput serialPort;
        private GlobalData globalData;
        private BlockingCollection<CANPacket> responseQueue;
        private BlockingCollection<CANPacket> eventQueue;
        private System.Timers.Timer responseTimer;
        private System.Timers.Timer eventTimer;
        private TransportLayer transportLayer;

        public event EventHandler<string> Analoginputevent;
        public event EventHandler<bool> deviceInfoEvent;

        public Serial(GlobalData globalData, TransportLayer transportLayer)
        {
            this.globalData = globalData;
            this.transportLayer = transportLayer;
            serialPort = new SerialPortInput();
            responseQueue = new BlockingCollection<CANPacket>();
            eventQueue = new BlockingCollection<CANPacket>();
            responseTimer = new System.Timers.Timer(100);
            eventTimer = new System.Timers.Timer(100);
            responseTimer.Elapsed += new ElapsedEventHandler(responseTimer_Elapsed);
            eventTimer.Elapsed += new ElapsedEventHandler(eventTimer_Elapsed);
            
            eventTimer.Start();
            responseTimer.Start();

            serialPort.ConnectionStatusChanged += (obj, args) =>
            {
                if (args.Connected)
                {
                    transportLayer.setPacketCollectorStaus(PacketCollectorStates.STATE_STX_1);
                    globalData.showSuccess("Data Com", "Serial Connected.(" + globalData.serialPort + ")");
                }
                else
                {
                    transportLayer.close();
                    globalData.showError("Data Com", "Serial Disconnect.");
                    globalData.serialPort = null;
                }
            };

            serialPort.MessageReceived += (obj, args) =>
            {
                for (int i = 0; i < args.Data.Length; i++)
                {
                    transportLayer.PacketDecoder(args.Data[i]);
                }
                //logger.Info("Received data from serial port message: {0}", BitConverter.ToString(args.Data));                
            };
            responseTimer.Start();
        }

        private void eventTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (CANPacket item in eventQueue.GetConsumingEnumerable())
                {
                    UInt16 packetId = (UInt16)item.Packet_id;
                    if (packetId == (UInt16)CANPacket.Notification_t.Notification_DeviceInfo)
                    {
                        Thread thread = new Thread(() =>
                        {
                            ResultConvertor.getDeviceInfor(item, globalData);
                            deviceInfoEvent(this, true);
                        });
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();                        
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void responseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (CANPacket item in responseQueue.GetConsumingEnumerable())
                {
                    Console.WriteLine("response found");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void addResponse(ref CANPacket obj)
        {
            responseQueue.Add(obj);
        }


        public void addEvent(ref CANPacket obj)
        {
            eventQueue.Add(obj);
        }
        public bool connect()
        {
            serialPort.SetPort(globalData.serialPort, 115200);
            return serialPort.Connect();
        }

        public bool isConnected()
        {
            return serialPort.IsConnected;
        }

        public void disconnect()
        {
            serialPort.Disconnect();
        }

        public bool sendMessage(byte[] message)
        {
            return serialPort.SendMessage(message);
        }
    }
}
