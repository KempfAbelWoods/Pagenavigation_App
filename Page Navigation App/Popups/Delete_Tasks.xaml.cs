using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Delete_Tasks : Window
{
    private string Initial_ID;
    public Delete_Tasks(string ID)
    {
        Initial_ID = ID;
        InitializeComponent();
        this.Tasks_ID.Text = ID;
    }

    private void Delete_Task_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        var data = new Db_Tasks()
        {
            ID = Initial_ID,
        };
        var err = Rw_Tasks.Delete(new List<Db_Tasks> { data },Paths.sqlite_path);
        if (err!=null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        this.Close();
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}