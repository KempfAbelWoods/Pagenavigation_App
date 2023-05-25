﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Page_Navigation_App.Popups;

public partial class Edit_Customer : Window
{
    private string Initial_ID;
    public Edit_Customer(string Customer_ID, string name, string adress, string mail, string phone)
    {
        Initial_ID = Customer_ID;
        InitializeComponent();
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
            //hier abspeichern der Daten
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
        //hier wird geprüft ob die ID in der Datenbank schon vergeben ist.
        //wobei die ID eventuell nicht änderbar gemacht werden sollte ??
        //nur abfragen bei neuerstellung eines Kundens?
        string ID = ID_Field.Text;
        
        if (Initial_ID != ID && ID != "")
        {
           MessageBox.Show("hallo du hast den Text geändert"); 
        }
        
        
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    
    
}