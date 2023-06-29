using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using Page_Navigation_App.Popups;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Tasks : UserControl
    {
        
        ObservableCollection<Db_Tasks> tasks = new ObservableCollection<Db_Tasks>();
        ObservableCollection<Db_Tasks> showntasks = new ObservableCollection<Db_Tasks>();
        private int SearchId = 1;
        public Tasks()
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
            tasks.Clear();
            
            var (list, err) = Rw_Tasks.Read("",Paths.sqlite_path);

                for (int i = 0; i < list.Count; i++)
                {
                    tasks.Add(new Db_Tasks { ID= list[i].ID, Description = list[i].Description, orderID = list[i].orderID, Username = list[i].Username, EstimatedHours = list[i].EstimatedHours, ActualHours = list[i].ActualHours, Costs = list[i].Costs});
                }
           if (dbread)
            {
                showntasks = tasks;
                textBoxFilter.Text = "";
            }

           TasksDataGrid.ItemsSource = showntasks;
        }
        
        void EditTasks(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                var (list, err) = Rw_Tasks.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
                if (list.Count == 1)
                {
                    Edit_Tasks editTasks = new Edit_Tasks(list[0].ID, list[0].orderID, list[0].Description,
                        list[0].Username, list[0].EstimatedHours, list[0].ActualHours, list[0].Costs);
                    editTasks.Owner = Application.Current.MainWindow;
                    editTasks.ShowDialog();
                    Load_Data(true);
                }
            }
        }
        
        void AddTasks(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                Edit_Tasks editTasks = new Edit_Tasks("", "", "Description", "User", 1,1,1);
                editTasks.Owner = Application.Current.MainWindow;
                editTasks.ShowDialog();
                Load_Data(true);
            }
        }
        
        void DeleteTasks(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(1, true))
            {
                //hier auch noch Kundennamen mitgeben
                Delete_Tasks deleteTasks = new Delete_Tasks(e.Parameter.ToString());
                deleteTasks.Owner = Application.Current.MainWindow;
                deleteTasks.ShowDialog();
                Load_Data(true);
            }
        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Db_Tasks> tempMembers = new ObservableCollection<Db_Tasks>();
            tempMembers.Clear();
            if (textBoxFilter.Text=="")
            {
                tempMembers = tasks;
            }
            else
            {
                foreach (var  x in tasks)
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
                            if (x.orderID.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 3:
                            if (x.Username.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;

                    }
                }
            }
            showntasks.Clear();
            showntasks = tempMembers;
            
            Load_Data(false);
        }

        private void SearchFor_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchId = SearchFor.SelectedIndex;
        }
        
    }
}
