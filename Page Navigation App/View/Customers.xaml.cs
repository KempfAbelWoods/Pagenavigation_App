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
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;
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

        /// <summary>
        /// Lade Daten aus Datenbank in Tabelle
        /// </summary>
        /// <param name="dbread"></param>
        void Load_Data(bool dbread)
        { 
            var converter = new BrushConverter();
            members.Clear();
            
            var (list, err) = RW_Customer.Read("",Paths.sqlite_path);

                for (int i = 0; i < list.Count; i++)
                {
                    var color =(Brush)converter.ConvertFromString(list[i].BgColor);
                      members.Add(new Member { ID = list[i].ID, Character = list[i].Character, BgColor = list[i].BgColor, Name = list[i].Name, Adress = list[i].Adress, Mail = list[i].Mail, Phone = list[i].Phone }); 
                }
           if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
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
            
            Edit_Customer editCustomer = new Edit_Customer("1","Max Mustermann","adress","max.mustermann@online.de","Phone Number");
            editCustomer.Owner = Application.Current.MainWindow;
            editCustomer.ShowDialog();

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
        public string BgColor { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
    


}
