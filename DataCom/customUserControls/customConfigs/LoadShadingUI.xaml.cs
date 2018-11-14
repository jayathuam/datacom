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
    /// Interaction logic for LoadShading.xaml
    /// </summary>
    public partial class LoadShadingUI : UserControl
    {
        private LoadShading loadShading;
        private ECU parent;
        private CommandHandler cmdHandler;
        private Serial serial;

        public LoadShadingUI(LoadShading loadShading, ECU parent, CommandHandler cmdHandler, Serial serial)
        {
            InitializeComponent();
            this.loadShading = loadShading;
            this.serial = serial;
            this.cmdHandler = cmdHandler;
            this.parent = parent;
            this.DataContext = loadShading;            
            powerCmb.ItemsSource = parent.analogList.Select(o => o.Header.ToString()).ToList();
            powerCmb.SelectedIndex = loadShading.PowerSource;
        }

        private void powerCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadShading.PowerSource = powerCmb.SelectedIndex;
        }
    }
}
