using StudentPortal.Models;
using StudentPortal.Services;
using StudentPortal.Validate;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditTermsNavPage : ContentPage
    {
        private readonly Term _termToEdit;

        public EditTermsNavPage(Term termToEdit)
        {
            InitializeComponent();
            _termToEdit = termToEdit;
            LoadTermData();
        }

        private void LoadTermData()
        {
            TermNameEntry.Text = _termToEdit.Name;
            StartDatePicker.Date = _termToEdit.StartDate;
            EndDatePicker.Date = _termToEdit.EndDate;
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

            _termToEdit.Name = TermNameEntry.Text;
            _termToEdit.StartDate = StartDatePicker.Date;
            _termToEdit.EndDate = EndDatePicker.Date;

            await DBService.EditTerms(_termToEdit);
            await DisplayAlert("Success", "Term was changed", "OK");
            await Navigation.PopAsync();
        }
    }
}