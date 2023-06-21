using System.Windows;

namespace Page_Navigation_App.Popups;

public partial class Edit_User : Window
{
    public Edit_User(string ID, string name, string username, string role, string rights, string password)
    {
        //Todo verwaltung von Rechten evtl. als string mit Trennzeichen übergeben
        //Todo überprüfen ob Nutzername schon vergeben
        InitializeComponent();
    }
}