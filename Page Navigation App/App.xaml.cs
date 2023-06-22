using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.View;
using Page_Navigation_App.ViewModel;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using StartupEventArgs = System.Windows.StartupEventArgs;

namespace Page_Navigation_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message + " You need to restart the Application", "Fatal Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            //Harter Exit
            Environment.Exit(0);
        }

        void App_Startup (object sender, StartupEventArgs e)
        {
            //hier wird beim Startup der in der Datenbank angemeldete Benutzer automatisch angemeldet
            var (data,err) = Rw_Settings.ReadwithID("4", Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().ToString());
            }
            else if (data.Count == 1)
            {
                var (list,error) =Rw_Users.ReadwithUserName(data[0].Ressource, Paths.sqlite_path);
                if (error!=null)
                {
                    MessageBox.Show(err.GetException().ToString()); 
                }
                else if (list.Count == 1)
                {
                    ActualUser.Username = list[0].Username;
                    ActualUser.Password = list[0].Password;
                }

            }
            

        }
    }
}
