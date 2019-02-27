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
	public partial class AddSubject : ContentPage
	{
		public AddSubject ()
		{
			InitializeComponent ();            
        }

        List<Subject> loadedSubjects = new List<Subject>();

        protected override async void OnAppearing()
        {
            await loadSubjects();
        }

        async Task loadSubjects()
        {
            loadedSubjects = await MySQLite.Database.GetSubjectsAsync();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            subjectAddAction(SubjectNameInput.Text);
        }

        async void postSubjectToDb(Subject subject)
        {
            await MySQLite.Database.SaveSubjectAsync(subject);
        }

        void subjectAddAction(string subjectName)
        {
            if (subjectName == "")
            {
                DisplayAlert("Chyba", "Nejdříve vyplňte název předmětu", "OK");
            }
            else
            {
                bool subjectExist = false;

                foreach (Subject subject in loadedSubjects.ToList())
                {
                    if (subject.Name == subjectName)
                    {
                        subjectExist = true;
                    }
                }

                if (!subjectExist)
                {
                    DisplayAlert("", "Předmět přidán", "OK");

                    Subject postedSubject = new Subject { Name = subjectName };

                    postSubjectToDb(postedSubject);
                    loadedSubjects.Add(postedSubject);
                }
                else
                {
                    DisplayAlert("Chyba", "Zadaný předmět již v databázi existuje", "OK");
                }
            }
        }
    }
}