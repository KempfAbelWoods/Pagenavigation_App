using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class User_Control : Window
{
    ObservableCollection<Db_Users> members = new ObservableCollection<Db_Users>();
    ObservableCollection<Db_Users> shownmembers = new ObservableCollection<Db_Users>();
    private int SearchId = 1;
    public User_Control()
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
            
            var (list, err) = Rw_Users.Read("",Paths.sqlite_path);

                for (int i = 0; i < list.Count; i++)
                {
                    members.Add(new Db_Users { ID = list[i].ID, Name = list[i].Name, Username = list[i].Username, Role = list[i].Role, Rights = list[i].Rights, Password = list[i].Password}); 
                }
           if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
            }

           membersDataGrid.ItemsSource = shownmembers;
        }
        
        void EditUser(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(2, true))
            {
                var (list, err) = Rw_Users.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
                if (list.Count == 1)
                {
                    Edit_User editUser = new Edit_User(list[0].ID, list[0].Name, list[0].Username, list[0].Role,
                        list[0].Rights, list[0].Password, false);
                    editUser.Owner = Application.Current.MainWindow;
                    editUser.ShowDialog();
                    Load_Data(true);
                }
            }
        }
        
        void AddUser(object sender, RoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(2, true))
            {
                Edit_User editUser = new Edit_User("", "Max Mustermann", "mmustermann", "Administrator", "readonly", "",
                    true);
                editUser.Owner = Application.Current.MainWindow;
                editUser.ShowDialog();
                Load_Data(true);
            }
        }
        
        void DeleteUser(object sender, ExecutedRoutedEventArgs e)
        {
            if (Userhandling.GrantPermission(2, true))
            {
                //hier auch noch Kundennamen mitgeben
                Delete_User deleteUser = new Delete_User(e.Parameter.ToString());
                deleteUser.Owner = Application.Current.MainWindow;
                deleteUser.ShowDialog();
                Load_Data(true);
            }
        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Db_Users> tempMembers = new ObservableCollection<Db_Users>();
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
                            if (x.Username.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 3:
                            if (x.Role.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 4:
                            if (x.Rights.Contains(textBoxFilter.Text))
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