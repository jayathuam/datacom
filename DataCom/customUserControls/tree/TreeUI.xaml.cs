using DataCom.customUserControls.customConfigs;
using DataCom.globalDataStore;
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

namespace DataCom.customUserControls.tree
{
    /// <summary>
    /// Interaction logic for TreeUI.xaml
    /// </summary>
    public partial class TreeUI : UserControl
    {
        private GlobalData globalData;
        private MainWindow mainWindow;

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
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(AnalogInput).Equals((e.NewValue.GetType())))
            {
                AnalogInputUI item = new AnalogInputUI((AnalogInput)e.NewValue);
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(ExternalInput).Equals((e.NewValue.GetType())))
            {
                ExternalInputUI item = new ExternalInputUI((ExternalInput)e.NewValue);
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(PositiveOutput).Equals((e.NewValue.GetType())))
            {
                PositiveOutputUI item = new PositiveOutputUI((PositiveOutput)e.NewValue);
                mainWindow.addChildToPanel(item);
            }
            else if (typeof(NegativeOutput).Equals((e.NewValue.GetType())))
            {
                NegativeOutputUI item = new NegativeOutputUI((NegativeOutput)e.NewValue);
                mainWindow.addChildToPanel(item);
            }

        }

        public void init(GlobalData globalData, MainWindow mainWindow)
        {
            this.globalData = globalData;
            this.mainWindow = mainWindow;
            treeView.Items.Clear();
            if (globalData.dataComModal.ecus.Count > 0)
            {
                TreeViewItem mainBoards = new TreeViewItem();
                mainBoards.Header = "MainBoards";
                mainBoards.Background = null;
                int num = 1;
                foreach (ECU ecu in globalData.dataComModal.ecus)
                {
                    TreeViewItem mainBoard = new TreeViewItem();
                    mainBoard.Header = "MainBoard_" + num;
                    mainBoard.Background = null;

                    createChild("Positive Outputs", "positive_output_", mainBoard, ecu.positiveList);
                    createChild("Negativee Outputs", "negativee_output_", mainBoard, ecu.negativeList);
                    createChild("External Inputs", "external_input_", mainBoard, ecu.externalList);
                    createChild("Analog Inputs", "analog_input_", mainBoard, ecu.analogList);
                    createChild("Combine Inputs", "combine_inputs_", mainBoard, ecu.combineList);
                    createChild("Events", "events_", mainBoard, ecu.eventsList);
                    num++;

                    //TreeViewItem loadShading = new TreeViewItem();
                    ecu.loadShading.Header = "Load Shading";
                    ecu.loadShading.Background = null;
                    ecu.loadShading.Padding = new Thickness(0, 3, 0, 3);
                    ecu.loadShading.Template = mainBoard.Template;

                    ecu.powerManagement.Header = "Power Management";
                    ecu.powerManagement.Background = null;
                    ecu.powerManagement.Padding = new Thickness(0, 3, 0, 3);
                    ecu.powerManagement.Template = mainBoard.Template;
                    //loadShading.Header = "Load Shading";
                    //loadShading.Background = null;
                    //TreeViewItem power = new TreeViewItem();
                    //power.Header = "Power Management";
                    //power.Background = null;
                    mainBoard.Items.Add(ecu.powerManagement);
                    mainBoard.Items.Add(ecu.loadShading);
                    mainBoards.Items.Add(mainBoard);
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
                    TreeViewItem keypad = new TreeViewItem();
                    keypad.Header = "keypad_" + num;
                    keypad.Background = null;
                    createChild("Keys", "Key_", keypad, item.keys);
                    KeyPads.Items.Add(keypad);
                }
                treeView.Items.Add(KeyPads);
            }

        }

        private void createChild(string parentName, string name, TreeViewItem ParentItem, List<object> list)
        {
            int num = 1;
            if (list.Count > 0)
            {
                TreeViewItem parent = new TreeViewItem();
                parent.Header = parentName;
                parent.Background = null;
                foreach (TreeViewItem item in list)
                {
                    item.Header = name + num;
                    item.Padding = new Thickness(0, 3, 0, 3);
                    item.Template = parent.Template;
                    parent.Items.Add(item);
                    num++;
                }
                ParentItem.Items.Add(parent);
            }
        }

        //private void createChild(string parentName, string name, int count, TreeViewItem ParentItem)
        //{
        //    int num = 1;
        //    if (count > 0)
        //    {
        //        TreeViewItem parent = new TreeViewItem();
        //        parent.Header = parentName;
        //        for (int i = 0; i < count; i++)
        //        {
        //            TreeViewItem childItem = new TreeViewItem();
        //            childItem.Header = name + num;
        //            childItem.Background = null;
        //            parent.Items.Add(childItem);
        //            num++;
        //        }
        //        ParentItem.Items.Add(parent);
        //    }
        //}
    }
}
