using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Create_Table_Customer(object sender, RoutedEventArgs e)
        {
            var err = Db_Customer.CreateTable(Paths.sqlite_path);
        }
        
        private void Create_Table_Order(object sender, RoutedEventArgs e)
        {
            var err = Db_Order.CreateTable(Paths.sqlite_path);
        }

        private void Set_BillPath(object sender, RoutedEventArgs e)
        {
            string Path ="";
            string correctedPath = "";
            var ookiiDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog( );
            if ( ookiiDialog.ShowDialog( ) == true )
                Path = ookiiDialog.SelectedPath;
            correctedPath = Path.Replace('\'','/');
            PDFPaths.Text = correctedPath;
            Trace.WriteLine(correctedPath);
        }
    }
}
