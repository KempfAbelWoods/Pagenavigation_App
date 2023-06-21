using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

namespace Page_Navigation_App.Popups;

public partial class Delete_User : Window
{
    private string Initial_ID;
    
    public Delete_User(string User_ID)
    {
        Initial_ID = User_ID;
        InitializeComponent();
        this.Order_ID.Text = User_ID;
    }
    
    private void Delete_User_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        var data = new Db_Users()
        {
            ID = Initial_ID,
        };
        var err = Rw_Users.Delete(new List<Db_Users> { data },Paths.sqlite_path);
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