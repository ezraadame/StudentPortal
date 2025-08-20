using StudentPortal.Models;
using StudentPortal.Services;
using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class AddTermsNavPage : ContentPage
    {
        public AddTermsNavPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var newTerm = new Term()
            {
                Name = TermNameEntry.Text,
                StartDate = StartDatePicker.Date,
                EndDate = EndDatePicker.Date
            };

            await DBService.InsertTerm(newTerm);
            await DisplayAlert("Success", "Term saved.", "OK");
            await Navigation.PopAsync();
        
        }
    }
}