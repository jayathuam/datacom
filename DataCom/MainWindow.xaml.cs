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
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using DataCom.globalDataStore;
using DataCom.otherWindows;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using DataCom.modals;
using Newtonsoft.Json.Linq;
using DataCom.SerialCommunication;

namespace DataCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private GlobalData globalData;
        private NewProject window;
        private OpenFileDialog openFileDialog;
        private Serial serial;
        private TransportLayer transporLayer;
        private CANInterface canInterface;
        private CommandHandler commandHandler;

        public MainWindow()
        {
            InitializeComponent();
            globalData = new GlobalData();
            transporLayer = new TransportLayer();
            serial = new Serial(globalData, transporLayer);
            serial.deviceInfoEvent += device_info_event;
            canInterface = new CANInterface(transporLayer, serial);
            commandHandler = new CommandHandler(canInterface, transporLayer, serial);
            mainGrid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "assets/3.jpg")));
        }

        private void device_info_event(object sender, bool e)
        {
            if (e)
            {
                deviceInfoUserControl.setEcu(globalData.deviceInfo.ecus);
                deviceInfoUserControl.setKeyPad(globalData.deviceInfo.keyPads);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            fillPortMenu();
        }

        private PackIconFontAwesome getPortSelectedIcon()
        {
            PackIconFontAwesome icon = new PackIconFontAwesome();
            icon.Kind = PackIconFontAwesomeKind.DotCircleRegular;
            icon.Width = 10;
            icon.Height = 10;
            icon.Margin = new Thickness(2, 0, 0, 0);
            return icon;
        }

        private void fillPortMenu()
        {
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                MenuItem temp = new MenuItem();
                temp.Header = s;
                temp.Click += new RoutedEventHandler(item_Click);
                menuItmPort.Items.Add(temp);
            }
        }

        public void item_Click(Object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menuItmPort.Items)
            {
                if(item.Icon != null)
                {
                    item.Icon = null;
                }
            }
            MenuItem mnu = (MenuItem)sender;
            mnu.Icon = getPortSelectedIcon();
            globalData.serialPort = mnu.Header.ToString();
            serial.connect();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            globalData.filePath = null;
            window = new NewProject(globalData, this);
            window.Show();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
            }
            openFileDialog.Filter = "DataCom Files (*.dcom)|*.dcom";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                globalData.filePath = openFileDialog.FileName;
                string contents = File.ReadAllText(globalData.filePath);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                globalData.dataComModal = JsonConvert.DeserializeObject<DataComModal>(contents, settings);
                //string file = JsonConvert.SerializeObject(globalData.dataComModal);
                //File.WriteAllText("xxx.json", file);
                moveToProjectScreen();
                populateTree();
            }
        }

        private void btnDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            commandHandler.cmdRequestDevInfo();
            Console.WriteLine("send request for info");
            deviceInfoFlyout.IsOpen = true;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(Environment.ExitCode);
        }

        public void populateTree()
        {
            treeview.init(globalData, serial, this);
        }

        public void addChildToPanel(UIElement item)
        {
            controPanel.Children.Clear();
            controPanel.Children.Add(item);
        }

        public void clearPanel()
        {
            controPanel.Children.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string file = JsonConvert.SerializeObject(globalData.dataComModal);
            File.WriteAllText(globalData.filePath, file);
            globalData.showSuccess("Success", "Project saved successfully.");
        }

        private void GetDeviceInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        public void moveToProjectScreen()
        {
            var bc = new BrushConverter();
            startupScreen.Visibility = Visibility.Hidden;
            mainGrid.Background = (Brush)bc.ConvertFrom("#FFA09E9E");
            startupGrid.Background = (Brush)bc.ConvertFrom("#FFF");
        }

        public void moveToStartupScreen()
        {
            startupScreen.Visibility = Visibility.Visible;
            mainGrid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "assets/3.jpg")));
            startupGrid.Background = null;
        }
    }
}
