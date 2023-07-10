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
using Page_Navigation_App.Popups;
using Page_Navigation_App.Utilities;
using Path = System.IO.Path;

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
            
            //Vorlage-Path
            var (pdfdata,err1) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
            //errorhandling
            if (err1!=null) Vorlagepath.Text = "You need to create a Table in your Database first.";
            else if (pdfdata.Count == 0) Vorlagepath.Text = "No File specified";
            else if (pdfdata.Count == 1) Vorlagepath.Text = pdfdata[0].Ressource;
            else Vorlagepath.Text = "Somehow there exist 2 or more elements in the Database, pls call the support"; 
            
            //IP-Adress
            var (ipdata,err2) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
            //errorhandling
            if (err2!=null) SocketIpAddress.Text = "You need to create a Table in your Database first.";
            else if (ipdata.Count == 0) SocketIpAddress.Text = "No IP specified";
            else if (ipdata.Count == 1) SocketIpAddress.Text = ipdata[0].Ressource;
            else SocketIpAddress.Text = "Somehow there exist 2 or more elements in the Database, pls call the support"; 
            
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
        
        private void Create_Table_Ressource(object sender, RoutedEventArgs e)
        {
            var err = Db_Ressources.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }
        
        private void Create_Table_Users(object sender, RoutedEventArgs e)
        {
            var err = Db_Users.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }
        
        private void Create_Table_Tasks(object sender, RoutedEventArgs e)
        {
            var err = Db_Tasks.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }
        private void Create_Finish_order(object sender, RoutedEventArgs e)
        {
            var err = Db_FinishedOrders.CreateTable(Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
        }

        private void Set_BillPath(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                string Path = "";
                var ookiiDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
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
        }
        

        private void Set_SocketIp(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(2, true))
            {
                string SetIpAddress = SocketIpAddress.Text;
                //Spalte mit alten Daten löschen
                var (list, err1) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
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
                    ID = "3",
                    Name = "Socket IP",
                    Ressource = SetIpAddress,
                    Comment = "IP der Socket Verbindung"

                };
                var err = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                if (err != null)
                {
                    MessageBox.Show(err.GetException().Message);
                }
                else
                {
                    SocketIpAddress.Text = SetIpAddress;
                }
            }
        }

        private void Start_Server(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(2, true))
            {
                Server.SocketServer();
            }
        }
        

        private void Get_Code(object sender, RoutedEventArgs e)
        {
            var (code, err) = ConnectionHelper.RandomString(8);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            Code.Text = code;
            Paths.ConnectionCode = code;
        }

        private void Set_ModelPdf(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                string Path = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == true)
                {
                    Path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                    
                    //Spalte mit alten Daten löschen
                    var (list, err1) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
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
                        ID = "2",
                        Name = "PDFModelpath",
                        Ressource = Path,
                        Comment = "Pfad zum Laden der Vorlagenpdf"

                    };
                    var err = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                    if (err != null)
                    {
                        MessageBox.Show(err.GetException().Message);
                    }
                    else
                    {
                        Vorlagepath.Text = Path;
                    }
                }
            }
        }

        private void PreviewPDF(object sender, RoutedEventArgs e)
        {
            string examplepath = "..//..//..//data";

            if (Userhandling.GrantPermission(1, true))
            {
                try
                {
                    new PDF_Generator().PDF_Generate("example", examplepath,"",DateTime.Now.ToString("dd.MM.yyyy"));

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }


                //Nach Generierung ansehen
                Show_Bill showBill = new Show_Bill(Path.GetFullPath(examplepath + "//example.pdf"));
                showBill.Owner = Application.Current.MainWindow;
                showBill.ShowDialog();
            }
        }
    }
}
