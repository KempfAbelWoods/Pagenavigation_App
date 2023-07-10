using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Edit_Tasks : Window
{
    private string Initial_ID;
    private List<string>  _ordernames = new List<string>();
    private List<string> _ressourcedescriptions  = new List<string>();
    public Edit_Tasks(string ID, string orderID, string Description, string Username, float EstimatedHours, float ActualHours, float Costs, string Ressource, bool IDenable)
    {
        Initial_ID = ID;
        InitializeComponent();
        if (!IDenable)
        {
            ID_Field.IsReadOnly = true;
            Costs_Field.IsReadOnly = true;
        }
        else
        { ID_Field.IsReadOnly = false; }
        Loaddata(Username,orderID,Ressource);
        ID_Field.Text = ID;
        //Order_Field.Text = orderID;
        Description_Field.Text = Description;
        //User_Field.Text = Username;
        Costs_Field.Text = Costs.ToString();
        Estimate_Field.Text = EstimatedHours.ToString();
        Actual_Field.Text = ActualHours.ToString();
    }
    
    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Loaddata(string username, string orderId, string Ressource)
    {
        //alle Daten aus DB laden
        var (orders, error) = RW_Order.Read("", Paths.sqlite_path);
        if (error!=null)
        {
            MessageBox.Show(error.GetException().Message);
        }
        var (users, err) = Rw_Users.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        var (ressources, err1) = Rw_Ressources.Read("", Paths.sqlite_path);
        if (err1!=null)
        {
            MessageBox.Show(err1.GetException().Message);
        }
        //Daten in Combobox laden
        //Todo hier noch Details einblenden
        foreach (var order in orders)
        {
            Order_Field.Items.Add(order.ID);
            _ordernames.Add(order.Description);
        }
        foreach (var user in users)
        {
            User_Field.Items.Add(user.Username);
        }
        foreach (var ressource in ressources)
        {
            Ressource_Field.Items.Add(ressource.ID);
            _ressourcedescriptions.Add(ressource.Name);
        }

        Ressource_Field.SelectedItem = Ressource;
        User_Field.SelectedItem = username;
        Order_Field.SelectedItem = orderId;

    }
    
    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string orderId = Order_Field.Text;
        string username = User_Field.Text;
        string description = Description_Field.Text;
        string ressource = Ressource_Field.Text;
        float actualhours = 0f;
        float estimatehours = 0f;
        float costs = 0f;
        try
        {
            actualhours = float.Parse(Actual_Field.Text,CultureInfo.InvariantCulture.NumberFormat);
            estimatehours = float.Parse(Estimate_Field.Text,CultureInfo.InvariantCulture.NumberFormat);
            costs = float.Parse(Costs_Field.Text,CultureInfo.InvariantCulture.NumberFormat);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
        if (ID!="" && orderId!="" && username !="" && actualhours !=0 && description!= "" && costs!=0 && estimatehours!=0 && ressource!="")
        {
            var data = new Db_Tasks()
            {
                ID = ID,
                OrderId = orderId,
                Username = username,
                ActualHours = actualhours,
                EstimatedHours = estimatehours,
                Costs = costs,
                Ressource = ressource,
                Description = description
            };
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Tasks.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err1 == null)
            {
                var error = Rw_Tasks.Delete(list,Paths.sqlite_path);
            }
            else
            {
                MessageBox.Show(err1.GetException().Message);
            }

            //neue Spalte einfügen
            var err = Rw_Tasks.Write(new List<Db_Tasks> { data }, Paths.sqlite_path);
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
    private void ID_Field_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string ID = ID_Field.Text;
        
        if (ID != "" && (Initial_ID == "" || ID != Initial_ID))
        {
            //check if ID is already in Database
            var (data,err) =Rw_Tasks.ReadwithID(ID, Paths.sqlite_path);
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

    private void Selection_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_ordernames.Count>=Order_Field.SelectedIndex && Order_Field.SelectedIndex>=0)
        {
            Order_Text.Text = _ordernames[Order_Field.SelectedIndex];
        }

        if (_ressourcedescriptions.Count>=Ressource_Field.SelectedIndex && Ressource_Field.SelectedIndex>=0)
        {
            Ressource_Text.Text = _ressourcedescriptions[Ressource_Field.SelectedIndex];
        }
        

    }
}
public class Comboboxobject
{
    public String ID { get; set; } = "";
    public String Name { get; set; } = "";
}