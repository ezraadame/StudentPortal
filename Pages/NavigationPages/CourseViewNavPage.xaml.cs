namespace StudentPortal.Pages.NavigationPage
{
    public partial class CourseViewNavPage : ContentPage
    {
        public CourseViewNavPage()
        {
            InitializeComponent();
        }

        private async void navAssessmentViewButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsNavPage());
        }

        private async void EditCourseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCourseNavPage());
        }
    }
}