using System.Windows;
using System.Windows.Input;

namespace Page_Navigation_App.Popups;

public partial class Delete_Customer : Window
{
    public Delete_Customer(string Customer_ID)
    {
        InitializeComponent();
        this.Customer_ID.Text = Customer_ID;
    }

    private void Delete_Customer_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}