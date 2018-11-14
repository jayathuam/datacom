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
        private EffectedOutput selectedOutputs;
        private ObservableCollection<Child> root;
        private List<ECU> ecus;
        private List<EffectedOutput> selectedOutputList;
        private bool loadFullTree;
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
            loadFullTree = false;
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

        public void draw(List<ECU> ecus, List<EffectedOutput> selectedOutputs)
        {
            selectedOutputList = selectedOutputs;
            this.ecus = ecus;
            loadFullTree = true;
            foreach (ECU item in ecus)
            {
                ObservableCollection<Child> positiveOutputs = new ObservableCollection<Child>();
                ObservableCollection<Child> negativeOutputs = new ObservableCollection<Child>();
                foreach (PositiveOutput pos in item.positiveList)
                {
                    Child child = new Child();
                    child.Title = pos.Header.ToString();
                    child.Index = pos.Index;
                    child.mainBoardShortAddress = item.shortAddress;
                    child.CheckedVisible = true;
                    child.type = 1;
                    var x = selectedOutputList.Find(y => y.ECUShortAddress == item.shortAddress);
                    child.Checked = x == null ? false : x.PositiveOutputs.Any(y => y == child.Index);
                    positiveOutputs.Add(child);
                }
                foreach (NegativeOutput neg in item.negativeList)
                {
                    Child child = new Child();
                    child.Title = neg.Header.ToString();
                    child.Index = neg.Index;
                    child.CheckedVisible = true;
                    child.mainBoardShortAddress = item.shortAddress;
                    var x = selectedOutputList.Find(y => y.ECUShortAddress == item.shortAddress);
                    child.Checked = x == null ? false : x.NegativeOutputs.Any(y => y == child.Index);
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

                Child mainboard = new Child();
                mainboard.Title = item.Header.ToString();
                mainboard.Index = item.shortAddress;
                mainboard.CheckedVisible = false;
                //mainboard.Checked = selectedOutputList.Any(x => x.ECUShortAddress == item.shortAddress);
                mainboard.type = 0;
                mainboard.Children = new ObservableCollection<Child>();
                mainboard.Children.Add(positive);                
                mainboard.Children.Add(negative);
                root.Add(mainboard);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;
            Child dataContext = checkBox?.DataContext as Child;
            if (loadFullTree)
            {
                if (dataContext.type == 1)
                {
                    var item = selectedOutputList.Find(x => x.ECUShortAddress == dataContext.mainBoardShortAddress);
                    if(item == null)
                    {
                        EffectedOutput newOne = new EffectedOutput();
                        newOne.ECUShortAddress = dataContext.mainBoardShortAddress;
                        newOne.PositiveOutputs.Add(dataContext.Index);
                        selectedOutputList.Add(newOne);                      
                    }
                    else
                    {
                        item.PositiveOutputs.Add(dataContext.Index);
                    }                    
                }
                else if (dataContext.type == 2)
                {
                    var item = selectedOutputList.Find(x => x.ECUShortAddress == dataContext.mainBoardShortAddress);
                    if (item == null)
                    {
                        EffectedOutput newOne = new EffectedOutput();
                        newOne.ECUShortAddress = dataContext.mainBoardShortAddress;
                        newOne.NegativeOutputs.Add(dataContext.Index);
                        selectedOutputList.Add(newOne);
                    }
                    else
                    {
                        item.NegativeOutputs.Add(dataContext.Index);
                    }
                }
            }
            else
            {
                if (dataContext.type == 1)
                {
                    selectedOutputs.PositiveOutputs.Add(dataContext.Index);
                }
                else if (dataContext.type == 2)
                {
                    selectedOutputs.NegativeOutputs.Add(dataContext.Index);
                }
            }


        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;
            Child dataContext = checkBox?.DataContext as Child;
            if (loadFullTree)
            {
                if (dataContext.type == 1)
                {
                    selectedOutputList.Find(x => x.ECUShortAddress == dataContext.mainBoardShortAddress).PositiveOutputs.Remove(dataContext.Index);
                }
                else if (dataContext.type == 2)
                {
                    selectedOutputList.Find(x => x.ECUShortAddress == dataContext.mainBoardShortAddress).NegativeOutputs.Remove(dataContext.Index);
                }
            }
            else
            {
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
