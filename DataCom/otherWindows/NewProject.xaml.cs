using DataCom.globalDataStore;
using DataCom.modals;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
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
        private SaveFileDialog openFileDialog;
        private GlobalData globalData;

        public NewProject(GlobalData globalData)
        {
            InitializeComponent();
            this.globalData = globalData;
        }

        private void btnBrows_Click(object sender, RoutedEventArgs e)
        {
            if(openFileDialog == null)
            {
                openFileDialog = new SaveFileDialog();
            }            
            openFileDialog.Filter = "DataCom Files (*.dcom)|*.dcom";
            if (openFileDialog.ShowDialog() == true)
            {
                //File.WriteAllText(openFileDialog.FileName, "");//todo: add the template string inside this param
                txtFile.Text = openFileDialog.FileName;
                globalData.filePath = openFileDialog.FileName;
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
            if (btnNext.Content.Equals("done"))
            {
                DataComModal dataModals = new DataComModal();
                dataModals.comment = txtComment.Text;
                dataModals.cusName = txtCusName.Text;
                dataModals.license = txtLicense.Text;
                dataModals.publisher = txtPublisher.Text;
                dataModals.vehicle = txtVehicle.Text;
                dataModals.version = txtVersion.Text;

                List<ECU> ecus = new List<ECU>();
                List<KeyPad> keyPads = new List<KeyPad>();

                for(int i=0; i<(int) numOfEcus.Value; i++)
                {
                    ECU ecu = new ECU();
                    ecus.Add(ecu);                        
                }

                for (int i = 0; i < (int)numOfKeyPads.Value; i++)
                {
                    KeyPad ecu = new KeyPad();
                    keyPads.Add(ecu);
                }

                dataModals.ecus = ecus;
                dataModals.keyPads = keyPads;
                globalData.dataComModal = dataModals;

                string file = JsonConvert.SerializeObject(dataModals);
                if(globalData.filePath == null || globalData.filePath.Equals(""))
                {
                    globalData.showError("Error", "Please add a valid project location.");
                }
                else
                {
                    File.WriteAllText(openFileDialog.FileName, file);
                    this.Close();
                }
                
            }
            else
            {
                Page0.Visibility = Visibility.Hidden;
                Page1.Visibility = Visibility.Visible;
                btnBack.IsEnabled = true;
                btnNext.Content = "done";
            }
            
        }        
    }
}
