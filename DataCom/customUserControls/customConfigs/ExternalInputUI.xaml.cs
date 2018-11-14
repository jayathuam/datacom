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
    /// Interaction logic for ExternalInputUI.xaml
    /// </summary>
    public partial class ExternalInputUI : UserControl
    {
        private ExternalInput externalInput;
        private ECU ecu;
        private Serial serial;
        public ExternalInputUI(ExternalInput externalInput, ECU ecu, Serial serial)
        {
            InitializeComponent();
            this.externalInput = externalInput;
            this.serial = serial;
            this.ecu = ecu;
            this.DataContext = externalInput;
            configsCmb.ItemsSource = Enum.GetValues(typeof(EXT_CONFIGURATIONS));
            targetActionCmb.ItemsSource = Enum.GetValues(typeof(TARGET_ACTION));
            cutomtree.draw(ecu, externalInput.SelectedOutputs);
        }
    }
}
