using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary;

namespace MyConsole
{
    class Program
    {
        static List<Mark> loadedMarks = new List<Mark>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            loadMarks();

            Console.Read();
        }

        static async void loadMarks()
        {
            loadedMarks = await MySQLite.Database.GetItemsAsync();

            foreach (Mark mark in loadedMarks)
            {
                Console.WriteLine("Známka: {0} - váha: {1}", mark.Value, mark.Scale);
            }
        }
    }
}
