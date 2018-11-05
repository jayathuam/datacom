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
    /// Interaction logic for LoadShading.xaml
    /// </summary>
    public partial class LoadShadingUI : UserControl
    {
        private LoadShading loadShading;
        public LoadShadingUI(LoadShading loadShading)
        {
            InitializeComponent();
            this.loadShading = loadShading;
            level1Slider.Value = loadShading.Level1;
            level2Slider.Value = loadShading.Level2;
            powerCmb.ItemsSource = Enum.GetValues(typeof(POWER_SOURCE));
            powerCmb.SelectedItem = (POWER_SOURCE) 1;
        }
    }
}
