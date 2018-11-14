using DataCom.customUserControls.customTree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static DataCom.modals.enums.Definitions;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AnalogInput : TreeViewItem
    {
        private double minVoltage;
        private double maxVoltage;

        [JsonProperty]
        public TARGET_ACTION TargetAction { get; set; }

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public bool EventDetection { get; set; }

        public AnalogInput(int index)
        {
            this.Index = index;
            SelectedOutputs = new EffectedOutput();
        }

        public AnalogInput()
        {
            Background = null;
            minVoltage = 0;
            maxVoltage = 0;
            SelectedOutputs = new EffectedOutput();            
        }
        public AnalogInput(string header)
        {
            Header = header;
            Background = null;
            minVoltage = 0;
            maxVoltage = 0;
            SelectedOutputs = new EffectedOutput();            
        }

        [JsonProperty]
        public double MinVoltage
        {
            get { return minVoltage; }
            set { minVoltage = value; }
        }

        [JsonProperty]
        public double MaxVoltage
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

        [JsonProperty]
        public EffectedOutput SelectedOutputs { get; set; }
    }
}
