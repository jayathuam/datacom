using DataCom.customUserControls.customConfigs;
using DataCom.globalDataStore;
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

namespace DataCom.customUserControls.tree
{
    /// <summary>
    /// Interaction logic for TreeUI.xaml
    /// </summary>
    public partial class TreeUI : UserControl
    {
        private GlobalData globalData;
        private MainWindow mainWindow;
        private Serial serial;

        public TreeUI()
        {
            InitializeComponent();
            treeView.SelectedItemChanged += treeItem_Selected;
        }

        private void treeItem_Selected(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TreeViewItem item = sender as TreeViewItem;           
            if (typeof(LoadShading).Equals((e.NewValue.GetType())))
            {
                LoadShadingUI item = new LoadShadingUI((LoadShading)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(AnalogInput).Equals((e.NewValue.GetType())))
            {
                AnalogInputUI item = new AnalogInputUI((AnalogInput)e.NewValue, serial);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(ExternalInput).Equals((e.NewValue.GetType())))
            {
                ExternalInputUI item = new ExternalInputUI((ExternalInput)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(PositiveOutput).Equals((e.NewValue.GetType())))
            {
                //typeof(ECU).Equals(e.)
                TreeViewItem x = (TreeViewItem)e.NewValue;
                ECU ecu = (ECU) ((TreeViewItem) x.Parent).Parent;                
                PositiveOutputUI item = new PositiveOutputUI((PositiveOutput)e.NewValue, ecu);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(NegativeOutput).Equals((e.NewValue.GetType())))
            {
                NegativeOutputUI item = new NegativeOutputUI((NegativeOutput)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(CombineInputs).Equals((e.NewValue.GetType())))
            {
                CombineInputUI item = new CombineInputUI((CombineInputs)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(modals.Key).Equals((e.NewValue.GetType())))
            {
                ButtonUI item = new ButtonUI((modals.Key)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(KeyPad).Equals((e.NewValue.GetType())))
            {
                KeyPadUI item = new KeyPadUI((KeyPad)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(ECU).Equals((e.NewValue.GetType())))
            {
                EcuUI item = new EcuUI((ECU)e.NewValue);
                item.Width = mainWindow.startupGrid.ActualWidth;
                mainWindow.addChildToPanel(item);
            }
            else
            {
                mainWindow.clearPanel();
            }

        }

        public void init(GlobalData globalData, Serial serial, MainWindow mainWindow)
        {
            this.globalData = globalData;
            this.mainWindow = mainWindow;
            this.serial = serial;
            treeView.Items.Clear();
            if (globalData.dataComModal.ecus.Count > 0)
            {
                TreeViewItem mainBoards = new TreeViewItem();
                mainBoards.Header = "MainBoards";
                mainBoards.Background = null;
                int num = 1;
                foreach (ECU ecu in globalData.dataComModal.ecus)
                {
                    if (ecu.Header == null)
                    {
                        ecu.Header = "ECU_" + num;
                    }                    
                    ecu.Name = ecu.Header.ToString().Replace(" ", string.Empty);
                    ecu.Background = null;
                    ecu.Padding = new Thickness(0, 3, 0, 3);
                    ecu.Template = mainBoards.Template;

                    createChild("Positive Outputs", "positive_output_", ecu, ecu.positiveList.Cast<Object>().ToList());
                    createChild("Negativee Outputs", "negativee_output_", ecu, ecu.negativeList.Cast<Object>().ToList());
                    createChild("External Inputs", "external_input_", ecu, ecu.externalList.Cast<Object>().ToList());
                    createChild("Analog Inputs", "analog_input_", ecu, ecu.analogList.Cast<Object>().ToList());
                    createChild("Combine Inputs", "combine_inputs_", ecu, ecu.combineList.Cast<Object>().ToList());
                    createChild("Events", "events_", ecu, ecu.eventsList.Cast<Object>().ToList());
                    num++;

                    if (ecu.loadShading.Header == null)
                    {
                        ecu.loadShading.Header = "Load Shading";
                    }                    
                    ecu.loadShading.Name = ecu.loadShading.Header.ToString().Replace(" ", string.Empty);
                    ecu.loadShading.Background = null;
                    ecu.loadShading.Padding = new Thickness(0, 3, 0, 3);
                    ecu.loadShading.Template = ecu.Template;

                    if (ecu.powerManagement.Header == null)
                    {
                        ecu.powerManagement.Header = "Power Management";
                    }                    
                    ecu.powerManagement.Name = ecu.powerManagement.Header.ToString().Replace(" ", string.Empty);
                    ecu.powerManagement.Background = null;
                    ecu.powerManagement.Padding = new Thickness(0, 3, 0, 3);
                    ecu.powerManagement.Template = ecu.Template;
                    ecu.Items.Add(ecu.powerManagement);
                    ecu.Items.Add(ecu.loadShading);
                    mainBoards.Items.Add(ecu);
                }
                treeView.Items.Add(mainBoards);
            }

            if (globalData.dataComModal.keyPads.Count > 0)
            {
                TreeViewItem KeyPads = new TreeViewItem();
                KeyPads.Header = "Keypads";
                KeyPads.Background = null;                             
                int num = 1;
                foreach (KeyPad item in globalData.dataComModal.keyPads)
                {
                    if (item.Header == null)
                    {
                        item.Header = "keypad_" + num;
                    }                    
                    item.Name = item.Header.ToString().Replace(" ", string.Empty);
                    item.Background = null;
                    item.Padding = new Thickness(0, 3, 0, 3);
                    item.Template = KeyPads.Template;
                    createChild("Keys", "Key_", item, item.keys.Cast<Object>().ToList());
                    KeyPads.Items.Add(item);
                    num++;
                }
                treeView.Items.Add(KeyPads);                
            }

        }

        private void createChild(string parentName, string name, TreeViewItem ParentItem, List<Object> list)
        {
            int num = 1;
            if (list.Count > 0)
            {
                TreeViewItem parent = new TreeViewItem();
                parent.Header = parentName;
                parent.Background = null;                
                foreach (TreeViewItem item in list)
                {
                    if(item.Header == null)
                    {
                        item.Header = name + num;
                    }                    
                    item.Name = item.Header.ToString().Replace(" ",string.Empty);
                    item.Padding = new Thickness(0, 3, 0, 3);
                    item.Template = parent.Template;
                    parent.Items.Add(item);
                    num++;
                }
                ParentItem.Items.Add(parent);
            }
        }        
    }
}
