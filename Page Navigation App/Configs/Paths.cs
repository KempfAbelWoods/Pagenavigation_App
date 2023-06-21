namespace Page_Navigation_App.Configs;

public static class Paths
{
    public const string sqlite_path = "../../../data/data.sqlite"; //"/identifier.sqlite"; //  /Page Navigation App/data/data.sqlite;


}

public static class ActualUser
{
    public static string Username = "";
    public static string Password = "";
} 

public class Set_Users
{
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            ActualUser.Username = _username;
            MainWindow.getuser();
        }
    }
    
    private string _username;
    private string _password;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            ActualUser.Password = _password;
            MainWindow.getuser();
        }
    }
}