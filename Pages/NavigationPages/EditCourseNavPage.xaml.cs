namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditCourseNavPage : ContentPage
    {
        public EditCourseNavPage()
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
    }
}