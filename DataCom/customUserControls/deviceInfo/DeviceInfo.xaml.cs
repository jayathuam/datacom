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
        private string _myProperty;

        public DeviceInfo()
        {
            InitializeComponent();
        }

        public string MyProperty
        {
            get { return _myProperty; }
            set { _myProperty = value; }
        }
    }
}
