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
        public string EndDate { get; set; }
        public float OrderValue { get; set; }
        public float ActualCosts { get; set; }





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