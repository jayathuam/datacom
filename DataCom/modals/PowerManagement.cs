using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
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
