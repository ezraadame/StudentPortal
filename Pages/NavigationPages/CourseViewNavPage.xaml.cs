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
            var button = sender as Button;
            if (button?.CommandParameter is Courses selectedCourse)
            {
                await Navigation.PushAsync(new AssessmentsNavPage(selectedCourse.Id));
            }
            
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

        private async void ShareButtonForNotes_Clicked(object sender, EventArgs e)
        {
            if (_course.Count > 0 && !string.IsNullOrWhiteSpace(_course[0].Notes))
            {
                await ShareText(_course[0].Notes);
            }
            else
            {
                await DisplayAlert("No Notes", "There are no notes to share for this course.", "OK");
            }
        }

        public async Task ShareText(string notes) => await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = notes,
            Title = "Share Text"
        });

    }
}