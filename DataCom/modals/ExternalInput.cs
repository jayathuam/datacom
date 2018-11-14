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
    public class ExternalInput : TreeViewItem
    {
        [JsonProperty]
        public TARGET_ACTION TargetAction { get; set; }

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public bool EventDetection { get; set; }

        [JsonProperty]
        public EXT_CONFIGURATIONS Configuration { get; set; }

        public ExternalInput(int index)
        {
            this.Index = index;
            SelectedOutputs = new EffectedOutput();
        }

        public ExternalInput()
        {
            Background = null;
        }
        public ExternalInput(string header)
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
