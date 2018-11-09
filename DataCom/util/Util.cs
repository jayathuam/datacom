using DataCom.globalDataStore;
using DataCom.modals;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.util
{
    public class Util
    {
        private GlobalData globalData;
        private OpenFileDialog openFileDialog;
        private RecentProject projects;
        public Util(GlobalData globalData)
        {
            this.globalData = globalData;
            projects = new RecentProject();
        }

        public void openProject(MainWindow mainwindow)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
            }
            openFileDialog.Filter = "DataCom Files (*.dcom)|*.dcom";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                globalData.filePath = openFileDialog.FileName;
                string contents = File.ReadAllText(globalData.filePath);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                globalData.dataComModal = JsonConvert.DeserializeObject<DataComModal>(contents, settings);                
                mainwindow.populateTree();
            }
        }

        public void addForRecent(string fileName)
        {
            try
            {
                string contents = File.ReadAllText("recent.json");
                JsonSerializerSettings settings = new JsonSerializerSettings();
                projects = JsonConvert.DeserializeObject<RecentProject>(contents);
                bool exists = projects.projects.Exists(element => element.path == fileName);
                if (!exists)
                {
                    projects.projects.RemoveAt(projects.projects.Count - 1);
                    Project item = new Project();
                    item.path = fileName;
                    //item.projName = fileName.Split('')
                }
            }
            catch (Exception ex)
            {

            }            
        }

        public List<object> getFromRecent()
        {
            return null;
        }
    }

    public class RecentProject
    {
        public List<Project> projects { get; set; }
        public RecentProject()
        {

        }
    }

    public class Project
    {
        public string projName { get; set; }
        public string path { get; set; }

        public Project()
        {

        }
    }
}
