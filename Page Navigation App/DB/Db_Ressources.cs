using System;
using Page_Navigation_App.Helper;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_Ressources
{
    public string ID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Ressourcetype { get; set; }
    public float Costs { get; set; }
    public bool Costtype { get; set; } //false einmalige Kosten, true Kosten pro Stunde





    public static Error CreateTable(string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
            conn.CreateTable<Db_Ressources>();

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