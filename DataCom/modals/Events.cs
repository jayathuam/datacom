using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
    public class Events : TreeViewItem
    {
        public Events()
        {
            Background = null;
        }
        public Events(string header)
        {
            Header = header;
            Background = null;
        }
    }
}
