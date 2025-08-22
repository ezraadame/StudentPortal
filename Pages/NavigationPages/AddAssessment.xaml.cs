using StudentPortal.Models;
using StudentPortal.Services;
using System.Threading.Tasks;
namespace StudentPortal.Pages.NavigationPages;

public partial class AddAssessment : ContentPage
{
	public AddAssessment()
	{
		InitializeComponent();
        TypePickerEntry.ItemsSource = new List<string>
            {
                "Performance",
                "Objective",
            };
    }

    private async void SaveCourseButton_Clicked(object sender, EventArgs e)
    {
        var newAssessment = new Assessments()
        {
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