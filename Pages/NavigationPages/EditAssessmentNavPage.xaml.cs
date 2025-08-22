using StudentPortal.Services;
using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditAssessmentNavPage : ContentPage
    {
        private readonly Assessments _assessmentToEdit;
        public EditAssessmentNavPage(Assessments assessmentToEdit)
        {
            InitializeComponent();
            _assessmentToEdit = assessmentToEdit;

            TypePickerEntry.ItemsSource = new List<string>()
            {
                "Performance",
                "Objective"
            };
            LoadAssessmentData();
        }

        private void LoadAssessmentData()
        {
            AssessmentNameEntry.Text = _assessmentToEdit.Name;
            StartDatePickerEntry.Date = _assessmentToEdit.StartDate;
            EndDatePickerEntry.Date = _assessmentToEdit.EndDate;
            TypePickerEntry.SelectedItem = _assessmentToEdit.Type;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _assessmentToEdit.Name = AssessmentNameEntry.Text;
            _assessmentToEdit.StartDate = StartDatePickerEntry.Date;
            _assessmentToEdit.EndDate = EndDatePickerEntry.Date;
            _assessmentToEdit.Type = (string)TypePickerEntry.SelectedItem;

            await DBService.EditAssessment(_assessmentToEdit);
            await DisplayAlert("Success", "Assessment was changed.", "OK");
            await Navigation.PopAsync();
        }
    }
}