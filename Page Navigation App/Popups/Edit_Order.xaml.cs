using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

    private void ID_Field_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Save_and_Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        string ID = ID_Field.Text;
        string kunde = Kunde_Field.Text;
        string beschreibung = Beschreibung_Field.Text;
        string ressourcen = Ressourcen_Field.Text;
        string date = Enddatum_Field.Text;
        
        if (ID!="" && kunde!="" && beschreibung !="" && ressourcen !="" && date!= "")
        {
            //hier abspeichern der Daten
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
        //hier wird geprüft ob die ID in der Datenbank schon vergeben ist.
        //wobei die ID eventuell nicht änderbar gemacht werden sollte ??
        //nur abfragen bei neuerstellung eines Kundens?
        string ID = ID_Field.Text;
        
        if (Initial_ID != ID && ID != "")
        {
            MessageBox.Show("hallo du hast den Text geändert"); 
        }
    }
}