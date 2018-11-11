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
    /// Interaction logic for PositiveOutputUI.xaml
    /// </summary>
    public partial class PositiveOutputUI : UserControl
    {
        private PositiveOutput positiveOutput;
        private ECU parent;        
        public List<string> IndexItems { get; set; }
        private List<int> sourceConditions;
        public PositiveOutputUI(PositiveOutput positiveOutput, ECU parent)
        {
            InitializeComponent();
            this.positiveOutput = positiveOutput;
            this.parent = parent;
            IndexItems = new List<string>();
            activatorCmb.ItemsSource = Enum.GetValues(typeof(OUTPUT_ACTIVATOR));
            conditionSourceCmb.ItemsSource = Enum.GetValues(typeof(CONDITION_SOURCE));
            sourceConditions = Enumerable.Range(0, 31).ToList();
        }        

        private void conditionSourceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CONDITION_SOURCE item = (CONDITION_SOURCE)conditionSourceCmb.SelectedItem;
            if(item == CONDITION_SOURCE.None)
            {
                sourceIndexCMB.IsEnabled = false;
                sourceOnCmb.IsEnabled = false;
                sourceOffCmb.IsEnabled = false;
            }
            else if(item == CONDITION_SOURCE.ExtInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.externalList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
            }
            else if (item == CONDITION_SOURCE.AnalogInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.analogList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                sourceOnCmb.ItemsSource = sourceConditions;
                sourceOffCmb.ItemsSource = sourceConditions;
            }
            else if (item == CONDITION_SOURCE.CombineInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.combineList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
            }
            else if (item == CONDITION_SOURCE.Event)
            {
                sourceIndexCMB.IsEnabled = false;
                IndexItems.Clear();
                sourceOnCmb.IsEnabled = false;
                sourceOffCmb.IsEnabled = false;
            }
        }
    }
}
