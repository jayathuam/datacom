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
        private int _level1;
        private int _level2;
        private POWER_SOURCE _powersource;

        public LoadShading()
        {
            _level1 = 0;
            _level2 = 0;
            _powersource = POWER_SOURCE.value_1;
            Background = null;
        }

        public LoadShading(int level1, int level2, POWER_SOURCE powersource, string header)
        {
            this._level1 = level1;
            this._level2 = level2;
            this._powersource = powersource;
            Background = null;
            Header = header;
        }

        [JsonProperty]
        public int Level1
        {
            get { return _level1; }
            set { _level1 = value; }
        }

        [JsonProperty]
        public int Level2
        {
            get { return _level2; }
            set { _level2 = value; }
        }

        [JsonProperty]
        public POWER_SOURCE PowerSource
        {
            get { return _powersource; }
            set { _powersource =  value; }
        }


    }
}
