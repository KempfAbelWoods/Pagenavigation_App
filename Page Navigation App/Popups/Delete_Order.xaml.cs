﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Delete_Order : Window
{
    private string Initial_ID;
    public Delete_Order(string Order_ID)
    {
        Initial_ID = Order_ID;
        InitializeComponent();
        this.Order_ID.Text = Order_ID;
    }

    private void Delete_Order_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        if (CheckOrders())
        {
            var data = new Db_Order()
            {
                ID = Initial_ID,
            };
            var err = RW_Order.Delete(new List<Db_Order> { data },Paths.sqlite_path);
            if (err!=null)
            {
                MessageBox.Show(err.GetException().Message);
            }
            this.Close();
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private bool CheckOrders()
    {
        bool Deletable = true;

        var (list, err) = Rw_Tasks.ReadwithOrderID(Initial_ID, Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }

        if (list.Count>0)
        {
            Deletable = false;
            MessageBox.Show("Order can't be deleted, cause it has unfinished Tasks.");
        }
        
        return Deletable;
    }
}