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
using Page_Navigation_App.Connection;
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
            
            //Bill-Path
            var (data,err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            //errorhandling
            if (err!=null) PDFPaths.Text = "You need to create a Table in your Database first.";
            else if (data.Count == 0) PDFPaths.Text = "No Directory specified";
            else if (data.Count == 1) PDFPaths.Text = data[0].Ressource;
            else PDFPaths.Text = "Somehow there exist 2 or more elements in the Database, pls call the support"; 
            
            //IP-Adress
            var (ipdata,err1) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
            //errorhandling
            if (err1!=null) SocketIpAddress.Text = "You need to create a Table in your Database first.";
            else if (ipdata.Count == 0) SocketIpAddress.Text = "No IP specified";
            else if (ipdata.Count == 1) SocketIpAddress.Text = ipdata[0].Ressource;
            else SocketIpAddress.Text = "Somehow there exist 2 or more elements in the Database, pls call the support"; 
            
            //Passwort
            var (passworddata,err2) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
            //errorhandling
            if (err2!=null) SocketPassword.Text = "You need to create a Table in your Database first.";
            else if (passworddata.Count == 0) SocketPassword.Text = "No Password specified";
            else if (passworddata.Count == 1) SocketPassword.Text = passworddata[0].Ressource;
            else SocketPassword.Text = "Somehow there exist 2 or more elements in the Database, pls call the support"; 
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
            if (ookiiDialog.ShowDialog() == true)
            {
                Path = ookiiDialog.SelectedPath;
                //Spalte mit alten Daten löschen
                var (list, err1) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
                if (err1 != null)
                {
                    MessageBox.Show(err1.GetException().Message);
                }

                var error = Rw_Settings.Delete(list, Paths.sqlite_path);
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
                var err = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                if (err != null)
                {
                    MessageBox.Show(err.GetException().Message);
                }
                else
                {
                    PDFPaths.Text = Path;
                }
            }
        }

        private void Set_Password(object sender, RoutedEventArgs e)
        {
            string SetPassword = SocketPassword.Text;
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
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
                ID = "2",
                Name = "Password",
                Ressource = SetPassword,
                Comment = "Passwort für Datenaustausch"

            };
            var err = Rw_Settings.Write(new List<Db_Settings>{data},Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            else
            {
                SocketPassword.Text = SetPassword;
            }
        }

        private void Set_SocketIp(object sender, RoutedEventArgs e)
        {
            string SetIpAddress = SocketIpAddress.Text;
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
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
                ID = "3",
                Name = "Socket IP",
                Ressource = SetIpAddress,
                Comment = "IP der Socket Verbindung"

            };
            var err = Rw_Settings.Write(new List<Db_Settings>{data},Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            else
            {
                SocketIpAddress.Text = SetIpAddress;
            }
        }

        private void Start_Server(object sender, RoutedEventArgs e)
        {
            Server.SocketServer();
        }
    }
}
