using DataCom.globalDataStore;
using SerialPortLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataCom.SerialCommunication
{    
    public class Serial
    {
        private SerialPortInput serialPort;
        private GlobalData globalData;
        private BlockingCollection<Object> responseQueue;
        private Timer responseTimer;

        public event EventHandler<string> Analoginputevent;

        public Serial(GlobalData  globalData)
        {
            this.globalData = globalData;
            serialPort = new SerialPortInput();
            responseQueue = new BlockingCollection<Object>();
            responseTimer = new Timer(100);
            responseTimer.Elapsed += new ElapsedEventHandler(responseTimer_Elapsed);

            serialPort.ConnectionStatusChanged += (obj, args) =>
            {               
                if (args.Connected)
                {
                    globalData.showSuccess("Data Com","Serial Connected.(" + globalData.serialPort + ")");                    
                }
                else
                {
                    globalData.showError("Data Com", "Serial Disconnect.");
                    globalData.serialPort = null;
                }
            };

            serialPort.MessageReceived += (obj, args) =>
            {
                for (int i = 0; i < args.Data.Length; i++)
                {
                   //packet decorderlogic
                }
                //logger.Info("Received data from serial port message: {0}", BitConverter.ToString(args.Data));                
            };
            responseTimer.Start();
        }

        private void responseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Object item in responseQueue.GetConsumingEnumerable())
                {                    
                    //implement here all the events
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void addResponse(Object obj)
        {
            responseQueue.Add(obj);
        }

        public bool connect(string port)
        {
            serialPort.SetPort(port, 115200);
            globalData.serialPort = port;
            return serialPort.Connect();
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
