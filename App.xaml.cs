using StudentPortal.Pages.NavigationPage;
using StudentPortal.Pages.NavigationPages.LogInPage;
using StudentPortal.Services;

namespace StudentPortal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogIn())
            {
                BackgroundColor = Colors.LightSteelBlue
            };
            Task.Run(async () => await DBService.InitializeEvaluationData());
            Task.Run(async () => await DBService.InitializeTestUserData());
        }
    }
}
