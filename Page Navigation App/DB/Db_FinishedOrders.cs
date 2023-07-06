using System;
using Page_Navigation_App.Helper;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_FinishedOrders
{
    public string ID { get; set; }
    public string Description { get; set; }
    public string CustomerID { get; set; }
    public string EndDate { get; set; }
    public float OrderValue { get; set; }
    public byte[] PDf { get; set; }
    
    public static Error CreateTable(string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
            conn.CreateTable<Db_FinishedOrders>();

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