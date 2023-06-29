using System.Windows;

namespace Page_Navigation_App.Popups;

public partial class Edit_Tasks : Window
{
    public Edit_Tasks(string ID, string orderID, string Description, string Username, float EstimatedHours, float ActualHours, float Costs)
    {
        InitializeComponent();
    }
}