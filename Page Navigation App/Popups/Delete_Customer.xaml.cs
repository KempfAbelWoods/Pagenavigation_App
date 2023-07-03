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
        if (CheckCustomers())
        {
            var data = new Db_Customer()
            {
                ID = Initial_ID,
            };
            var err = RW_Customer.Delete(new List<Db_Customer> { data }, Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            this.Close();
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
    
    private bool CheckCustomers()
    {
        bool Deletable = true;

        var (data, error) = RW_Customer.ReadwithID(Initial_ID, Paths.sqlite_path);
        if (error!=null)
        {
            MessageBox.Show(error.GetException().Message);
        }

        string ID = data[0].ID;

        var (list, err) = RW_Order.ReadwithCustomer(ID, Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }

        if (list.Count>0)
        {
            Deletable = false;
            MessageBox.Show("Customer can't be deleted, cause it has unfinished Orders.");
        }
        
        return Deletable;
    }
}