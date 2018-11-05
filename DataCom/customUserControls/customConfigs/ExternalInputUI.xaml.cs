using DataCom.modals;
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
        public ExternalInputUI(ExternalInput externalInput)
        {
            InitializeComponent();
            this.externalInput = externalInput;
            configsCmb.ItemsSource = Enum.GetValues(typeof(EXTERNAL_CONFIGS));
            configsCmb.SelectedItem = (EXTERNAL_CONFIGS)1;
        }
    }
}
