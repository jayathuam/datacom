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
    public class NegativeOutput : TreeViewItem
    {
        [JsonProperty]
        public int Index { get; set; }
        [JsonProperty]
        public OUTPUT_ACTIVATOR Activator { get; set; }
        [JsonProperty]
        public int ActiveTime { get; set; }
        [JsonProperty]
        public CONDITION_SOURCE ConditionSource { get; set; }
        [JsonProperty]
        public int SourceIndex { get; set; }
        [JsonProperty]
        public int TurnOnWhen { get; set; }
        [JsonProperty]
        public int TurnOffWhen { get; set; }
        [JsonProperty]
        public int VoltageSource { get; set; }
        [JsonProperty]
        public double MinVoltage { get; set; }
        [JsonProperty]
        public double MaxVoltage { get; set; }
        [JsonProperty]
        public int LEDUnderVoltageFreq { get; set; }
        [JsonProperty]
        public int LEDUnderVoltageBlink { get; set; }
        [JsonProperty]
        public int LEDUnderVoltageBright { get; set; }

        [JsonProperty]
        public int LEDOverVoltageFreq { get; set; }
        [JsonProperty]
        public int LEDOverVoltageBlink { get; set; }
        [JsonProperty]
        public int LEDOverVoltageBright { get; set; }        

        [JsonProperty]
        public int LEDOutputErrFreq { get; set; }
        [JsonProperty]
        public int LEDOutputErrBlink { get; set; }
        [JsonProperty]
        public int LEDOutputErrBright { get; set; }

        [JsonProperty]
        public bool VoltageMonitor { get; set; }        

        [JsonProperty]
        public LOAD_SHADING LoadShading { get; set; }
        [JsonProperty]
        public int StartupDelay { get; set; }

        public NegativeOutput(int index)
        {
            this.Index = index;
        }

        public NegativeOutput()
        {
            Background = null;
        }
        public NegativeOutput(string header)
        {
            Header = header;
            Background = null;
        }

        [JsonProperty]
        public string CustomLabel
        {
            get { return Header == null ? null : Header.ToString(); }
            set { Header = value; }
        }

    }
}
