using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AnalogInput : TreeViewItem
    {
        private int minVoltage;
        private int maxVoltage;

        public AnalogInput()
        {
            Background = null;
            minVoltage = 0;
            maxVoltage = 0;
        }
        public AnalogInput(string header)
        {
            Header = header;
            Background = null;
            minVoltage = 0;
            maxVoltage = 0;
        }

        [JsonProperty]
        public int MinVoltage
        {
            get { return minVoltage; }
            set { minVoltage = value; }
        }

        [JsonProperty]
        public int MaxVoltage
        {
            get { return maxVoltage; }
            set { maxVoltage = value; }
        }

        [JsonProperty]
        public string CustomLabel
        {
            get { return Header == null ? null : Header.ToString(); }
            set { Header = value; }
        }
    }
}
