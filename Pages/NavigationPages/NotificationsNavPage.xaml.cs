using Plugin.LocalNotification;
using StudentPortal.Models;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class NotificationsNavPage : ContentPage
    {
        public NotificationsNavPage()
        {
            InitializeComponent();
        }

        private void TermStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void TermEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            var request = new NotificationRequest()
            {
                Title = "Term End Date Alert",
                Subtitle = "Term ends soon",
                BadgeNumber = 42,
                CategoryType = NotificationCategoryType.Reminder,
                Schedule = new NotificationRequestSchedule()
                {
                    
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        private void CourseStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void CourseEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void AssessmentStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void AssessmentEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}