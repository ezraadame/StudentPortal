using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class CourseViewNavPage : ContentPage
    {
        private ObservableCollection<Courses> _course = new();

        private int _courseId;
        public CourseViewNavPage()
        {
            InitializeComponent();
            SpecificCourse.ItemsSource = _course;
        }

        public CourseViewNavPage(int courseId) : this()
        {
            _courseId = courseId;
        }

        private async void navAssessmentViewButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsNavPage());
        }

        private async void EditCourseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCourseNavPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadSpecificCourse(_courseId);
        }

        private async Task LoadSpecificCourse(int courseId)
        {
            var course = await DBService.GetCourse(courseId);
            _course.Clear();
            if (course != null)
            {
                _course.Add(course);
            }
        }

    }
}