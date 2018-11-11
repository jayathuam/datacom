using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static DataCom.modals.enums.Definitions;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PositiveOutput : TreeViewItem
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
        public int MinimumCurrent { get; set; }
        [JsonProperty]
        public int MaxCurrent { get; set; }

        [JsonProperty]
        public int LEDUnderCurrFreq { get; set; }
        [JsonProperty]
        public int LEDUnderCurrBlink { get; set; }
        [JsonProperty]
        public int LEDUnderCurrBright { get; set; }

        [JsonProperty]
        public int LEDOverCurrFreq { get; set; }
        [JsonProperty]
        public int LEDOverCurrBlink { get; set; }
        [JsonProperty]
        public int LEDOverCurrBright { get; set; }

        [JsonProperty]
        public LOAD_SHADING LoadShading { get; set; }
        [JsonProperty]
        public int StartupDelay { get; set; }

        public PositiveOutput(int index)
        {
            this.Index = index;
        }

        public PositiveOutput()
        {
            Background = null;
        }
        public PositiveOutput(string header)
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
