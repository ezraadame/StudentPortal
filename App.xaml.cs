using StudentPortal.Pages.NavigationPage;

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
        }
    }
}
