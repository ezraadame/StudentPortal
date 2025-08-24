using StudentPortal.Models;
using StudentPortal.Services;
using StudentPortal.Validate;
using System.Threading.Tasks;
namespace StudentPortal.Pages.NavigationPages;

public partial class AddAssessment : ContentPage
{
    private int _courseId;
	public AddAssessment(int courseId)
	{
		InitializeComponent();
        _courseId = courseId;
        TypePickerEntry.ItemsSource = new List<string>
            {
                "Performance",
                "Objective",
            };
    }
    private async void SaveCourseButton_Clicked(object sender, EventArgs e)
    {
        var validationResult= ValidateAssessment.ValidateAssessmentInput(
            AssessmentNameEntry.Text,
            TypePickerEntry.SelectedItem,
            StartDatePickerEntry.Date,
            EndDatePickerEntry.Date);

        if (!validationResult.IsValid)
        {
            await DisplayAlert("Validation Error", validationResult.ErrorMessage, "OK");
            return;
        }

        var newAssessment = new Assessments()
        {
            CourseId = _courseId,
            Name = AssessmentNameEntry.Text,
            StartDate = StartDatePickerEntry.Date,
            EndDate = EndDatePickerEntry.Date,
            Type = (string)TypePickerEntry.SelectedItem
        };

        await DBService.InsertAssessment(newAssessment);
        await DisplayAlert("Success", "Assessment added!", "OK");
        await Navigation.PopAsync();
    }
}