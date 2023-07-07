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
        if (ActualUser.Username!="")
        { Save.Content = "Logout"; }
        else
        { Save.Content = "Login"; }
        Username_Field.Text = ActualUser.Username;
    }


    private void Login(object sender, ExecutedRoutedEventArgs e)
    {
        if (ActualUser.Username == "")
        {
            var (list, err) = Rw_Users.ReadwithUserName(Username_Field.Text, Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            else if (list.Count == 0)
            {
                MessageBox.Show("User does not exist");
            }

            if (list.Count == 1)
            {
                if (Password_Field.Password == list[0].Password)
                {
                    if (StayLogged.IsChecked != null && StayLogged.IsChecked.Value)
                    {
                        var data = new Db_Settings
                        {
                            ID = "4",
                            Name = "LoggedUser",
                            Ressource = list[0].Username,
                            Comment =
                                "Hier steht der aktuell angemeldete Benutzer, wenn niemand angemeldet ist ist das Feld leer"
                        };
                        Delete_User_Settings();

                        //neue Zeile einfügen
                        var err1 = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                        if (err1 != null)
                        {
                            MessageBox.Show(err.GetException().Message);
                        }
                    }

                    ActualUser.Username = Username_Field.Text;
                    ActualUser.Password = Password_Field.Password;
                    Save.Content = "Logout";
                    Username_Field.Text = ActualUser.Username;
                }
                else
                {
                    MessageBox.Show("Wrong Password");
                }
            }
        }
        else
        {
            ActualUser.Username = "";
            ActualUser.Password = "";
            Save.Content = "Login";
            Username_Field.Text = ActualUser.Username;
            Delete_User_Settings();
        }
    }

    private void Delete_User_Settings()
    {
        //alte Zeile löschen
        var (Row, err2) = Rw_Settings.ReadwithID("4", Paths.sqlite_path);
        if (err2 != null)
        {
            MessageBox.Show(err2.GetException().Message);
        }

        if (Row.Count == 1)
        {
            var error = Rw_Settings.Delete(Row, Paths.sqlite_path);
            if (error != null)
            {
                MessageBox.Show(error.GetException().Message);
            }
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
       
        User_Control users = new User_Control();
        users.Owner = Application.Current.MainWindow;
        users.ShowDialog();
    }
}