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
            if (ActualUser.Username != "")
            {
                Username_Field.Text = ActualUser.Username;
            }
        }
        private void Log_in(object sender, RoutedEventArgs e)
        {
            Log_in login = new Log_in();
            login.Owner = Application.Current.MainWindow;
            login.ShowDialog();
        }

        public static void getuser()
        {
            if (ActualUser.Username != "")
            {
                //Username_Field.Text = ActualUser.Username;
                //Todo noch irgendwie schauen das man den Username bei einem Change aktualisiert.
                //NotifyPropertyChanged https://www.appsloveworld.com/csharp/100/1187/refresh-textbox-when-variable-changes
            }
        }

    }
}
