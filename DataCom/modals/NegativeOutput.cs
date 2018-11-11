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
    public class NegativeOutput : TreeViewItem
    {
        public int ShortAddress { get; set; }
        public NegativeOutput()
        {
            Background = null;           
        }
        public NegativeOutput(string header)
        {
            Header = header;
            Background = null;            
        }

        
    }
}
