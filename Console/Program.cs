using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary;

namespace MyConsole
{
    class Program
    {
        static List<Mark> loadedMarks = new List<Mark>();
        static List<Subject> loadedSubjects = new List<Subject>();

        static void Main(string[] args)
        {
            loadMarks();
            loadSubjects();

            actionSelection();

            Console.Read();
        }

        static async void loadMarks()
        {
            loadedMarks = await MySQLite.Database.GetMarksAsync();
        }

        static async void loadSubjects()
        {
            loadedSubjects = await MySQLite.Database.GetSubjectsAsync();
        }

        static void actionSelection()
        {
            bool isActionSlected = false;

            while(!isActionSlected)
            {
                Console.WriteLine("[1] - Přidat známku");
                Console.WriteLine("[2] - Přidat předmět");
                Console.WriteLine("[3] - Zobrazit známky");

                ConsoleKeyInfo selection = Console.ReadKey();

                switch (selection.KeyChar)
                {
                    case '1':
                        MySQLite.Database.SaveMarkAsync(addMark());
                        Console.Clear();
                        Console.WriteLine("Známka přidána!");
                        break;
                    case '2':
                        MySQLite.Database.SaveSubjectAsync(addSubject());
                        Console.Clear();
                        break;

                    case '3':
                        loadMarks();
                        loadSubjects();

                        Console.Clear();
                        Console.WriteLine("Stisknutím libovolné klávesy se vratíte zpět do menu...\n");

                        foreach (Mark mark in loadedMarks)
                        {
                            Console.WriteLine("{0}: {1} ({2}%)", loadedSubjects[mark.SubjectId].Name, mark.Value, mark.Weight);
                        }
                        
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        static Mark addMark()
        {
            loadSubjects();

            Mark newMark = new Mark();

            bool isSubjectSelected = false;
            int subjectId = 0;

            while (!isSubjectSelected)
            {
                Console.Clear();
                Console.WriteLine("Vyberte předmět: \n");

                int i = 1;
                foreach(Subject sub in loadedSubjects)
                {
                    Console.WriteLine("[{0}] - {1}", i, sub.Name);
                    i++;
                }

                string subjectIdStr = Console.ReadLine();

                if (int.TryParse(subjectIdStr, out subjectId) && subjectId <= i && subjectId != 0)
                {
                    isSubjectSelected = true;
                }
            }
            newMark.SubjectId = subjectId - 1;

            bool isValueSelected = false;
            float value = 0;

            while (!isValueSelected)
            {
                Console.Clear();
                Console.Write("Známka: ");
                string valueStr = Console.ReadLine();
                if (float.TryParse(valueStr, out value) && value >= 1 && value <= 5)
                {
                    isValueSelected = true;
                }
            }
            newMark.Value = value;

            bool isWeightSelected = false;
            int weight = 0;

            while (!isWeightSelected)
            {
                Console.Clear();
                Console.Write("Váha známky: ");
                string weightStr = Console.ReadLine();
                if (int.TryParse(weightStr, out weight) && weight >= 1 && weight <= 100)
                {
                    isWeightSelected = true;
                }
            }
            newMark.Weight = weight;

            return newMark;
        }

        static Subject addSubject()
        {
            Subject newSubject = new Subject();

            bool isNameSelected = false;
            string name = "";

            while (!isNameSelected)
            {
                Console.Clear();
                Console.Write("Název předmětu: ");
                name = Console.ReadLine();

                if (name != "")
                {
                    isNameSelected = true;
                }
            }
            newSubject.Name = name;

            return newSubject;
        }
    }
}
