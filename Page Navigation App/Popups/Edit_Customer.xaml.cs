using System.Text.RegularExpressions;
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
        this.Close();
    }
    
    void Close_Window( object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
    
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}