namespace Page_Navigation_App.ViewModel;

public class Dashboardstyle
{
    public string Orders { get; set; }

    public Dashboardstyle()
    {
        //hier dann alle Daten aus Datenbank lesen die im Dashboard angezeigt werden sollen
        Orders = "FCB";
    }

}