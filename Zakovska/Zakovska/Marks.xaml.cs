using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ClassLibrary;

namespace Zakovska
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Marks : ContentPage
	{
        static List<Subject> loadedSubjects = new List<Subject>();

        public Marks ()
		{
			InitializeComponent ();

            loadSubjects();

            loadedSubjects.Add(new Subject { Id = 0, Name = "Hello" });
            loadedSubjects.Add(new Subject { Id = 1, Name = "World" });
            loadedSubjects.Add(new Subject { Id = 2, Name = "!!!" });

            int i = 0;

            while(i <= 100)
            {
                loadedSubjects.Add(new Subject { Id = i+3, Name = "!!!" });
                i++;
            }

            foreach (Subject subject in loadedSubjects)
            {
                Label label = new Label();
                label.Text = subject.Name;

                MarksStack.Children.Add(label);
            }
		}

        static async void loadSubjects()
        {
            loadedSubjects = await MySQLite.Database.GetSubjectsAsync();
        }
    }
}