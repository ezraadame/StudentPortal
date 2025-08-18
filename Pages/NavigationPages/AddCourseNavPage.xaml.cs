namespace StudentPortal.Pages.NavigationPage
{
    public partial class AddCourseNavPage : ContentPage
    {
        public AddCourseNavPage()
        {
            InitializeComponent();

            StatusPicker.ItemsSource = new List<string>
            {
                "Pending",
                "In Progress",
                "Completed",
                "Cancelled"
            }; 
        }

        //TODO Implement AddCourse;
    }
}