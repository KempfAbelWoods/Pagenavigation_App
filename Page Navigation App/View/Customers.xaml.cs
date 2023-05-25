using System;
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
using System.Globalization;
using System.Runtime.InteropServices.ObjectiveC;
using Page_Navigation_App.Popups;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        ObservableCollection<Member> members = new ObservableCollection<Member>();
        ObservableCollection<Member> shownmembers = new ObservableCollection<Member>();
        public Customers()
        {
            InitializeComponent();
            
            Load_Data(true);

        }

        void Load_Data(bool dbread)
        { 
            var converter = new BrushConverter();
            members.Clear();
                //hier aus Datenbank lesen
                for (int i = 0; i < 5; i++) 
                {
                      members.Add(new Member { ID = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Adress = "Bergstraße 26", Mail = "john.doe@gmail.com", Phone = "415-954-1475" }); 
                      members.Add(new Member { ID = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "FCB", Adress = "Bergstraße 26", Mail = "john.doe@gmail.com", Phone = "415-954-1475" });
                }
           if (dbread)
            {
                shownmembers = members;
            }

            membersDataGrid.ItemsSource = shownmembers;
        }
        void EditCustomer(object sender, ExecutedRoutedEventArgs e)
        {

           //hier anhand von Parameter Daten des Kunden auslesen und als Parameter mitgeben
           Edit_Customer editCustomer = new Edit_Customer(e.Parameter.ToString(),"Name","adress","Mail","Phone");
           editCustomer.Owner = Application.Current.MainWindow;
           editCustomer.ShowDialog();
           //hier dann Daten neu laden in Tabelle, da aktualisiert

        }
        
        void AddCustomer(object sender, RoutedEventArgs e)
        {

            //hier anhand von Parameter Daten des Kunden auslesen und als Parameter mitgeben
            Edit_Customer editCustomer = new Edit_Customer("1","Max Mustermann","adress","max.mustermann@online.de","Phone Number");
            editCustomer.Owner = Application.Current.MainWindow;
            editCustomer.ShowDialog();
            //hier dann Daten neu laden in Tabelle, da aktualisiert

        }
        
        void DeleteCustomer(object sender, ExecutedRoutedEventArgs e)
        {
            //hier auch noch Kundennamen mitgeben
            Delete_Customer deleteCustomer = new Delete_Customer(e.Parameter.ToString());
            deleteCustomer.Owner = Application.Current.MainWindow;
            deleteCustomer.ShowDialog();
            //hier dann Daten neu laden in Tabelle, da aktualisiert

        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Member> tempMembers = new ObservableCollection<Member>();
            tempMembers.Clear();
            if (textBoxFilter.Text=="")
            {
                tempMembers = members;
            }
            else
            {
                foreach (var  x in members)
                {
                    if (x.Name.Contains(textBoxFilter.Text))
                    {
                        tempMembers.Add(x);
                    }
                }
            }
            shownmembers.Clear();
            shownmembers = tempMembers;
            
            Load_Data(false);
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
