using StudentPortal.Models;
using StudentPortal.Services;
using StudentPortal.Validate;
using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditCourseNavPage : ContentPage
    {

        private readonly Courses _courseToEdit;
        public EditCourseNavPage(Courses courseToEdit)
        {
            InitializeComponent();
            _courseToEdit = courseToEdit;

            StatusPickerEntry.ItemsSource = new List<string>
            {
                "Pending",
                "In Progress",
                "Completed",
                "Cancelled"
            };
            LoadCourseDate();
        }

        private void LoadCourseDate()
        {
            CourseNameEntry.Text = _courseToEdit.Name;
            StartDatePickerEntry.Date = _courseToEdit.StartDate;
            EndDatePickerEntry.Date = _courseToEdit.EndDate;
            StatusPickerEntry.SelectedItem = _courseToEdit.Status;
            InstructorNameEntry.Text = _courseToEdit.InstructorName;
            InstructorEmailEntry.Text = _courseToEdit.InstructorEmail;
            InstructorPhoneEntry.Text = _courseToEdit.InstructorPhone;
            AdditionalNotesEntry.Text = _courseToEdit.Notes;
        }

        

        private async void SaveButton_Clicked(object sender, EventArgs e)
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

            _courseToEdit.Name = CourseNameEntry.Text;
            _courseToEdit.StartDate = StartDatePickerEntry.Date;
            _courseToEdit.EndDate = EndDatePickerEntry.Date;
            _courseToEdit.Status = (string)StatusPickerEntry.SelectedItem;
            _courseToEdit.InstructorName = InstructorNameEntry.Text;
            _courseToEdit.InstructorEmail = InstructorEmailEntry.Text;
            _courseToEdit.InstructorPhone = InstructorPhoneEntry.Text;
            _courseToEdit.Notes = AdditionalNotesEntry.Text;

            try
            {
                await DBService.EditCourse(_courseToEdit);
                await DisplayAlert("Success", "Course saved.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to edit course. Please try again.", "OK");
            }
        }
    }
}