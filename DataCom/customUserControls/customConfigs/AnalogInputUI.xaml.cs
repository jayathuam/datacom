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
    /// Interaction logic for AnalogInputUI.xaml
    /// </summary>
    public partial class AnalogInputUI : UserControl
    {
        private AnalogInput analogInput;
        private Serial serial;
        private ECU ecu;
        public AnalogInputUI(AnalogInput analogInput, ECU ecu, Serial serial)
        {
            InitializeComponent();
            this.analogInput = analogInput;
            this.ecu = ecu;
            this.DataContext = analogInput;
            this.serial = serial;
            targetActionCmb.ItemsSource = Enum.GetValues(typeof(TARGET_ACTION));
            serial.Analoginputevent += event_recived;
            cutomtree.draw(ecu, analogInput.SelectedOutputs);
        }

        private void event_recived(object sender, string e)
        {
            throw new NotImplementedException();
        }
    }
}
