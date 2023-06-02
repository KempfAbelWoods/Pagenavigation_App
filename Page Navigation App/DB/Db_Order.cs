using System;
using Page_Navigation_App.Helper;
using Page_Navigation_App.View;
using SQLite;

namespace Page_Navigation_App.DB;

public class Db_Order
{

    public class Order
    {
        public string Character { get; set; }
        public string BgColor { get; set; }
        public string ID { get; set; }
        public string Kunde { get; set; }
        public string Description { get; set; }
        public string Ressources { get; set; }
        public string EndDate { get; set; }





        public static Error CreateTable(string dataSource)
        {
            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(dataSource);
                conn.CreateTable<Order>();

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
}