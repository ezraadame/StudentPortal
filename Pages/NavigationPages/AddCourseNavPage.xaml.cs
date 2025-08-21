using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class AddCourseNavPage : ContentPage
    {
        public AddCourseNavPage()
        {
            InitializeComponent();

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
            var newCourse = new Courses()
            {
                Name = CourseNameEntry.Text,
                StartDate = StartDatePickerEntry.Date,
                EndDate = EndDatePickerEntry.Date,
                Status = (string)StatusPickerEntry.SelectedItem,
                InstructorName = InstructorNameEntry.Text,
                InstructorPhone = InstructorPhoneEntry.Text,
                InstructorEmail = InstructorEmailEntry.Text,
                Notes = AdditionalNotesEntry.Text
                //NotificationOn = notificationOn


            };

            await DBService.InsertCourse(newCourse);
            await DisplayAlert("Success", "Course saved.", "OK");
            await Navigation.PopAsync();
        }

        
    }
}