using SQLite;
using System;
using System.IO;

namespace ClassLibrary
{
    public class MySQLite
    {
        static ItemDatabase database;

        public static ItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ItemDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDatabase.db3"));
                }
                return database;
            }
        }
    }
}
