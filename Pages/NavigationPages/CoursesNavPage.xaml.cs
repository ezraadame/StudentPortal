using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class CoursesNavPage : ContentPage
    {
        public CoursesNavPage()
        {
            InitializeComponent();
        }

        private async void navCourse1PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void navCourse2PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void navCourse3PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void navCourse4PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void navCourse5PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void navCourse6PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseViewNavPage());
        }

        private async void addCourseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourseNavPage());
        }
    }
}