using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Printing.Interop;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.ViewModel;

public class ChartData
{
    public int month { get; set; }
    public int year { get; set; }
    public float costs { get; set; }
    public string Customer { get; set; }
}
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
        Orders = "Anzahl: " + GetOrders();
        BillValue = "gesamt";
        OpenTasks = "Anzahl: " + GetTasks();
        Customernumber = "Anzahl: " + GetCustomers();
        (ReadyBills, var err) = RW_Order.Read("", Paths.sqlite_path);
        if (err != null)
        {
           Trace.WriteLine(err.GetException().ToString());
        }

        PrintChart();
    }


    public void PrintChart()
    {
        var data = Getchartdata();
        ChartValues<double> chartvalue = new ChartValues<double>();
        int actualmonth = DateTime.Now.Month;
        int actualyear = DateTime.Now.Year;
        Labels = new[] { "default", "default", "default", "default" };
        for (int i = 4; i > 0; i--)
        {
            
            Labels[i - 1] = "0" + actualmonth.ToString() + "/" + actualyear.ToString();
            float costs = 0;
            foreach (var member in data)
            {
                if (actualmonth == member.month && actualyear==member.year)
                {
                    costs += member.costs;
                    //todo verbesserungen, passt ned vom auslesen her
                }
            }
            chartvalue.Add(costs);
            if (actualmonth-i<=0)
            {
                actualyear--;
                
                actualmonth = 12 +(actualmonth-i);
            }
            else
            {
                actualmonth--;
            }
        }
        
        ChartValues<double> charts = new ChartValues<double>();
        if (chartvalue.Count==4)
        {
            for (int i = 0; i < 4; i++)
            {
                charts.Add(chartvalue[3-i]);
            }
        }
        SeriesCollection = new SeriesCollection()
        {
            new StackedColumnSeries
            {
                Values = charts,
                StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                DataLabels = true,
                
            }
        };
        Formatter = value => value + "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<ChartData> Getchartdata()
    {
        List<ChartData> charts = new List<ChartData>();
        var (list, err) = Rw_FinishedOrders.Read("", Paths.sqlite_path);
        if (err != null)
        {
            Trace.WriteLine(err.GetException().ToString());
        }
        try
        {
            charts.Clear();
            foreach (var chart in list)
            {
                var data = new ChartData()
                {
                    costs = chart.OrderValue,
                    Customer = chart.CustomerID,
                    year = DateTime.Parse(chart.EndDate).Year,
                    month = DateTime.Parse(chart.EndDate).Month
                };
                charts.Add(data);
            }
        }
        catch (Exception e)
        {
            Trace.WriteLine(e.Message);
        }
        return (charts);
    }

    public string GetCustomers()
    {
        string Customers = "0";
        int Anzahl = 0;

        var (list, err) = RW_Customer.Read("", Paths.sqlite_path);
        if (err != null)
        {
            Trace.WriteLine(err.GetException().Message);
        }

        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i + 1;
            Customers = Anzahl.ToString();
        }

        return Customers;
    }

    public string GetTasks()
    {
        string Tasks = "0";

        int Anzahl = 0;

        var (list, err) = Rw_Tasks.Read("", Paths.sqlite_path);
        if (err != null)
        {
            Trace.WriteLine(err.GetException().Message);
        }

        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i + 1;
            Tasks = Anzahl.ToString();
        }

        return Tasks;
    }

    public string GetOrders()
    {
        string Orders = "0";

        int Anzahl = 0;

        var (list, err) = RW_Order.Read("", Paths.sqlite_path);
        if (err != null)
        {
            Trace.WriteLine(err.GetException().Message);
        }

        for (var i = 0; i < list.Count; i++)
        {
            Anzahl = i + 1;
            Orders = Anzahl.ToString();
        }

        return Orders;
    }
}