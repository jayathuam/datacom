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
    public class Key: TreeViewItem
    {
        [JsonProperty]
        public TARGET_ACTION TargetAction { get; set; }

        [JsonProperty]
        public List<EffectedOutput> SelectedOutputs { get; set; }

        [JsonProperty]
        public int Index { get; set; }

        public Key(int index)
        {
            this.Index = index;
            SelectedOutputs = new List<EffectedOutput>();
        }

        public Key()
        {
            Background = null;
        }
        public Key(string header)
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
