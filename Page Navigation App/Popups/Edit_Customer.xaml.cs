using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.View;
using Page_Navigation_App.Helper;


namespace Page_Navigation_App.Popups;

public partial class Edit_Customer : Window
{
    private string Initial_ID;
    public Edit_Customer(string Customer_ID, string name, string adress, string mail, string phone, bool IDenable)
    {
        Initial_ID = Customer_ID;
        InitializeComponent();
        if (!IDenable)
        { ID_Field.IsReadOnly = true; }
        else
        { ID_Field.IsReadOnly = false; }
        ID_Field.Text = Customer_ID;
        Name_Field.Text = name;
        Adress_Field.Text = adress;
        Mail_Field.Text = mail;
        Phone_Field.Text = phone;
    }

    void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string name = Name_Field.Text;
        string adress = Adress_Field.Text;
        string mail = Mail_Field.Text;
        string phone = Phone_Field.Text;
        if (ID!="" && name!="" && adress !="" && mail !="" && phone!= "")
        {
            //Definition der zu schreibenden Reihe
            var data = new Db_Customer
            {
                ID = ID,
                Name = name,
                Mail = mail,
                Adress = adress,
                Phone = phone,
                Character = name.Substring(0,1),
                BgColor = RandomColor.GenerateColor()
            };
            //Spalte mit alten Daten löschen
            var (list, err1) = RW_Customer.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err1 == null)
            {
                var error = RW_Customer.Delete(list,Paths.sqlite_path);
                if (error!=null)
                {
                    MessageBox.Show(error.GetException().Message);
                }
            }
            else
            {
                MessageBox.Show(err1.GetException().Message);
            }

            //neue Spalte einfügen
            var err = RW_Customer.Write(new List<Db_Customer> { data }, Paths.sqlite_path);
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

    void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void ID_Field_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string ID = ID_Field.Text;
        
        if (ID != "" && (Initial_ID == "" || ID != Initial_ID))
        {
           //check if ID is already in Database
           var (data,err) =RW_Customer.ReadwithID(ID, Paths.sqlite_path);
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
    
}