using System;
using System.Collections.Generic;
using Page_Navigation_App.Helper;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_Tasks
{
    public string ID { get; set; }
    public string orderID { get; set; }
    public string Description { get; set; }
    public string Username { get; set; } //Maschinen auch als Benutzer zuweisen aber einem anderen User unterordnen
    public float EstimatedHours { get; set; }
    public float ActualHours { get; set; }
    public float Costs { get; set; } //ist der Task im Minus oder plus?


    public static Error CreateTable(string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
            conn.CreateTable<Db_Tasks>();

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