using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
    public class ExternalInput : TreeViewItem
    {
        public ExternalInput()
        {
            Background = null;
        }
        public ExternalInput(string header)
        {
            Header = header;
            Background = null;
        }
    }
}
