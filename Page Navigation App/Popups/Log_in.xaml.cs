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
        Username_Field.Text = ActualUser.Username;
    }
    

    private void Login(object sender, ExecutedRoutedEventArgs e)
    {
        var (list,err) = Rw_Users.ReadwithUserName(Username_Field.Text, Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show("User does not exist");
        }
        if (list.Count==1)
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
                        Comment = "Hier steht der aktuell angemeldete Benutzer, wenn niemand angemeldet ist ist das Feld leer"

                    };
                    var err1 = Rw_Settings.Write(new List<Db_Settings> { data }, Paths.sqlite_path);
                    if (err1 != null)
                    {
                        MessageBox.Show(err.GetException().Message);
                    }
                }
                
                ActualUser.Username = Username_Field.Text;
               ActualUser.Password = Password_Field.Password;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        //TODo Hier muss abgefragt werden ob der angemeldete Benutzer die Rechte hat neue Anzulegen
        User_Control users = new User_Control();
        users.Owner = Application.Current.MainWindow;
        users.ShowDialog();
    }
}