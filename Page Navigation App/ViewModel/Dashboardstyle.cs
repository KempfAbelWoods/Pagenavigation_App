using System.Collections.Generic;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.ViewModel;

public class Dashboardstyle
{
    public string Orders { get; set; }
    public string FinishedOrderValue { get; set; }
    public string OpenTasks { get; set; }
    public string Customernubmer { get; set; }
    public List<Db_Order> ReadyBills { get; set; }

    public Dashboardstyle()
    {
        //hier dann alle Daten aus Datenbank lesen die im Dashboard angezeigt werden sollen
        Orders = "FCB";
        FinishedOrderValue = "x";
    }

}