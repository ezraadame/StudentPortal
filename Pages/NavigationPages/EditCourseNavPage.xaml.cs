using StudentPortal.Models;
using StudentPortal.Services;
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
            _courseToEdit.Name = CourseNameEntry.Text;
            _courseToEdit.StartDate = StartDatePickerEntry.Date;
            _courseToEdit.EndDate = EndDatePickerEntry.Date;
            _courseToEdit.Status = (string)StatusPickerEntry.SelectedItem;
            _courseToEdit.InstructorName = InstructorNameEntry.Text;
            _courseToEdit.InstructorEmail = InstructorEmailEntry.Text;
            _courseToEdit.InstructorPhone = InstructorPhoneEntry.Text;
            _courseToEdit.Notes = AdditionalNotesEntry.Text;

            await DBService.EditCourse(_courseToEdit);
            await DisplayAlert("Success", "Course was changed.", "OK");
            await Navigation.PopAsync();
        }
    }
}