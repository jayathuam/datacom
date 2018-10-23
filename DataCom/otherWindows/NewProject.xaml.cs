using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace DataCom.otherWindows
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : MetroWindow
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void btnBrows_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DataCom Files (*.dcom)|*.dcom";
            if (openFileDialog.ShowDialog() == true)
            {
                txtFile.Text = openFileDialog.FileName;
            }
                
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Page0.Visibility = Visibility.Visible;
            Page1.Visibility = Visibility.Hidden;
            btnBack.IsEnabled = false;
            btnNext.Content = "next";
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Page0.Visibility = Visibility.Hidden;
            Page1.Visibility = Visibility.Visible;
            btnBack.IsEnabled = true;
            btnNext.Content = "done";
        }
    }
}
