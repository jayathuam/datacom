using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
    public class NegativeOutput : TreeViewItem
    {
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
