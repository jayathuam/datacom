using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataCom.customUserControls.customTree
{
    /// <summary>
    /// Interaction logic for CustomTree.xaml
    /// </summary>
    public partial class CustomTree : UserControl
    {
        public CustomTree()
        {
            InitializeComponent();
            Child child = new Child();
            child.Header = "abc";
            child.Checked = false;
                        
            Child child1 = new Child();
            child1.Header = "abc";
            child1.Checked = false;

            ObservableCollection<Child> xx = new ObservableCollection<Child>();
            xx.Add(child1);

            child.Children = xx;
            treeView.Items.Add(child);
        }

        public class Child: TreeViewItem
        {
            //public string Header { get; set; }
            public bool Checked { get; set; }
            public ObservableCollection<Child> Children { get; set; }
        }
    }
}
