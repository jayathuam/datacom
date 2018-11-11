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
            this.DataContext = positiveOutput;
            IndexItems = new List<string>();
            activatorCmb.ItemsSource = Enum.GetValues(typeof(OUTPUT_ACTIVATOR));
            conditionSourceCmb.ItemsSource = Enum.GetValues(typeof(CONDITION_SOURCE));
            sourceConditions = Enumerable.Range(0, 31).ToList();
            voltageSourceCmb.ItemsSource = parent.analogList.Select(o => o.Header.ToString()).ToList();
            voltageSourceCmb.SelectedIndex = positiveOutput.VoltageSource;
            loadShadingCmb.ItemsSource = Enum.GetValues(typeof(LOAD_SHADING));
        }        

        private void conditionSourceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CONDITION_SOURCE item = (CONDITION_SOURCE)conditionSourceCmb.SelectedItem;
            if (item == CONDITION_SOURCE.None)
            {
                sourceIndexCMB.IsEnabled = false;
                sourceOnCmb.IsEnabled = false;
                sourceOffCmb.IsEnabled = false;
            }
            else if (item == CONDITION_SOURCE.ExtInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.externalList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.SourceIndex = 0;
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                try
                {
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOnWhen;                    
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOffWhen;
                    if (sourceOnCmb.SelectedItem == null || sourceOffCmb.SelectedItem ==null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.TurnOnWhen = 0;
                    positiveOutput.TurnOffWhen = 0;
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOffWhen;
                }


            }
            else if (item == CONDITION_SOURCE.AnalogInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.analogList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.SourceIndex = 0;
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                sourceOnCmb.ItemsSource = sourceConditions;
                try
                {
                    sourceOnCmb.SelectedItem = sourceConditions[positiveOutput.TurnOnWhen];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.TurnOnWhen = 0;
                    sourceOnCmb.SelectedItem = sourceConditions[positiveOutput.TurnOnWhen];
                }
                sourceOffCmb.ItemsSource = sourceConditions;
                try
                {
                    sourceOffCmb.SelectedItem = sourceConditions[positiveOutput.TurnOffWhen];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.TurnOffWhen = 0;
                    sourceOffCmb.SelectedItem = sourceConditions[positiveOutput.TurnOffWhen];
                }
            }
            else if (item == CONDITION_SOURCE.CombineInput)
            {
                sourceIndexCMB.IsEnabled = true;
                sourceOnCmb.IsEnabled = true;
                sourceOffCmb.IsEnabled = true;
                IndexItems.Clear();
                IndexItems = parent.combineList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.SourceIndex = 0;
                    sourceIndexCMB.SelectedItem = IndexItems[positiveOutput.SourceIndex];
                }
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                try
                {
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOffWhen;
                    if (sourceOnCmb.SelectedItem == null || sourceOffCmb.SelectedItem == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    positiveOutput.TurnOnWhen = 0;
                    positiveOutput.TurnOffWhen = 0;
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)positiveOutput.TurnOffWhen;
                }
            }
            else if (item == CONDITION_SOURCE.Event)
            {
                sourceIndexCMB.IsEnabled = false;
                IndexItems.Clear();
                sourceOnCmb.IsEnabled = false;
                sourceOffCmb.IsEnabled = false;
            }
        }

        private void sourceIndexCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positiveOutput.SourceIndex = sourceIndexCMB.SelectedIndex;
        }

        private void sourceOnCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positiveOutput.TurnOnWhen = sourceOnCmb.SelectedIndex;
        }

        private void sourceOffCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positiveOutput.TurnOffWhen = sourceOffCmb.SelectedIndex;
        }

        private void voltageSourceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positiveOutput.VoltageSource = voltageSourceCmb.SelectedIndex;
        }
    }
}
