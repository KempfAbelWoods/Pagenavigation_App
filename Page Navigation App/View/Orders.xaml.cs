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
using Page_Navigation_App.Popups;
using Page_Navigation_App.ViewModel;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    //TODo es soll umschaltbar zwischen Finished ORders und offenen Orders sein
    public partial class Orders : UserControl
    {
        ObservableCollection<Db_Order> members = new ObservableCollection<Db_Order>();
        ObservableCollection<Db_Order> shownmembers = new ObservableCollection<Db_Order>();
        private int SearchId = 1;
        public Orders()
        {
            InitializeComponent();
            
            Load_Data(true);

        }

        /// <summary>
        /// Lade Daten aus Datenbank in Tabelle
        /// </summary>
        /// <param name="dbread"></param>
        /// Todo Ressourcen von Order mit DB_Ressources verknüpfen
        void Load_Data(bool dbread)
        { 
            var converter = new BrushConverter();
            members.Clear();
            
            var (list, err) = RW_Order.Read("",Paths.sqlite_path);

                for (int i = 0; i < list.Count; i++)
                {
                    members.Add(new Db_Order { ID= list[i].ID, Description = list[i].Description, RessourcesID = list[i].RessourcesID, Customer = list[i].Customer, EndDate = list[i].EndDate });
                }
           if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
            }

           ordersDataGrid.ItemsSource = shownmembers;
        }
        
        void EditOrder(object sender, ExecutedRoutedEventArgs e)
        {

            var (list,err) = RW_Order.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
            if (list.Count==1)
            {
                Edit_Order editOrder = new Edit_Order(list[0].ID,list[0].Customer,list[0].Description,list[0].RessourcesID,list[0].EndDate);
                editOrder.Owner = Application.Current.MainWindow;
                editOrder.ShowDialog();
                Load_Data(true);
            }
        }
        
        void AddOrder(object sender, RoutedEventArgs e)
        {
            
            Edit_Order editOrder = new Edit_Order("","Customer","Description","Ressources","Date");
            editOrder.Owner = Application.Current.MainWindow;
            editOrder.ShowDialog();
            Load_Data(true);

        }
        
        void DeleteOrder(object sender, ExecutedRoutedEventArgs e)
        {
            //hier auch noch Kundennamen mitgeben
            Delete_Order deleteOrder = new Delete_Order(e.Parameter.ToString());
            deleteOrder.Owner = Application.Current.MainWindow;
            deleteOrder.ShowDialog();
            Load_Data(true);

        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Db_Order> tempMembers = new ObservableCollection<Db_Order>();
            tempMembers.Clear();
            if (textBoxFilter.Text=="")
            {
                tempMembers = members;
            }
            else
            {
                foreach (var  x in members)
                {
                    switch (SearchId)
                    {
                        case 0:
                            if (x.ID.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 1:
                            if (x.Description.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 2:
                            if (x.RessourcesID.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 3:
                            if (x.EndDate.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 4:
                            if (x.Customer.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        
                    }
                }
            }
            shownmembers.Clear();
            shownmembers = tempMembers;
            
            Load_Data(false);
        }

        private void SearchFor_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchId = SearchFor.SelectedIndex;
        }
    }
    
}
