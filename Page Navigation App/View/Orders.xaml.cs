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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        ObservableCollection<Member1> members = new ObservableCollection<Member1>();
        ObservableCollection<Member1> shownmembers = new ObservableCollection<Member1>();
        public Orders()
        {
            InitializeComponent();
        }
        
        void Load_Data(bool dbread)
        { 
            var converter = new BrushConverter();
            members.Clear();
            //hier aus Datenbank lesen
            for (int i = 0; i < 5; i++) 
            {
                members.Add(new Member1 { ID = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Kunde = "John Doe", Description = "Baum fällen", Ressources = "Kettensäge", EndDate = "23.12.23" }); 
                members.Add(new Member1 { ID = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Kunde = "FCB", Description = "Mähen", Ressources = "Rasenmäher", EndDate = "31.12.23" });
            }
            if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
            }

            ordersDataGrid.ItemsSource = shownmembers;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            //hier anhand von Parameter Daten des Kunden auslesen und als Parameter mitgeben
            Edit_Order editCustomer = new Edit_Order("1","Max Mustermann","Rasen mähen","Aufsitzmäher","31.12.2023");
            editCustomer.Owner = Application.Current.MainWindow;
            editCustomer.ShowDialog();
            //hier dann Daten abspeichern
        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            {
                ObservableCollection<Member1> tempMembers = new ObservableCollection<Member1>();
                tempMembers.Clear();
                if (textBoxFilter.Text=="")
                {
                    tempMembers = members;
                }
                else
                {
                    foreach (var  x in members)
                    {
                        if (x.Kunde.Contains(textBoxFilter.Text))
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

        private void EditOrder(object sender, ExecutedRoutedEventArgs e)
        {
            //hier anhand von Parameter Daten des Kunden auslesen und als Parameter mitgeben
            Edit_Order editOrder = new Edit_Order(e.Parameter.ToString(),"Kunde","Beschreibung","Ressourcen","Enddatum");
            editOrder.Owner = Application.Current.MainWindow;
            editOrder.ShowDialog();
            //hier dann Daten neu laden in Tabelle, da aktualisiert
        }

        private void DeleteOrder(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Member1
    {
        public string Character { get; set; }
        public Brush BgColor { get; set; }
        public string ID { get; set; }
        public string Kunde { get; set; }
        public string Description { get; set; }
        public string Ressources { get; set; }
        public string EndDate { get; set; }
    }
}
