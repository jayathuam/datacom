using DataCom.globalDataStore;
using SerialPortLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.SerialCommunication
{    
    public class Serial
    {
        private SerialPortInput serialPort;
        private GlobalData globalData;
        public Serial(GlobalData  globalData)
        {
            this.globalData = globalData;
            serialPort = new SerialPortInput();
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
