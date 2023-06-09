using System;
using System.Windows;
using System.Windows.Controls;

namespace Page_Navigation_App.Popups;

public partial class Show_Bill : Window
{
    public Show_Bill(string source)
    {
        InitializeComponent();
        try
        {
            WebBrowser.Navigate(source);
        }
        catch (Exception e)
        {
            MessageBox.Show("File is damaged");
        }
    }
    
}