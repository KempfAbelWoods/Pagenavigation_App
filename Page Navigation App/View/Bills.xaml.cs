using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;
using Page_Navigation_App.Popups;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Bills : UserControl
    {
        private string SourcePath = "";

        public Bills()
        {
            InitializeComponent();
            var (data, err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            else if (data.Count == 0)
            {
                MessageBox.Show("No Directory for Pdf Files set");
            }
            else if (data.Count == 1)
            {
                SourcePath = data[0].Ressource;
                GetBills(SourcePath);
            }
            else
            {
                MessageBox.Show("Somehow there exist 2 or more elements in the Database, pls call the support.");
            }
        }

        public void GetBills(string source)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(source);
                FileInfo[] files = directoryInfo.GetFiles("*.pdf");
                ObservableCollection<Sources> Filesource = new ObservableCollection<Sources>();
                if (files.Length != 0)
                {
                    foreach (var file in files)
                    {
                        Filesource.Add(new Sources
                        {
                            Adress = file.FullName, Name = file.Name, Größe = file.Length.ToString(),
                            Änderungsdatum = file.LastWriteTime.ToString()
                        });
                    }
                }

                PdfDataGrid.ItemsSource = Filesource;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ViewFile(object sender, ExecutedRoutedEventArgs e)
        {
            Show_Bill showBill = new Show_Bill(e.Parameter.ToString());
            showBill.Owner = Application.Current.MainWindow;
            showBill.ShowDialog();
        }

        private void DeleteFile(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                Delete_Pdf showBill = new Delete_Pdf(e.Parameter.ToString());
                showBill.Owner = Application.Current.MainWindow;
                showBill.ShowDialog();
                GetBills(SourcePath);
            }
        }

        public class Sources
        {
            public string Adress { get; set; }
            public string Name { get; set; }
            public string Größe { get; set; }
            public string Änderungsdatum { get; set; }
        }
    }
}