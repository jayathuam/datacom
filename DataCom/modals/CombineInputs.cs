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
    public class CombineInputs : TreeViewItem
    {
        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public INPUT_SOURCE InputSource1 { get; set; }
        [JsonProperty]
        public int InputIndex1 { get; set; }
        [JsonProperty]
        public bool Inverted1 { get; set; }
        [JsonProperty]
        public OPERATIONS Operation { get; set; }
        [JsonProperty]
        public INPUT_SOURCE InputSource2 { get; set; }
        [JsonProperty]
        public int InputIndex2 { get; set; }
        [JsonProperty]
        public bool Inverted2 { get; set; }
        [JsonProperty]
        public TARGET_ACTION TargetAction { get; set; }
        [JsonProperty]
        public bool EventDetection { get; set; }

        public CombineInputs(int index)
        {
            this.Index = index;
            SelectedOutputs = new EffectedOutput();
        }

        public CombineInputs()
        {
            Background = null;
        }
        public CombineInputs(string header)
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

        [JsonProperty]
        public EffectedOutput SelectedOutputs { get; set; }
    }
}
