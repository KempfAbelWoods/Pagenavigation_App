using System;
using System.Collections.Generic;
using System.Linq;
using Page_Navigation_App.Helper;
using SQLite;

namespace Page_Navigation_App.DB;

public class Rw_Settings
{
     /// <summary>
    /// Reihen in DB schreiben
    /// </summary>
    public static Error Write(List<Db_Settings> rows, string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
               
            foreach (var row in rows)
            {
                string where = $"WHERE ID='{row.ID}'";

                // pruefen ob Item vorhanden ist
                var item = conn.Query<Db_Settings>($"SELECT * FROM {nameof(Db_Settings)} {where}").FirstOrDefault();
                if (item != null)
                {
                    // schon vorhanden, loeschen
                    conn.Execute($"DELETE FROM {nameof(Db_Settings)} {where}");
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
    public static Error Delete(List<Db_Settings> rows, string dataSource)
    {
        
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
               
            foreach (var row in rows)
            {
                string where = $"WHERE ID='{row.ID}'";

                // pruefen ob Item vorhanden ist
                var item = conn.Query<Db_Settings>($"SELECT * FROM {nameof(Db_Settings)} {where}").FirstOrDefault();
                if (item != null)
                {
                    // schon vorhanden, loeschen
                    conn.Execute($"DELETE FROM {nameof(Db_Settings)} {where}");
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
    public static (List<Db_Settings>, Error) ReadwithID(string ID, string dataSource)
    {
        var where = $"ID='{ID}'";
        return Read(where, dataSource);
    }
    
    public static (List<Db_Settings>, Error) ReadwithName(string Name, string dataSource)
    {
        var where = $"Name='{Name}'";
        return Read(where, dataSource);
    }
    
    public static (List<Db_Settings>, Error) Read(string where, string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);

            string query = $"SELECT * FROM {nameof(Db_Settings)} ";
            if (where != "")
            {
                query += "WHERE " + where;
            }

            return (conn.Query<Db_Settings>(query), null);
        }
        catch (Exception ex)
        {
            return (new List<Db_Settings>(), new Error(ex.ToString()));
        }
        finally
        {
            conn?.Close();
        }
    }
}