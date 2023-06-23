using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.Popups;

public partial class Edit_User : Window
{
    
    private string Initial_ID;
    public Edit_User(string ID, string name, string username, string role, string rights, string password)
    {
        //Todo verwaltung von Rechten evtl. als string mit Trennzeichen übergeben
        //Todo überprüfen ob Nutzername schon vergeben
        Initial_ID = ID;
        InitializeComponent();
        ID_Field.Text = ID;
        Name_Field.Text = name;
        Username_Field.Text = username;
        Role_Field.Text = role;
        Password_Field.Password = password;
        checkboxes(rights);
    }
    
    private void ID_Field_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string ID = ID_Field.Text;
        
        if (ID != "" && (Initial_ID == "" || ID != Initial_ID))
        {
            //check if ID is already in Database
            var (data,err) =Rw_Users.ReadwithID(ID, Paths.sqlite_path);
            if (data.Count !=0)
            {
                Save.IsEnabled = false;
                Save.Content = "ID already used";
                ID_Field.ToolTip = "ID already used";
            }
            else
            {
                Save.Content = "Save";
                Save.IsEnabled = true;
            }
        }

        if (ID == Initial_ID)
        {
            Save.Content = "Save";
            Save.IsEnabled = true;
        }
    }
    
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void checkboxes(string rights)
    {
        string[] permissions =rights.Split(",");
        foreach (var permission in permissions)
        {
            if (permission == "readaccess")
            {
                Readaccess.IsChecked = true;
            }
            if (permission == "writeaccess")
            {
                Writeaccess.IsChecked = true;
            }
            if (permission == "fullaccess")
            {
                Fullaccess.IsChecked = true;
            }
        }
        
    }
    private string Rightstring()
    {
        string rights="";
        if (Readaccess.IsChecked != null && Readaccess.IsChecked.Value)
        {
            rights = "readaccess";
        }
        if (Writeaccess.IsChecked != null && Writeaccess.IsChecked.Value)
        {
            rights = rights + "," + "writeaccess";
        }

        if (Fullaccess.IsChecked != null && Fullaccess.IsChecked.Value)
        {
            rights = rights + "," + "fullaccess";
        }

        return rights;
    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string name = Name_Field.Text;
        string Username = Username_Field.Text;
        string Role = Role_Field.Text;
        string Rights = Rightstring();
        string Password = Password_Field.Password;
        if (ID!="" && name!="" && Username !="" && Role !="" && Rights!= "" && Password!= "")
        {
            var data = new Db_Users
            {
                ID = ID,
                Name = name,
                Username = Username,
                Role = Role,
                Rights = Rights,
                Password = Password,
            };
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Users.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err1 == null)
            {
                var error = Rw_Users.Delete(list,Paths.sqlite_path);
            }
            else
            {
                MessageBox.Show(err1.GetException().Message);
            }

            //neue Spalte einfügen
            var err = Rw_Users.Write(new List<Db_Users> { data }, Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            this.Close();
        }
        else
        {
            MessageBox.Show("An Empty Textbox is not allowed!"+Initial_ID);
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Show_Password(object sender, MouseButtonEventArgs e)
    {
        Password_Field.Visibility = Visibility.Visible;
    }

    private void Hide_Password(object sender, MouseButtonEventArgs e)
    {
        Password_Field.Visibility = Visibility.Hidden;
    }
}