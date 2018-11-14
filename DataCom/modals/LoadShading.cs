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
    public class LoadShading: TreeViewItem
    {
        private double _level1;
        private double _level2;        

        [JsonProperty]
        public int PowerSource { get; set; }

        public LoadShading()
        {
            _level1 = 0;
            _level2 = 0;           
            Background = null;
        }

        public LoadShading(int level1, int level2, string header)
        {
            this._level1 = level1;
            this._level2 = level2;            
            Background = null;
            Header = header;
        }

        [JsonProperty]
        public double Level1
        {
            get { return _level1; }
            set { _level1 = value; }
        }

        [JsonProperty]
        public double Level2
        {
            get { return _level2; }
            set { _level2 = value; }
        }        

        [JsonProperty]
        public string CustomLabel
        {
            get { return Header == null ? null : Header.ToString(); }
            set { Header = value; }
        }


    }
}
