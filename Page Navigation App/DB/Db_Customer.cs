using System;
using System.Collections.Generic;
using System.Linq;
using Page_Navigation_App.Helper;
using Page_Navigation_App.View;
using SQLite;
namespace Page_Navigation_App.DB;

public class Db_Customer
{
    
    public class Customer
    {
        public string Character { get; set; }
        public string BgColor { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
    
        [PrimaryKey, AutoIncrement]
        public uint ID { get; set; }

        public string Character { get; set; }
        
        public string BGColor { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
        
        public string Mail { get; set; }
        
        public string Phone { get; set; }

        


        public static Error CreateTable(string dataSource)
        {
            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(dataSource);
                conn.CreateTable<Customer>();

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
    