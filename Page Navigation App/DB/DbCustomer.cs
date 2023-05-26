using System;
using System.Collections.Generic;
using System.Linq;
using Page_Navigation_App.Helper;
using Page_Navigation_App.View;
using SQLite;
namespace Page_Navigation_App.DB;

public class DbCustomer
{
    
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
                conn.CreateTable<Member>();

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
    