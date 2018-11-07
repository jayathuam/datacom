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

namespace DataCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private GlobalData globalData;
        private NewProject window;
        public MainWindow()
        {
            InitializeComponent();
            globalData = new GlobalData();
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
            MenuItem mnu = (MenuItem)sender;
            MessageBox.Show(mnu.Header.ToString());
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            globalData.filePath = null;
            window = new NewProject(globalData, this);
            window.Show();
        }

        private void btnDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            deviceInfoFlyout.IsOpen = true;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void populateTree()
        {
            treeview.init(globalData, this);
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
    }
}
