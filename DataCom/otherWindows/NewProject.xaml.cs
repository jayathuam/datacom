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
using static DataCom.modals.enums.Definitions;

namespace DataCom.otherWindows
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : MetroWindow
    {
        private SaveFileDialog openFileDialog;
        private GlobalData globalData;
        private MainWindow mainWindow;

        public NewProject(GlobalData globalData,MainWindow mainWindow)
        {
            InitializeComponent();
            this.globalData = globalData;
            this.mainWindow = mainWindow;
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
                ecus.AddRange(Enumerable.Repeat(0, (int)numOfEcus_1.Value).Select(x => new ECU(ECU_TYPE.MainboardCatogory_System_1)));
                ecus.AddRange(Enumerable.Repeat(0, (int)numOfEcus_2.Value).Select(x => new ECU(ECU_TYPE.MainboardCatogory_System_2)));
                ecus.AddRange(Enumerable.Repeat(0, (int)numOfEcus_3.Value).Select(x => new ECU(ECU_TYPE.MainboardCatogory_System_3)));
                ecus.AddRange(Enumerable.Repeat(0, (int)numOfEcus_4.Value).Select(x => new ECU(ECU_TYPE.MainboardCatogory_System_4)));                

                List<KeyPad> keyPads = new List<KeyPad>();
                keyPads.AddRange(Enumerable.Repeat(0, (int)numOfKeyPads_1.Value).Select(x => new KeyPad(KEYPAD_TYPE.KeypadCatogory_Keypad_5_plus_1)));
                keyPads.AddRange(Enumerable.Repeat(0, (int)numOfKeyPads_2.Value).Select(x => new KeyPad(KEYPAD_TYPE.KeypadCatogory_Keypad_8_plus_2)));
                keyPads.AddRange(Enumerable.Repeat(0, (int)numOfKeyPads_3.Value).Select(x => new KeyPad(KEYPAD_TYPE.KeypadCatogory_Keypad_10_plus_2)));                               

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
                    File.WriteAllText(globalData.filePath, file);
                    globalData.showSuccess("Success", "Project created successfully.");
                    mainWindow.populateTree();
                    mainWindow.moveToProjectScreen();
                    this.Close();
                }
                
            }
            else
            {
                Page0.Visibility = Visibility.Hidden;
                Page1.Visibility = Visibility.Visible;
                btnBack.IsEnabled = true;
                btnNext.Content = "done";
                if(globalData.filePath != null && !globalData.filePath.Equals(""))
                {
                    txtFile.Text = globalData.filePath;
                }
            }
            
        }        
    }
}
