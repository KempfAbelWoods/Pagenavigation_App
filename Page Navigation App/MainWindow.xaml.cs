using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Configs;
using Page_Navigation_App.Popups;

namespace Page_Navigation_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Log_in(object sender, RoutedEventArgs e)
        {
            Log_in login = new Log_in();
            login.Owner = Application.Current.MainWindow;
            login.ShowDialog();
        }
        
        private void MainWindow_OnActivated(object sender, EventArgs e)
        {

                Username_Field.Text = ActualUser.Username; 
            
        }
    }
}
