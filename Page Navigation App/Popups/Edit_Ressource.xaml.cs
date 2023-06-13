using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Edit_Ressource : Window
{
    private string Initial_ID;
    
    public Edit_Ressource()
    {
        InitializeComponent();
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ID_Field_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string ID = ID_Field.Text;
        
        if (ID != "" && (Initial_ID == "" || ID != Initial_ID))
        {
            //check if ID is already in Database
            var (data,err) =Rw_Ressources.ReadwithID(ID, Paths.sqlite_path);
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