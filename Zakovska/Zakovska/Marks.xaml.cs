using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ClassLibrary;
using System.Threading.Tasks;

namespace Zakovska
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Marks : ContentPage
	{
        public Marks ()
		{
			InitializeComponent ();

            //postSubjectToDb(new Subject { Name = "Matematika" });
            /*postMarkToDb(new Mark { Value = 4.5f, SubjectId = 1, Weight = 15 });
            postMarkToDb(new Mark { Value = 5, SubjectId = 1, Weight = 15 });*/

           
            /*int i = 0;

            while(i <= 100)
            {
                loadedSubjects.Add(new Subject { Id = i+3, Name = "!!!" });
                i++;
            }*/
		}

        protected override async void OnAppearing()
        {
            await loadMarks();
        }


        async Task loadMarks()
        {
            List<Subject> loadedSubject = await MySQLite.Database.GetSubjectsAsync();
            List<Mark> loadedMarks = await MySQLite.Database.GetMarksAsync();

            MarksStack.Children.Clear();

            foreach (Subject subject in loadedSubject)
            {
                Label subjectLabel = new Label();
                subjectLabel.Text = subject.Name;
                MarksStack.Children.Add(subjectLabel);

                foreach (Mark mark in loadedMarks)
                {
                    if(mark.SubjectId == subject.Id)
                    {
                        Label markLabel = new Label();
                        markLabel.Text = mark.Value.ToString() + " (" + mark.Weight + "%)";
                        if (mark.Value >= 4.5f)
                        {
                            markLabel.TextColor = Color.Red;
                        }
                        else
                        {
                            markLabel.TextColor = Color.Green;
                        }
                        MarksStack.Children.Add(markLabel);
                    }
                }
            }
        }

        async void postSubjectToDb(Subject subject)
        {
            await MySQLite.Database.SaveSubjectAsync(subject);
        }

        async void postMarkToDb(Mark mark)
        {
            await MySQLite.Database.SaveMarkAsync(mark);
        }
    }
}