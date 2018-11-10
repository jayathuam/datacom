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

namespace DataCom.customUserControls.deviceInfo
{
    /// <summary>
    /// Interaction logic for DeviceInfo.xaml
    /// </summary>
    public partial class DeviceInfo : UserControl
    {
        private List<ECU> ecuList;
        private List<KeyPad> keybadList;

        public DeviceInfo()
        {
            InitializeComponent();
            keypads.Header = GlobalData.keypadname;
            mainboards.Header = GlobalData.mainBoardName;
        }

        public void setEcu(List<ECU> ecus)
        {
            this.ecuList = ecus;
            List<string> list = ecus.Select(o => o.uuid).ToList();
            this.Dispatcher.Invoke(() =>
            {
                mainboardList.Items.Clear();
                foreach (string s in list)
                {
                    mainboardList.Items.Add(s);
                }
            });
        }

        public void setKeyPad(List<KeyPad> keybadList)
        {
            this.keybadList = keybadList;
            List<string> list = keybadList.Select(o => o.uuid).ToList();
            this.Dispatcher.Invoke(() =>
            {
                keypadList.Items.Clear();
                foreach (string s in list)
                {
                    keypadList.Items.Add(s);
                }
            });
        }
    }
}
