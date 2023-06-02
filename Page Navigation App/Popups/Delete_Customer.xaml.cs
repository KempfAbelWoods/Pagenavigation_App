using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.View;

namespace Page_Navigation_App.Popups;

public partial class Delete_Customer : Window
{
    private string Initial_ID;
    public Delete_Customer(string Customer_ID)
    {
        Initial_ID = Customer_ID;
        InitializeComponent();
        this.Customer_ID.Text = Customer_ID;
    }

    private void Delete_Customer_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        var data = new Db_Customer()
        {
            ID = Initial_ID,
        };
        var err = RW_Customer.Delete(new List<Db_Customer> { data },Paths.sqlite_path);
        this.Close();
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}