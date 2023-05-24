using System.Windows;
using System.Windows.Input;

namespace Page_Navigation_App.Popups;

public partial class Edit_Customer : Window
{
    public Edit_Customer()
    {
        InitializeComponent();
    }

    void Save_and_Close_Window( object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("save");
        this.Close();
    }
    
    void Close_Window( object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("cancel");
        this.Close();
    }
}