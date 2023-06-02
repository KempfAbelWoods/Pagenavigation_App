using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Page_Navigation_App.Helper;
using System.Collections.Generic;
using System.Linq;
using System;
using Page_Navigation_App.View;

namespace Page_Navigation_App.DB;
using System.Data.Common;
using System.Data.SqlClient;
using SQLite;

public static class RW_Customer
{
    
    /// <summary>
    /// Reihen in DB schreiben
    /// </summary>
    public static Error Write(List<Db_Customer> rows, string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
               
            foreach (var row in rows)
            {
                string where = $"WHERE ID='{row.ID}'";

                // pruefen ob Item vorhanden ist
                var item = conn.Query<Db_Customer>($"SELECT * FROM {nameof(Db_Customer)} {where}").FirstOrDefault();
                if (item != null)
                {
                    // schon vorhanden, loeschen
                    conn.Execute($"DELETE FROM {nameof(Db_Customer)} {where}");
                }

                conn.Insert(row);
            }

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

    /// <summary>
    /// Löscht die ausgewählte Reihe
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="dataSource"></param>
    /// <returns></returns>
    public static Error Delete(List<Db_Customer> rows, string dataSource)
    {
        
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
               
            foreach (var row in rows)
            {
                string where = $"WHERE ID='{row.ID}'";

                // pruefen ob Item vorhanden ist
                var item = conn.Query<Db_Customer>($"SELECT * FROM {nameof(Db_Customer)} {where}").FirstOrDefault();
                if (item != null)
                {
                    // schon vorhanden, loeschen
                    conn.Execute($"DELETE FROM {nameof(Db_Customer)} {where}");
                }
            }
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
   
    /// <summary>
    /// Reihen mit SerialNumber lesen
    /// </summary>
    public static (List<Db_Customer>, Error) ReadwithID(string ID, string dataSource)
    {
        var where = $"ID='{ID}'";
        return Read(where, dataSource);
    }
    
    public static (List<Db_Customer>, Error) ReadwithName(string Name, string dataSource)
    {
        var where = $"Name='{Name}'";
        return Read(where, dataSource);
    }
    
    public static (List<Db_Customer>, Error) Read(string where, string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);

            string query = $"SELECT * FROM {nameof(Db_Customer)} ";
            if (where != "")
            {
                query += "WHERE " + where;
            }

            return (conn.Query<Db_Customer>(query), null);
        }
        catch (Exception ex)
        {
            return (new List<Db_Customer>(), new Error(ex.ToString()));
        }
        finally
        {
            conn?.Close();
        }
    }
    
}
