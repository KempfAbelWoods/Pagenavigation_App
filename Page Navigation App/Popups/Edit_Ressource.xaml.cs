using System.Collections.Generic;
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
    
    public Edit_Ressource(string Ressource_ID, string Description, string Name, string Ressourcetype, float costs, bool Costtype)
    {
        Initial_ID = Ressource_ID;
        InitializeComponent();
        ID_Field.Text = Ressource_ID;
        Name_Field.Text = Name;
        Description_Field.Text = Description;
        Ressourcetype_Field.Text = Ressourcetype;
        Costs_Field.Text = costs.ToString();
        
        if (Costtype) 
        { Costtype_Field.SelectedIndex = 1; }
        else { Costtype_Field.SelectedIndex = 0; }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string name = Name_Field.Text;
        float costs = float.Parse(Costs_Field.Text);
        bool costtype;
        if (Costtype_Field.SelectedIndex == 1)
        { costtype = true; }
        else
        { costtype = false; }
        string ressourcetype = Ressourcetype_Field.Text;
        string description = Description_Field.Text;
        if (ID!="" && name!="" && name !="" && ressourcetype !="" && description!= "")
        {
            var data = new Db_Ressources()
            {
                ID = ID,
                Name = name,
                Costs = costs,
                Costtype = costtype,
                Ressourcetype = ressourcetype,
                Description = description
            };
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Ressources.ReadwithID(Initial_ID, Paths.sqlite_path);
            if (err1 == null)
            {
                var error = Rw_Ressources.Delete(list,Paths.sqlite_path);
            }
            else
            {
                MessageBox.Show(err1.GetException().Message);
            }

            //neue Spalte einfügen
            var err = Rw_Ressources.Write(new List<Db_Ressources> { data }, Paths.sqlite_path);
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
    
    private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        // TODO irgendwie die scheiß validierung machen
        Regex regex = new Regex(@"[-+]?[0-9]*\.?[0-9]+");
        e.Handled = !regex.IsMatch(e.Text);
    }
}