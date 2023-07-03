using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Finish_Order : Window
{
    public Finish_Order(string OrderID)
    {
        var (order, err) = RW_Order.ReadwithID(OrderID, Paths.sqlite_path);
        if (err!= null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        InitializeComponent();
        if (order.Count>0)
        { 
            Order.Text = "OrderID:  " +order[0].ID+"\n"+order[0].Description;
        }
    }
    
    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void FinishOrder(object sender, ExecutedRoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}