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
            var (data,err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err!=null)
            {
                PDFPaths.Text = "You need to create a Table in your Database first.";
            }
            else if (data.Count == 0)
            {
                PDFPaths.Text = "No Directory specified";
            }
            else if (data.Count == 1)
            {
                PDFPaths.Text = data[0].Ressource;
            }
            else
            {
                PDFPaths.Text = "Somehow there exist 2 or more elements in the Database, pls call the support";
            }
        }

        private void Create_Table_Customer(object sender, RoutedEventArgs e)
        {
            var err = Db_Customer.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }
        
        private void Create_Table_Order(object sender, RoutedEventArgs e)
        {
            var err = Db_Order.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }
        
        private void Create_Table_Settings(object sender, RoutedEventArgs e)
        {
            var err = Db_Settings.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }

        private void Set_BillPath(object sender, RoutedEventArgs e)
        {
            string Path ="";
            string correctedPath = "";
            var ookiiDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog( );
            if ( ookiiDialog.ShowDialog( ) == true )
                Path = ookiiDialog.SelectedPath;
            
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err1 != null)
            {
                MessageBox.Show(err1.GetException().Message);
            }
            var error = Rw_Settings.Delete(list,Paths.sqlite_path);
            if (error != null)
            {
                MessageBox.Show(error.GetException().Message);
            }
            //Spalte mit neuen Daten speichern
            var data = new Db_Settings
            {
                ID = "1",
                Name = "Billpath",
                Ressource = Path,
                Comment = "Pfad zum Ablegen der Rechnungspdfs"

            };
            var err = Rw_Settings.Write(new List<Db_Settings>{data},Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            else
            {
                PDFPaths.Text = Path;
            }
        }
    }
}
