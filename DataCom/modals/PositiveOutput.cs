using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace DataCom.modals
{
    public class PositiveOutput : TreeViewItem
    {
        public PositiveOutput()
        {
            Background = null;
        }
        public PositiveOutput(string header)
        {
            Header = header;
            Background = null;
        }
    }
}
