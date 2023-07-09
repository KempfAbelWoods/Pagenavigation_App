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

        ObservableCollection<Db_FinishedOrders> members_finished = new ObservableCollection<Db_FinishedOrders>();
        ObservableCollection<Db_FinishedOrders> shownmembers_finished = new ObservableCollection<Db_FinishedOrders>();
        private int SearchId = 1;
        private bool showfinish = false;

        public Orders()
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
            members.Clear();

            var (list, err) = RW_Order.Read("", Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            for (int i = 0; i < list.Count; i++)
            {
                members.Add(new Db_Order
                {
                    ID = list[i].ID, Description = list[i].Description, CustomerID = list[i].CustomerID,
                    EndDate = list[i].EndDate, ActualCosts = list[i].ActualCosts, OrderValue = list[i].OrderValue
                });
            }

            if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
            }

            ordersDataGrid.ItemsSource = shownmembers;
        }

        void Load_Data_finished(bool dbread)
        {
            members_finished.Clear();

            var (list, err) = Rw_FinishedOrders.Read("", Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            for (int i = 0; i < list.Count; i++)
            {
                members_finished.Add(new Db_FinishedOrders()
                {
                    ID = list[i].ID, Description = list[i].Description, CustomerID = list[i].CustomerID,
                    EndDate = list[i].EndDate, OrderValue = list[i].OrderValue
                });
            }

            if (dbread)
            {
                shownmembers_finished = members_finished;
                textBoxFilter.Text = "";
            }

            ordersDataGrid.ItemsSource = shownmembers_finished;
        }

        void EditOrder(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                if (showfinish == false)
                {
                    var (list, err) = RW_Order.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
                    if (list.Count == 1)
                    {
                        Edit_Order editOrder = new Edit_Order(list[0].ID, list[0].CustomerID, list[0].Description,
                            list[0].EndDate, list[0].OrderValue, list[0].ActualCosts, false);
                        editOrder.Owner = Application.Current.MainWindow;
                        editOrder.ShowDialog();
                        Load_Data(true);
                    }
                }
            }
        }

        void AddOrder(object sender, RoutedEventArgs e)
        {
            if (showfinish == false)
            {
                if (Userhandling.GrantPermission(1, true))
                {
                    Edit_Order editOrder = new Edit_Order("", "Customer", "Description", "Date", 0, 0, true);
                    editOrder.Owner = Application.Current.MainWindow;
                    editOrder.ShowDialog();
                    Load_Data(true);
                }
            }
        }

        void DeleteOrder(object sender, ExecutedRoutedEventArgs e)
        {
            if (showfinish == false)
            {
                if (Userhandling.GrantPermission(1, true))
                {
                    //hier auch noch Kundennamen mitgeben
                    Delete_Order deleteOrder = new Delete_Order(e.Parameter.ToString());
                    deleteOrder.Owner = Application.Current.MainWindow;
                    deleteOrder.ShowDialog();
                    Load_Data(true);
                }
            }
        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //Todo Suche ermöglichen bei finished orders
            ObservableCollection<Db_Order> tempMembers = new ObservableCollection<Db_Order>();
            tempMembers.Clear();
            if (textBoxFilter.Text == "")
            {
                tempMembers = members;
            }
            else
            {
                foreach (var x in members)
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
                            if (x.CustomerID.Contains(textBoxFilter.Text))
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

        private void FinishOrder(object sender, ExecutedRoutedEventArgs e)
        {
            if (showfinish == false)
            {
                if (Userhandling.GrantPermission(1, true))
                {
                    Finish_Order finishOrder = new Finish_Order(e.Parameter.ToString());
                    finishOrder.Owner = Application.Current.MainWindow;
                    finishOrder.ShowDialog();
                    Load_Data(true);
                }
            }
        }

        private void View_Finished_Orders(object sender, RoutedEventArgs e)
        {
            if (showfinish)
            {
                Load_Data(true);
                textBoxFilter.IsEnabled = true;
                ViewOrders.Text = "View Finished Orders";
                showfinish = false;
            }
            else
            {
                Load_Data_finished(true);
                //todo wenn suche funktioniert, die disable wieder rausmachen
                textBoxFilter.IsEnabled = false;
                ViewOrders.Text = "View actual Orders";
                showfinish = true;
            }
        }
    }
}