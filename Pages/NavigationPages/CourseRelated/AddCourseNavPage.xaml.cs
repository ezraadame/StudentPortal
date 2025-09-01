using StudentPortal.Models;
using StudentPortal.Services;
using StudentPortal.Validate;
using System.Text.RegularExpressions;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class AddCourseNavPage : ContentPage
    {
        private int _termId;
        public AddCourseNavPage(int termId)
        {
            InitializeComponent();
            _termId = termId;

            StatusPickerEntry.ItemsSource = new List<string>
            {
                "Pending",
                "In Progress",
                "Completed",
                "Cancelled"
            }; 
        }

        private async void SaveCourseButton_Clicked(object sender, EventArgs e)
        {
            var validationResult = ValidateCourse.ValidateCourseInput(
            CourseNameEntry.Text,
            InstructorNameEntry.Text,
            StatusPickerEntry.SelectedItem,
            StartDatePickerEntry.Date,
            EndDatePickerEntry.Date,
            InstructorPhoneEntry.Text,
            InstructorEmailEntry.Text
        );

            if (!validationResult.IsValid)
            {
                await DisplayAlert("Validation Error", validationResult.ErrorMessage, "OK");
                return;
            }

            var newCourse = new Courses()
            {
                TermId = _termId,
                UserId = UserSession.CurrentUserId,
                Name = CourseNameEntry.Text.Trim(),
                StartDate = StartDatePickerEntry.Date,
                EndDate = EndDatePickerEntry.Date,
                Status = (string)StatusPickerEntry.SelectedItem,
                InstructorName = InstructorNameEntry.Text.Trim(),
                InstructorPhone = InstructorPhoneEntry.Text.Trim(),
                InstructorEmail = InstructorEmailEntry.Text.Trim(),
                Notes = AdditionalNotesEntry.Text.Trim()

            };
            try
            {
                await DBService.InsertCourse(newCourse);
                await DisplayAlert("Success", "Course saved.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Failed to add course. Please try again.", "OK");
            }
        }
    }
}