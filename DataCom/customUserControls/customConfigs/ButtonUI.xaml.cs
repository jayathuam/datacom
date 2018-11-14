using DataCom.modals;
using DataCom.SerialCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DataCom.modals.enums.Definitions;

namespace DataCom.customUserControls.customConfigs
{
    /// <summary>
    /// Interaction logic for ButtonUI.xaml
    /// </summary>
    public partial class ButtonUI : UserControl
    {
        private DataCom.modals.Key key;
        private List<ECU> ecus;
        private Serial serial;
        public ButtonUI(DataCom.modals.Key key, List<ECU> ecus, Serial serial)
        {
            this.key = key;
            this.ecus = ecus;
            this.serial = serial;
            InitializeComponent();
            this.DataContext = key;
            targetActionCmb.ItemsSource = Enum.GetValues(typeof(TARGET_ACTION));
            cutomtree.draw(ecus, key.SelectedOutputs);
        }
    }
}
