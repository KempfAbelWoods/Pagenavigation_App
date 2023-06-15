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
    /// Interaction logic for Shipments.xaml
    /// </summary>
    public partial class Ressources : UserControl
    {
         ObservableCollection<Db_Ressources> members = new ObservableCollection<Db_Ressources>();
        ObservableCollection<Db_Ressources> shownmembers = new ObservableCollection<Db_Ressources>();
        private int SearchId = 1;
        public Ressources()
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
            
            var (list, err) = Rw_Ressources.Read("",Paths.sqlite_path);

                for (int i = 0; i < list.Count; i++)
                {
                    members.Add(new Db_Ressources { ID= list[i].ID, Description = list[i].Description, Name = list[i].Name, Ressourcetype = list[i].Ressourcetype, Costs = list[i].Costs, Costtype = list[i].Costtype});
                }
           if (dbread)
            {
                shownmembers = members;
                textBoxFilter.Text = "";
            }
           ressourcesDataGrid.ItemsSource = shownmembers;
           string Costtype;
         /*  if (shownmembers[0].Costtype)
           { Costtype = "Per Hour"; }
           else
           { Costtype = "One-Time Costs";}

           ressourcesDataGrid.Items[5] = Costtype;*/
        }
        
        void EditRessources(object sender, ExecutedRoutedEventArgs e)
        {

            var (list,err) = Rw_Ressources.ReadwithID(e.Parameter.ToString(), Paths.sqlite_path);
            if (list.Count==1)
            {
                Edit_Ressource editRessources = new Edit_Ressource(list[0].ID,list[0].Description,list[0].Name,list[0].Ressourcetype,list[0].Costs,list[0].Costtype);
                editRessources.Owner = Application.Current.MainWindow;
                editRessources.ShowDialog();
                Load_Data(true);
            }
        }
        
        void AddRessources(object sender, RoutedEventArgs e)
        {
            
            Edit_Ressource editRessources = new Edit_Ressource("","Description","Name","Machine",0, true);
            editRessources.Owner = Application.Current.MainWindow;
            editRessources.ShowDialog();
            Load_Data(true);

        }
        
        void DeleteRessources(object sender, ExecutedRoutedEventArgs e)
        {
            //hier auch noch Kundennamen mitgeben
            Delete_Ressource deleteRessources = new Delete_Ressource(e.Parameter.ToString());
            deleteRessources.Owner = Application.Current.MainWindow;
            deleteRessources.ShowDialog();
            Load_Data(true);

        }

        private void TextBoxFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Db_Ressources> tempMembers = new ObservableCollection<Db_Ressources>();
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
                            if (x.Description.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
                        case 3:
                            if (x.Ressourcetype.Contains(textBoxFilter.Text))
                            {
                                tempMembers.Add(x);
                            }
                            break;
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
