using System;
using Page_Navigation_App.Helper;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_Users
{
    //Maschinen auch als Benutzer zuweisen aber einem anderen User unterordnen
    public string ID { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Role { get; set; } //Administrator, Worker, Machine, 
    public string Rights { get; set; } //Lesen von Datenbanken, Lesen und Schreiben alles, Anstempeln von Zeiten
    public string Password { get; set; }


    public static Error CreateTable(string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
            conn.CreateTable<Db_Users>();

            return null;
        }
        catch (Exception ex)
        {
            return new Error(ex.ToString());
        }
        finally
        {
            conn?.Close();
        }
    }
}