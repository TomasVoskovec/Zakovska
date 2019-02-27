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
	public partial class AddMark : ContentPage
	{
		public AddMark ()
		{
			InitializeComponent ();
		}

        List<Subject> loadedSubjects = new List<Subject>();

        protected override async void OnAppearing()
        {
            await loadSubjects();
            updateSubjects();
        }

        void updateSubjects ()
        {
            subjectPicker.Items.Clear();

            foreach (Subject subject in loadedSubjects.ToList())
            {
                subjectPicker.Items.Add(subject.Name);
            }
        }

        async Task loadSubjects()
        {
            loadedSubjects = await MySQLite.Database.GetSubjectsAsync();
        }
    }
}