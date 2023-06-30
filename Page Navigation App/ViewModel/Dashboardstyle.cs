using System.Collections.Generic;
using System.Windows.Forms;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.ViewModel;

public class Dashboardstyle
{
    public string Orders { get; set; }
    public string FinishedOrderValue { get; set; }
    public string BillValue { get; set; }
    public string OpenTasks { get; set; }
    public string Customernumber { get; set; }
    public List<Db_Order> ReadyBills { get; set; }

    public Dashboardstyle()
    {
        //hier dann alle Daten aus Datenbank lesen die im Dashboard angezeigt werden sollen
        Orders = "FCB";
        BillValue = "gesamt";
        OpenTasks = "Anzahl aktiver Tasks";
        Customernumber = "Anzahl Kunden";
        //Todo überall wo ready Bills eine Listenanzeige integrieren
        (ReadyBills,var err) = RW_Order.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().ToString());
        }
    }

}