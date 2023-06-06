using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Popups;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        ObservableCollection<Db_Customer> members = new ObservableCollection<Db_Customer>();
        ObservableCollection<Db_Customer> shownmembers = new ObservableCollection<Db_Customer>();
        private int SearchId = 1;
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
                      members.Add(new Db_Customer { ID = list[i].ID, Character = list[i].Character, BgColor = list[i].BgColor, Name = list[i].Name, Adress = list[i].Adress, Mail = list[i].Mail, Phone = list[i].Phone }); 
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

            var (list,err) = RW_Customer.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
            if (list.Count==1)
            {
                Edit_Customer editCustomer = new Edit_Customer(list[0].ID,list[0].Name,list[0].Adress,list[0].Mail,list[0].Phone);
                editCustomer.Owner = Application.Current.MainWindow;
                editCustomer.ShowDialog();
                Load_Data(true);
            }
        }
        
        void AddCustomer(object sender, RoutedEventArgs e)
        {
            
            Edit_Customer editCustomer = new Edit_Customer("","Max Mustermann","adress","max.mustermann@online.de","Phone Number");
            editCustomer.Owner = Application.Current.MainWindow;
            editCustomer.ShowDialog();
            Load_Data(true);

        }
        
        void DeleteCustomer(object sender, ExecutedRoutedEventArgs e)
        {
            //hier auch noch Kundennamen mitgeben
            Delete_Customer deleteCustomer = new Delete_Customer(e.Parameter.ToString());
            deleteCustomer.Owner = Application.Current.MainWindow;
            deleteCustomer.ShowDialog();
            Load_Data(true);

        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Db_Customer> tempMembers = new ObservableCollection<Db_Customer>();
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
                            if (x.Name.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 2:
                            if (x.Adress.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 3:
                            if (x.Mail.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 4:
                            if (x.Phone.Contains(textBoxFilter.Text))
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
