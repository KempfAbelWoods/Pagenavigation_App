using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Log_in : Window
{
    public Log_in()
    {
        InitializeComponent();
    }
    

    private void Login(object sender, ExecutedRoutedEventArgs e)
    {
        string Password = Password_Field.Text;
        var (list,err) = Rw_Users.ReadwithUserName(Username_Field.Text, Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show("Wrong Username or Password");
        }
        if (list.Count==1)
        {
            if (StayLogged.IsChecked.Value)
            {
                var data = new Db_Settings
                {
                    ID = "4",
                    Name = "LoggedUser",
                    Ressource = list[0].Username,
                    Comment = "Hier steht der aktuell angemeldete Benutzer, wenn niemand angemeldet ist ist das Feld leer"

                };
                var err1 = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                if (err1 != null)
                {
                    MessageBox.Show(err.GetException().Message);
                }
            }
            //Todo Globale Variablen mit Login

        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        Edit_Users users = new Edit_Users();
        users.Owner = Application.Current.MainWindow;
        users.ShowDialog();
    }
}