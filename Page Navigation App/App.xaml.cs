using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Page_Navigation_App.View;
using Page_Navigation_App.ViewModel;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

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
    }
}
