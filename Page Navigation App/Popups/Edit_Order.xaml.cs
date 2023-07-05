using System;
using System.Collections.Generic;
using System.Globalization;
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
    
    public Edit_Order(string Order_ID, string kunde, string beschreibung, string date, float OrderValue, float ActualCosts, bool IDenable)
    {
        Initial_ID = Order_ID;
        InitializeComponent();
        if (!IDenable)
        { ID_Field.IsReadOnly = true; }
        else
        { ID_Field.IsReadOnly = false; }
        ID_Field.Text = Order_ID;
        Loaddata(kunde);
        Beschreibung_Field.Text = beschreibung;
        Enddatum_Field.Text = date;
        OrderValue_Field.Text = OrderValue.ToString();
        ActualCosts_Field.Text = ActualCosts.ToString();
    }
    
    private void Loaddata(string Kunde)
    {
        //alle Daten aus DB laden
        var (Customers, error) = RW_Customer.Read("", Paths.sqlite_path);
        if (error!=null)
        {
            MessageBox.Show(error.GetException().Message);
        }

        //Daten in Combobox laden
        //Todo hier noch Details einblenden
        foreach (var customer in Customers)
        {
            Kunde_Field.Items.Add(customer.ID);
        }
        Kunde_Field.SelectedItem = Kunde;

    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string kunde = Kunde_Field.Text;
        string beschreibung = Beschreibung_Field.Text;
        string date = Enddatum_Field.SelectedDate.ToString();
        float OrderValue = 0f;
        float ActualCosts = 0f;
        
        try
        {
            OrderValue = float.Parse(OrderValue_Field.Text,CultureInfo.InvariantCulture.NumberFormat);
            ActualCosts = float.Parse(ActualCosts_Field.Text,CultureInfo.InvariantCulture.NumberFormat);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
        
        if (ID!="" && beschreibung!="" && kunde !="" && date!= "")
        {
            var data = new Db_Order
            {
                ID = ID,
                Description = beschreibung,
                CustomerID = kunde,
                EndDate = date,
                OrderValue = OrderValue,
                ActualCosts = ActualCosts
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
    
    private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        // TODO irgendwie die scheiß validierung machen
        Regex regex = new Regex(@"^\d*\.?\d*$"); //^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$
        e.Handled = !regex.IsMatch(e.Text);
    }
}