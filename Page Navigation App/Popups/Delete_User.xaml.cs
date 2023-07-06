using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Delete_User : Window
{
    private string Initial_ID;

    public Delete_User(string User_ID)
    {
        Initial_ID = User_ID;
        InitializeComponent();
        this.Order_ID.Text = User_ID;
    }

    private void Delete_User_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        bool createAdmin = true;
        bool getID = true;
        var data = new Db_Users()
        {
            ID = Initial_ID,
        };
        //Wenn deleteable löschen
        if (Check_Delete())
        {
            //TODO Nutzer auch aus Settings löschen wenn er eingeloggt ist
            var (user, err5) = Rw_Users.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err5 != null)
            {
                MessageBox.Show(err5.GetException().Message);
            }

            var (setting, err6) = Rw_Settings.ReadwithID("4", Paths.sqlite_path);
            if (err6 != null)
            {
                MessageBox.Show(err6.GetException().Message);
            }

            if (user.Count == 1)
            {
                if (setting.Count == 1)
                {
                    if (user[0].Username == setting[0].Ressource)
                    {
                        var dat = new Db_Settings()
                        {
                            ID = "4",
                        };
                        var err7 = Rw_Settings.Delete(new List<Db_Settings> { dat }, Paths.sqlite_path);
                        if (err7 != null)
                        {
                            MessageBox.Show(err7.GetException().Message);
                        }
                    }
                }

                var err = Rw_Users.Delete(new List<Db_Users> { data }, Paths.sqlite_path);
                if (err != null)
                {
                    MessageBox.Show(err.GetException().Message);
                }
            }


            var (list, err1) = Rw_Users.Read("", Paths.sqlite_path);
            if (err1 != null)
            {
                MessageBox.Show(err1.GetException().Message);
            }

            foreach (var user1 in list)
            {
                if (user1.Rights.Split(",").Length == 3)
                {
                    createAdmin = false;
                }
            }

            //wenn kein Admin Account mehr vorhanden ist den default admin erstellen
            if (createAdmin)
            {
                int adminID = 0;
                while (getID)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].ID == (adminID+1).ToString())
                        {
                            getID = true;
                            break;
                        }

                        getID = false;
                    }

                    if (list.Count == 0)
                    { 
                        getID = false;
                    }

                    adminID++;
                }

                var user2 = new Db_Users()
                {
                    ID = adminID.ToString(),
                    Username = "admin",
                    Password = "root",
                    Name = "Administrator",
                    Rights = "readaccess,writeaccess,fullaccess",
                    Role = "Administrator"
                };
                var error = Rw_Users.Write(new List<Db_Users> { user2 }, Paths.sqlite_path);
                if (error != null)
                {
                    MessageBox.Show(error.GetException().Message);
                }
            }
        }
        else
        {
            MessageBox.Show("User not deletable, there are Open Tasks");
        }
        this.Close();
    }

    public bool Check_Delete()
    {
        //Prüfen ob aktive Tasks oder Irgendwas auf den Nutzer läuft
        var (tasks, err) = Rw_Tasks.Read("", Paths.sqlite_path);
        if (err != null)
        {
            MessageBox.Show(err.GetException().Message);
        }

        var (user, err5) = Rw_Users.ReadwithID(Initial_ID, Paths.sqlite_path);
        if (err5 != null)
        {
            MessageBox.Show(err5.GetException().Message);
        }

        if (user.Count == 1)
        {
            foreach (var task in tasks)
            {
                if (task.Username == user[0].Username)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}