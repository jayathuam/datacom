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

namespace DataCom.customUserControls.openingDialog
{
    /// <summary>
    /// Interaction logic for OpeningDialog.xaml
    /// </summary>
    public partial class OpeningDialog : UserControl
    {
        public OpeningDialog()
        {
            InitializeComponent();
            //conainer.Background = new ImageBrush(new BitmapImage(new Uri(@"/DataCom;component/assets/3.jpg")));
        }
    }
}
