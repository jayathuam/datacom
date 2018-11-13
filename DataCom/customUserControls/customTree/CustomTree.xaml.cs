using DataCom.modals;
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
        private ECU ecu;
        //public static readonly DependencyProperty SelectedOutputsProperty = DependencyProperty.Register
        //    (
        //         "SelectedOutputs",
        //         typeof(EffectedOutput),
        //         typeof(CustomTree),
        //         new PropertyMetadata(new EffectedOutput())
        //    );

        //public EffectedOutput SelectedOutputs
        //{
        //    get { return (EffectedOutput)GetValue(SelectedOutputsProperty); }
        //    set { SetValue(SelectedOutputsProperty, value); }
        //}

        private EffectedOutput selectedOutputs;
        private ObservableCollection<Child> root;


        public CustomTree()
        {
            InitializeComponent();
            root = new ObservableCollection<Child>();
            this.DataContext = root;
        }

        public void draw(ECU ecu, EffectedOutput selectedOutputs)
        {
            this.ecu = ecu;
            this.selectedOutputs = selectedOutputs;
            ObservableCollection<Child> positiveOutputs = new ObservableCollection<Child>();
            ObservableCollection<Child> negativeOutputs = new ObservableCollection<Child>();

            foreach (PositiveOutput item in ecu.positiveList)
            {
                Child child = new Child();
                child.Title = item.Header.ToString();
                child.Index = item.Index;
                child.mainBoardShortAddress = ecu.shortAddress;
                child.CheckedVisible = true;
                child.type = 1;
                child.Checked = selectedOutputs.PositiveOutputs.Any(x => x == child.Index);
                positiveOutputs.Add(child);
            }

            foreach (NegativeOutput item in ecu.negativeList)
            {
                Child child = new Child();
                child.Title = item.Header.ToString();
                child.Index = item.Index;
                child.mainBoardShortAddress = ecu.shortAddress;
                child.CheckedVisible = true;
                child.Checked = selectedOutputs.NegativeOutputs.Any(x => x == child.Index);
                child.type = 2;
                negativeOutputs.Add(child);
            }

            Child positive = new Child();
            positive.Title = "Positive Outputs";
            positive.CheckedVisible = false;
            positive.Children = positiveOutputs;

            Child negative = new Child();
            negative.Title = "Negative Outputs";
            negative.CheckedVisible = false;
            negative.Children = negativeOutputs;

            root.Add(positive);
            root.Add(negative);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;
            Child dataContext = checkBox?.DataContext as Child;
            if (dataContext.type == 1)
            {
                selectedOutputs.PositiveOutputs.Add(dataContext.Index);
            }
            else if (dataContext.type == 2)
            {
                selectedOutputs.NegativeOutputs.Add(dataContext.Index);
            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;
            Child dataContext = checkBox?.DataContext as Child;
            if (dataContext.type == 1)
            {
                selectedOutputs.PositiveOutputs.Remove(dataContext.Index);
            }
            else if (dataContext.type == 2)
            {
                selectedOutputs.NegativeOutputs.Remove(dataContext.Index);
            }
        }
    }

    public class Child
    {

        public string Title { get; set; }
        public bool Checked { get; set; }
        public bool CheckedVisible { get; set; }
        public int mainBoardShortAddress { get; set; }
        public int Index { get; set; }
        public int type { get; set; }
        public ObservableCollection<Child> Children { get; set; }
    }

    public class EffectedOutput
    {
        public int ECUShortAddress { get; set; }
        public List<int> PositiveOutputs { get; set; }
        public List<int> NegativeOutputs { get; set; }

        public EffectedOutput()
        {
            PositiveOutputs = new List<int>();
            NegativeOutputs = new List<int>();
        }
    }
}
