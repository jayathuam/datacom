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
    /// Interaction logic for CombineInputUI.xaml
    /// </summary>
    public partial class CombineInputUI : UserControl
    {
        private CombineInputs CombineInput;
        private ECU ecu;
        private Serial serial;
        public CombineInputUI(CombineInputs CombineInput, ECU ecu, Serial serial)
        {
            InitializeComponent();
            this.CombineInput = CombineInput;
            this.ecu = ecu;
            this.serial = serial;            
            this.DataContext = CombineInput;
            targetActionCmb.ItemsSource = Enum.GetValues(typeof(TARGET_ACTION));
            index1Cmb.ItemsSource = Enum.GetValues(typeof(INPUT_SOURCE));
            index2Cmb.ItemsSource = Enum.GetValues(typeof(INPUT_SOURCE));
            operationCmb.ItemsSource = Enum.GetValues(typeof(OPERATIONS));
            cutomtree.draw(ecu, CombineInput.SelectedOutputs);
        }

        private void index1Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            INPUT_SOURCE inputSource = (INPUT_SOURCE) index1Cmb.SelectedItem;
            if(INPUT_SOURCE.Always_Off == inputSource || INPUT_SOURCE.Always_On == inputSource)
            {
                categorySource1cmb.IsEnabled = false;
                inverted1.IsEnabled = false;
            }
            else if(INPUT_SOURCE.Analog_Input == inputSource)
            {
                categorySource1cmb.IsEnabled = true;
                inverted1.IsEnabled = true;
                var items = ecu.analogList.Select(o => o.Header.ToString()).ToList();
                categorySource1cmb.ItemsSource = items;
                try
                {
                    categorySource1cmb.SelectedItem = items[CombineInput.InputIndex1];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    CombineInput.InputIndex1 = 0;
                    categorySource1cmb.SelectedItem = items[CombineInput.InputIndex1];
                }
            }
            else if (INPUT_SOURCE.Ext_Input == inputSource)
            {
                categorySource1cmb.IsEnabled = true;
                inverted1.IsEnabled = true;
                var items = ecu.externalList.Select(o => o.Header.ToString()).ToList();
                categorySource1cmb.ItemsSource = items;
                try
                {
                    categorySource1cmb.SelectedItem = items[CombineInput.InputIndex1];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    CombineInput.InputIndex1 = 0;
                    categorySource1cmb.SelectedItem = items[CombineInput.InputIndex1];
                }
            }
        }

        private void index2Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            INPUT_SOURCE inputSource = (INPUT_SOURCE)index2Cmb.SelectedItem;
            if (INPUT_SOURCE.Always_Off == inputSource || INPUT_SOURCE.Always_On == inputSource)
            {
                categorySource2cmb.IsEnabled = false;
                inverted2.IsEnabled = false;
            }
            else if (INPUT_SOURCE.Analog_Input == inputSource)
            {
                categorySource2cmb.IsEnabled = true;
                inverted2.IsEnabled = true;
                var items = ecu.analogList.Select(o => o.Header.ToString()).ToList();
                categorySource2cmb.ItemsSource = items;
                try
                {
                    categorySource2cmb.SelectedItem = items[CombineInput.InputIndex2];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    CombineInput.InputIndex1 = 0;
                    categorySource2cmb.SelectedItem = items[CombineInput.InputIndex2];
                }
            }
            else if (INPUT_SOURCE.Ext_Input == inputSource)
            {
                categorySource2cmb.IsEnabled = true;
                inverted2.IsEnabled = true;
                var items = ecu.externalList.Select(o => o.Header.ToString()).ToList();
                categorySource2cmb.ItemsSource = items;
                try
                {
                    categorySource2cmb.SelectedItem = items[CombineInput.InputIndex2];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    CombineInput.InputIndex1 = 0;
                    categorySource2cmb.SelectedItem = items[CombineInput.InputIndex2];
                }
            }
        }
    }
}
