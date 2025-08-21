using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class CoursesNavPage : ContentPage
    {
        private ObservableCollection<Courses> _courses = new();
        public CoursesNavPage()
        {
            InitializeComponent();
            CourseCollection.ItemsSource = _courses;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCourses();
        }

        private async Task LoadCourses()
        {
            var courses = await DBService.GetCourses();
            _courses.Clear();
            foreach (var course in courses)
                _courses.Add(course);
        }

        private async void addCourseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourseNavPage());
        }

        private async void navCoursePageButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.CommandParameter is Courses course)
            {
                await Navigation.PushAsync(new CourseViewNavPage(course.Id));
                return;
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var courseToDelete = button?.CommandParameter as Courses;

            if (courseToDelete != null)
            {
                bool userConfirmed = await DisplayAlert(
                    "Delete Term",
                    $"Are you sure you want to delete '{courseToDelete.Name}'?",
                    "Delete",
                    "Cancel"
                    );

                if (userConfirmed)
                {
                    await DBService.DeleteCourse(courseToDelete);
                    _courses.Remove(courseToDelete);

                    await DisplayAlert("Success", "Term deleted successfully", "OK");
                }
            }
        }
    }
}