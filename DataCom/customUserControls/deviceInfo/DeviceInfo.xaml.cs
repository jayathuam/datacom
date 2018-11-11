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
        private FlowDocument flowDoc;

        public DeviceInfo()
        {
            InitializeComponent();
            keypads.Header = GlobalData.keypadname;
            mainboards.Header = GlobalData.mainBoardName;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/DataCom;component/assets/d2.PNG"));       
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

        //todo: list item click fill

        private void drawKeypad(KeyPad keyapd)
        {
            Label uuid = new Label();
            uuid.Content = "UUID : " + keyapd.uuid;

            Label label = new Label();
            label.Content = "Custom label : " + keyapd.label;

            Label keypads = new Label();
            keypads.Content = "Number of Keypads : " + keyapd.keys.Count.ToString();

            detalPanel.Children.Clear();
            detalPanel.Children.Add(uuid);
            detalPanel.Children.Add(label);
            detalPanel.Children.Add(keypads);
        }

        //todo: list item click
        private void drawMainboards(ECU ecu)
        {
            Label uuid = new Label();
            uuid.Content = "UUID : " + ecu.uuid;

            Label label = new Label();
            label.Content = "Custom label : " + ecu.label;

            Label positive = new Label();
            positive.Content = "Number of Positive outputs : " + ecu.positiveList.Count.ToString();

            Label negative = new Label();
            negative.Content = "Number of Negative outputs : " + ecu.negativeList.Count.ToString();

            Label external = new Label();
            external.Content = "Number of External inputs : " + ecu.externalList.Count.ToString();

            Label analog = new Label();
            analog.Content = "Number of Analog inputs : " + ecu.analogList.Count.ToString();

            Label combine = new Label();
            combine.Content = "Number of Combine inputs : " + ecu.combineList.Count.ToString();

            Label events = new Label();
            events.Content = "Number of Events : " + ecu.eventsList.Count.ToString();

            detalPanel.Children.Clear();
            detalPanel.Children.Add(uuid);
            detalPanel.Children.Add(label);
            detalPanel.Children.Add(positive);
            detalPanel.Children.Add(negative);
            detalPanel.Children.Add(external);
            detalPanel.Children.Add(analog);
            detalPanel.Children.Add(combine);
            detalPanel.Children.Add(events);
        }

        private void mainboardList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            string text = item.Content.ToString();
            ECU current = ecuList.Find(o => o.uuid == text);
            drawMainboards(current);
        }

        private void keypadList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            string text = item.Content.ToString();
            KeyPad current = keybadList.Find(o => o.uuid == text);
            drawKeypad(current);
        }
    }
}
