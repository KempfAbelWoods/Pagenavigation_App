using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
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
    public SeriesCollection SeriesCollection { get; set; }
    public string[] Labels { get; set; }
    public Func<double, string> Formatter { get; set; }

    public Dashboardstyle()
    {
        //hier dann alle Daten aus Datenbank lesen die im Dashboard angezeigt werden sollen
        Orders = "Anzahl: "+GetOrders();
        BillValue = "gesamt";
        OpenTasks = "Anzahl: "+GetTasks();
        Customernumber = "Anzahl: "+GetCustomers();
        (ReadyBills,var err) = RW_Order.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().ToString());
        }
        
        SeriesCollection = new SeriesCollection()
        {               
            new StackedColumnSeries
            {
                Values = new ChartValues<double> {4, 5, 6, 8},
                StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                DataLabels = true
            },
            new StackedColumnSeries
            {
                Values = new ChartValues<double> {2, 5, 6, 7},
                StackMode = StackMode.Values,
                DataLabels = true
            }

        };
        //adding series updates and animates the chart
        SeriesCollection.Add(new StackedColumnSeries
        {
            Values = new ChartValues<double> {6, 2, 7},
            StackMode = StackMode.Values,
            DataLabels = true
        });
        
        //adding values also updates and animates
        SeriesCollection[2].Values.Add(4d);
 
        Labels = new[] {"Chrome", "Mozilla", "Opera", "IE"};
        Formatter = value => value + " Mill";

    }
    

    public string GetCustomers()
    {
        string Customers = "0";
        int Anzahl = 0;

        var (list, err) = RW_Customer.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i+1;
            Customers = Anzahl.ToString();
        }
        
        return Customers;
    }

    public string GetTasks()
    {
        string Tasks = "0";
        
        int Anzahl = 0;

        var (list, err) = Rw_Tasks.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i+1;
            Tasks = Anzahl.ToString();
        }
        return Tasks;
    }
    
    public string GetOrders()
    {
        string Orders = "0";
        
        int Anzahl = 0;

        var (list, err) = RW_Order.Read("", Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i+1;
            Orders = Anzahl.ToString();
        }
        return Orders;
    }
}