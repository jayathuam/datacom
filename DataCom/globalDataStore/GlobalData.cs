using DataCom.modals;
using NLog;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCom.globalDataStore
{
    public class GlobalData
    {
        private NotificationManager notificationManager;
        public string serialPort { get; set; }
        public string filePath { get; set; }
        public DataComModal dataComModal { get; set; }
        public GlobalData()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "Log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            NLog.LogManager.Configuration = config;

            notificationManager = new NotificationManager();            
        }

        public void showInfo(string title, string msg)
        {
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = msg,
                Type = NotificationType.Information
            });
        }

        public void showError(string title, string msg)
        {
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = msg,
                Type = NotificationType.Error
            });
        }

        public void showWarning(string title, string msg)
        {
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = msg,
                Type = NotificationType.Warning
            });
        }

        public void showSuccess(string title, string msg)
        {
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = msg,
                Type = NotificationType.Success
            });
        }

    }
}
