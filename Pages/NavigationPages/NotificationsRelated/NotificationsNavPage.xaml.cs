using Plugin.LocalNotification;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class NotificationsNavPage : ContentPage
    {
        public NotificationsNavPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadToggleStates();
        }

        private void LoadToggleStates()
        {
            TermStartDateToggle.IsToggled = Preferences.Get("TermStartNotifications", false);
            TermEndDateToggle.IsToggled = Preferences.Get("TermEndNotifications", false);
            CourseStartDateToggle.IsToggled = Preferences.Get("CourseStartNotifications", false);
            CourseEndDateToggle.IsToggled = Preferences.Get("CourseEndNotifications", false);
            AssessmentStartDateToggle.IsToggled = Preferences.Get("AssessmentStartNotifications", false);
            AssessmentEndDateToggle.IsToggled = Preferences.Get("AssessmentEndNotifications", false);

        }

        private async void TermStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("TermStartNotifications", e.Value);

            var terms = await DBService.GetTermsByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var termsStartingToday = terms.Where(term => term.StartDate.Date == today).ToList();

            foreach (var term in termsStartingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = term.Id + 1000,
                    Title = "TERM STARTING TODAY!",
                    Description = $"Your term '{term.Name}' is starting today!",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        private async void TermEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("TermEndNotifications", e.Value);

            var terms = await DBService.GetTermsByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var termsEndingToday = terms.Where(term => term.EndDate.Date == today).ToList();

            foreach (var term in termsEndingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = term.Id + 2000,
                    Title = "TERM ENDING TODAY",
                    Description = $"Your term '{term.Name}' is ending today!",

                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        private async void CourseStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("CourseStartNotifications", e.Value);

            var courses = await DBService.GetCoursesByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var coursesStartingToday = courses.Where(course => course.StartDate.Date == today).ToList();

            foreach (var course in coursesStartingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = course.Id + 3000,
                    Title = "COURSE STARTING TODAY!",
                    Description = $"Your term '{course.Name}' is starting today!",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        private async void CourseEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("CourseEndNotifications", e.Value);

            var courses = await DBService.GetCoursesByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var coursesEndingToday = courses.Where(course => course.EndDate.Date == today).ToList();

            foreach (var course in coursesEndingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = course.Id + 4000,
                    Title = "COURSE ENDING TODAY!",
                    Description = $"Your term '{course.Name}' is ending today!",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        private async void AssessmentStartDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("AssessmentStartNotifications", e.Value);

            var assessments = await DBService.GetAssessmentsByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var assessmentStartingToday = assessments.Where(assessment => assessment.StartDate.Date == today).ToList();

            foreach (var assessment in assessmentStartingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = assessment.Id + 5000,
                    Title = "ASSESSMENT STARTING TODAY!",
                    Description = $"Your assessment '{assessment.Name}' is starting today!",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        private async void AssessmentEndDateToggle_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("AssessmentEndNotifications", e.Value);

            var assessments = await DBService.GetAssessmentsByUser(UserSession.CurrentUserId);
            var today = DateTime.Today;
            var assessmentEndingToday = assessments.Where(assessment => assessment.EndDate.Date == today).ToList();

            foreach (var assessment in assessmentEndingToday)
            {
                var request = new NotificationRequest()
                {
                    NotificationId = assessment.Id + 6000,
                    Title = "ASSESSMENT ENDING TODAY!",
                    Description = $"Your assessment '{assessment.Name}' is ending today!",
                    BadgeNumber = 1,
                    CategoryType = NotificationCategoryType.Reminder,
                    Schedule = new NotificationRequestSchedule()
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }
    }
}