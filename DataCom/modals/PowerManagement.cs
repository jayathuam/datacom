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
    public class PowerManagement : TreeViewItem
    {
        public PowerManagement()
        {
            Background = null;
        }
        public PowerManagement(string header)
        {
            Header = header;
            Background = null;
        }
    }
}
