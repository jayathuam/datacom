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
    /// Interaction logic for NegativeOutputUI.xaml
    /// </summary>
    public partial class NegativeOutputUI : UserControl
    {
        private NegativeOutput negativeOutput;
        private ECU parent;
        public List<string> IndexItems { get; set; }
        private CommandHandler cmdHandler;
        private Serial serial;
        public NegativeOutputUI(NegativeOutput negativeOutput, ECU parent, CommandHandler cmdHandler, Serial serial)
        {
            InitializeComponent();
            this.negativeOutput = negativeOutput;
            this.parent = parent;
            this.cmdHandler = cmdHandler;
            this.serial = serial;            
            this.DataContext = negativeOutput;
            IndexItems = new List<string>();
            activatorCmb.ItemsSource = Enum.GetValues(typeof(OUTPUT_ACTIVATOR));
            conditionSourceCmb.ItemsSource = Enum.GetValues(typeof(CONDITION_SOURCE));
            voltageSourceCmb.ItemsSource = parent.analogList.Select(o => o.Header.ToString()).ToList();
            voltageSourceCmb.SelectedIndex = negativeOutput.VoltageSource;
            loadShadingCmb.ItemsSource = Enum.GetValues(typeof(LOAD_SHADING));
        }

        private void conditionSourceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CONDITION_SOURCE item = (CONDITION_SOURCE)conditionSourceCmb.SelectedItem;
            if (item == CONDITION_SOURCE.None)
            {
                sourceIndexCMB.IsEnabled = false;
                disableAllSourceElements();
            }
            else if (item == CONDITION_SOURCE.ExtInput)
            {
                sourceIndexCMB.IsEnabled = true;
                dropdownShow();
                IndexItems.Clear();
                IndexItems = parent.externalList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    negativeOutput.SourceIndex = 0;
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                }
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                try
                {
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOffWhen;
                    if (sourceOnCmb.SelectedItem == null || sourceOffCmb.SelectedItem == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    negativeOutput.TurnOnWhen = 0;
                    negativeOutput.TurnOffWhen = 0;
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOffWhen;
                }


            }
            else if (item == CONDITION_SOURCE.AnalogInput)
            {
                sourceIndexCMB.IsEnabled = true;
                trackbarShow();
                IndexItems.Clear();
                IndexItems = parent.analogList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                    turnOffSlider.Value = negativeOutput.TurnOffWhen / 1000.0;
                    turnOnSlider.Value = negativeOutput.TurnOnWhen / 1000.0;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    negativeOutput.SourceIndex = 0;
                    negativeOutput.TurnOffWhen = 0;
                    negativeOutput.TurnOnWhen = 0;
                    turnOffSlider.Value = negativeOutput.TurnOffWhen;
                    turnOnSlider.Value = negativeOutput.TurnOnWhen;
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                }
            }
            else if (item == CONDITION_SOURCE.CombineInput)
            {
                sourceIndexCMB.IsEnabled = true;
                dropdownShow();
                IndexItems.Clear();
                IndexItems = parent.combineList.Select(o => o.Header.ToString()).ToList();
                sourceIndexCMB.ItemsSource = IndexItems;
                try
                {
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    negativeOutput.SourceIndex = 0;
                    sourceIndexCMB.SelectedItem = IndexItems[negativeOutput.SourceIndex];
                }
                sourceOnCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                sourceOffCmb.ItemsSource = Enum.GetValues(typeof(SOURCE_CONDITIONS));
                try
                {
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOffWhen;
                    if (sourceOnCmb.SelectedItem == null || sourceOffCmb.SelectedItem == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    negativeOutput.TurnOnWhen = 0;
                    negativeOutput.TurnOffWhen = 0;
                    sourceOnCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOnWhen;
                    sourceOffCmb.SelectedItem = (SOURCE_CONDITIONS)negativeOutput.TurnOffWhen;
                }
            }
            else if (item == CONDITION_SOURCE.Event)
            {
                sourceIndexCMB.IsEnabled = false;
                IndexItems.Clear();
                disableAllSourceElements();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cmdHandler.cmdWriteNegativeOutputConfigs((uint)parent.shortAddress, negativeOutput);
        }

        private void sourceIndexCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            negativeOutput.SourceIndex = sourceIndexCMB.SelectedIndex;
        }

        private void sourceOnCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            negativeOutput.TurnOnWhen = sourceOnCmb.SelectedIndex;
        }

        private void sourceOffCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            negativeOutput.TurnOffWhen = sourceOffCmb.SelectedIndex;
        }

        private void voltageSourceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            negativeOutput.VoltageSource = voltageSourceCmb.SelectedIndex;
        }       

        private void disableAllSourceElements()
        {
            turnOnSlider.IsEnabled = false;
            turnOffSlider.IsEnabled = false;
            turnOnLbl.IsEnabled = false;
            trunOffLbl.IsEnabled = false;
            sourceOnCmb.IsEnabled = false;
            sourceOffCmb.IsEnabled = false;
        }

        private void trackbarShow()
        {
            turnOnSlider.IsEnabled = true;
            turnOnSlider.Visibility = Visibility.Visible;
            turnOffSlider.IsEnabled = true;
            turnOffSlider.Visibility = Visibility.Visible;
            turnOnLbl.IsEnabled = true;
            turnOnLbl.Visibility = Visibility.Visible;
            trunOffLbl.IsEnabled = true;
            trunOffLbl.Visibility = Visibility.Visible;

            sourceOnCmb.IsEnabled = false;
            sourceOnCmb.Visibility = Visibility.Hidden;
            sourceOffCmb.IsEnabled = false;
            sourceOffCmb.Visibility = Visibility.Hidden;
        }

        private void dropdownShow()
        {
            turnOnSlider.IsEnabled = false;
            turnOnSlider.Visibility = Visibility.Hidden;
            turnOffSlider.IsEnabled = false;
            turnOffSlider.Visibility = Visibility.Hidden;
            turnOnLbl.IsEnabled = false;
            turnOnLbl.Visibility = Visibility.Hidden;
            trunOffLbl.IsEnabled = false;
            trunOffLbl.Visibility = Visibility.Hidden;

            sourceOnCmb.IsEnabled = true;
            sourceOnCmb.Visibility = Visibility.Visible;
            sourceOffCmb.IsEnabled = true;
            sourceOffCmb.Visibility = Visibility.Visible;
        }

        private void turnOffSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            negativeOutput.TurnOffWhen = Convert.ToInt16(e.NewValue * 1000.0);
        }

        private void turnOnSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            negativeOutput.TurnOnWhen = Convert.ToInt16(e.NewValue * 1000.0);
        }       
    }
}
