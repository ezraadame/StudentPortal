using StudentPortal.Pages.NavigationPage;
using StudentPortal.Services;

namespace StudentPortal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TermsNavPage())
            {
                BackgroundColor = Colors.LightSteelBlue
            };
            Task.Run(async () => await DBService.InitializeEvaluationData());
        }
    }
}
