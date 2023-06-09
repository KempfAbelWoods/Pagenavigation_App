using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.Popups;

public partial class Edit_Order : Window
{
    private string Initial_ID;
    
    public Edit_Order(string Order_ID, string kunde, string beschreibung, string ressourcen, string date)
    {
        Initial_ID = Order_ID;
        InitializeComponent();
        ID_Field.Text = Order_ID;
        Kunde_Field.Text = kunde;
        Beschreibung_Field.Text = beschreibung;
        Ressourcen_Field.Text = ressourcen;
        Enddatum_Field.Text = date;
    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string kunde = Kunde_Field.Text;
        string beschreibung = Beschreibung_Field.Text;
        string ressourcen = Ressourcen_Field.Text;
        string date = Enddatum_Field.Text;
        
        if (ID!="" && beschreibung!="" && kunde !="" && ressourcen !="" && date!= "")
        {
            var data = new Db_Order
            {
                ID = ID,
                Description = beschreibung,
                Customer = kunde,
                Ressources = ressourcen,
                EndDate = date,
            };
            //Spalte mit alten Daten löschen
            var (list, err1) = RW_Order.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err1 == null)
            {
                var error = RW_Order.Delete(list,Paths.sqlite_path);
            }
            else
            {
                MessageBox.Show(err1.GetException().Message);
            }

            //neue Spalte einfügen
            var err = RW_Order.Write(new List<Db_Order> { data }, Paths.sqlite_path);
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

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void ID_Field_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string ID = ID_Field.Text;
        
        if (ID != "" && (Initial_ID == "" || ID != Initial_ID))
        {
            //check if ID is already in Database
            var (data,err) =RW_Order.ReadwithID(ID, Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
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