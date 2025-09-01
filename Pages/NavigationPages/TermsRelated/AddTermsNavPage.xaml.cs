using StudentPortal.Models;
using StudentPortal.Validate;
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
            var validationResult = ValidateTerm.ValidateTermInput(
            TermNameEntry.Text,
            StartDatePicker.Date,
            EndDatePicker.Date);

            if (!validationResult.IsValid)
            {
                await DisplayAlert("Validation Error", validationResult.ErrorMessage, "OK");
                return;
            }

            var newTerm = new Term()
            {
                Name = TermNameEntry.Text,
                StartDate = StartDatePicker.Date,
                EndDate = EndDatePicker.Date,
                UserId = UserSession.CurrentUserId
            };

            await DBService.InsertTerm(newTerm);
            await DisplayAlert("Success", "Term saved.", "OK");
            await Navigation.PopAsync();
        
        }
    }
}