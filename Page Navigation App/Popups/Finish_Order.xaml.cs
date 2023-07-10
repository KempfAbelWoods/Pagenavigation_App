using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;
using Page_Navigation_App.Utilities;
using PdfSharp.Pdf;

namespace Page_Navigation_App.Popups;

public partial class Finish_Order : Window
{
    private string InitialID;
    private string OrderName;

    public Finish_Order(string OrderID)
    {
        var (order, err) = RW_Order.ReadwithID(OrderID, Paths.sqlite_path);
        if (err != null)
        {
            MessageBox.Show(err.GetException().Message);
        }

        InitialID = OrderID;
        InitializeComponent();
        if (order.Count > 0)
        {
            OrderName = order[0].Description;
            Order.Text = "OrderID:  " + order[0].ID + "\n" + order[0].Description;
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private void FinishOrder(object sender, ExecutedRoutedEventArgs e)
    {
        if (Date.SelectedDate.HasValue)
        {
            var (Tasks, err) = Rw_Tasks.ReadwithOrderID(InitialID, Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            if (Tasks.Count > 0)
            {
                var pdf = new PDF_Generator().PDF_Generate(OrderName, "", InitialID, Date.SelectedDate.ToString());

                //Order Auslesen
                var (order, err1) = RW_Order.ReadwithID(InitialID, Paths.sqlite_path);
                if (err1 != null)
                {
                    MessageBox.Show(err1.GetException().Message);
                }


                if (order.Count > 0)
                {
                    var data = new Db_FinishedOrders()
                    {
                        ID = order[0].ID,
                        Description = order[0].Description,
                        CustomerID = order[0].CustomerID,
                        //todo zeit rauswerfen ToString("MMMM dd, yyyy")
                        EndDate = DateTime.Now.ToString("dd.MM.yyyy"),
                        OrderValue = order[0].ActualCosts,
                        PDf = pdf
                    };
                    //Spalte mit alten Daten löschen
                    var (list, err2) = Rw_Tasks.ReadwithOrderID(InitialID, Paths.sqlite_path);
                    if (err2 == null)
                    {
                        var error = Rw_Tasks.Delete(list, Paths.sqlite_path);
                        if (error != null)
                        {
                            MessageBox.Show(error.GetException().Message);
                        }

                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show(err2.GetException().Message);
                    }

                    //Spalte in neue FinishedOrderDb schreiben
                    var error1 = Rw_FinishedOrders.Write(new List<Db_FinishedOrders> { data }, Paths.sqlite_path);
                    if (error1 != null)
                    {
                        MessageBox.Show(error1.GetException().Message);
                    }
                    //Order löschen
                    var err3 = RW_Order.Delete(order, Paths.sqlite_path);
                    if (err3!=null)
                    {
                        MessageBox.Show(err3.GetException().Message);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("There are no Tasks for this Order.");
            }

            this.Close();
        }
        else
        {
            MessageBox.Show("Please specify a delivery Date.");
        }
    }
}