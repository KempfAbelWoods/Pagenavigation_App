﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        public Customers()
        {
            InitializeComponent();
            
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { ID = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Adress = "Bergstraße 26", Mail = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { ID = "2", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Adress = "Bergstraße 26", Mail = "john.doe@gmail.com", Phone = "415-954-1475" });
            membersDataGrid.ItemsSource = members;

        }
        
      /*  void EditCustomer(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(sender.GetHashCode());
        } */
        void EditCustomer(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(e.Parameter.ToString());
        }

    }

    public class Member
    {
        public string Character { get; set; }
        public Brush BgColor { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
    
}
