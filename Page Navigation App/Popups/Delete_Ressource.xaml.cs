using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Delete_Ressource : Window
{
    private string Initial_ID;

    public Delete_Ressource(string Ressource_ID)
    {
        Initial_ID = Ressource_ID;
        InitializeComponent();
        Ressourcename.Text = Ressource_ID;
    }

    private void Delete_Ressource_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        if (CheckTasks())
        {
            var data = new Db_Ressources()
            {
                ID = Initial_ID,
            };
            var err = Rw_Ressources.Delete(new List<Db_Ressources> { data }, Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            this.Close();
        }
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    private bool CheckTasks()
    {
        bool Deletable = true;

        var (list, err) = Rw_Tasks.ReadwithRessourceID(Initial_ID, Paths.sqlite_path);
        if (err != null)
        {
            MessageBox.Show(err.GetException().Message);
        }

        if (list.Count > 0)
        {
            Deletable = false;
            MessageBox.Show("Ressource can't be deleted, cause it is used by Tasks.");
        }

        return Deletable;
    }
}