using System;
using System.Collections.Generic;
using Page_Navigation_App.Helper;
using Page_Navigation_App.View;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_Order
{
    
        public string ID { get; set; }
        public string Description { get; set; }
        public string CustomerID { get; set; }
       //TODO Datetime picker
        public string EndDate { get; set; }
        //hier befinden sich nun die Order Details zur Berechnung verschiedenster Dinge
        //TODO einbinden der OrderValue in DB
        public float OrderValue { get; set; }
        // Todo Ressources tauschen durch das hier
        public float ActualCosts { get; set; }
        //Todo DB_Tasks erstellen (Mitarbeiter verknüpfen und dann in Actual Costs mit reinrechnen)
        //public List<DB_Tasks> 





        public static Error CreateTable(string dataSource)
        {
            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(dataSource);
                conn.CreateTable<Db_Order>();

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