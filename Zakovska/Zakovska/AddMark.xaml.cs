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

        private void Send_Clicked(object sender, EventArgs e)
        {
            Mark newMark = new Mark();

            if (markValueInput.Text != "")
            {
                if (markWeightInput.Text != "")
                {
                    if (subjectPicker.SelectedItem.ToString() != "" || subjectPicker.SelectedItem != null)
                    {
                        string valueStr = markValueInput.Text;
                        newMark.Value = float.Parse(valueStr);

                        newMark.Weight = int.Parse(markWeightInput.Text);

                        foreach (Subject subject in loadedSubjects.ToList())
                        {
                            if(subject.Name == subjectPicker.SelectedItem.ToString())
                            {
                                newMark.SubjectId = subject.Id;
                            }
                        }
                    }
                    else
                    {
                        DisplayAlert("Chyba", "Nejdříve vyberte předmět", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Chyba", "Špatně vyplněná váha známky", "OK");
                }
            }
            else
            {
                DisplayAlert("Chyba", "Špatně vyplněná známka", "OK");
            }
        }
    }
}