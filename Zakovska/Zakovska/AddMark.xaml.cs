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

            if (markValueInput.Text != "" && float.TryParse(markValueInput.Text, out float value) && value <= 5 && value >= 1)
            {
                if (markWeightInput.Text != "" && int.TryParse(markWeightInput.Text, out int wight) && wight <= 100 && wight >= 1)
                {
                    if (subjectPicker.SelectedIndex != -1)
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

                        postMarkToDb(newMark);

                        DisplayAlert("", "Známka přidána", "OK");
                    }
                    else
                    {
                        subjectPicker.Focus();
                        DisplayAlert("Chyba", "Nejdříve vyberte předmět", "OK");
                    }
                }
                else
                {
                    markWeightInput.Focus();
                    DisplayAlert("Chyba", "Špatně vyplněná váha známky", "OK");
                }
            }
            else
            {
                markValueInput.Focus();
                DisplayAlert("Chyba", "Špatně vyplněná známka", "OK");
            }
        }

        async void postMarkToDb(Mark mark)
        {
            await MySQLite.Database.SaveMarkAsync(mark);
        }
    }
}