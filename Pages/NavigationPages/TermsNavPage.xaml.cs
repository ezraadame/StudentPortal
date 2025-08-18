namespace StudentPortal.Pages.NavigationPage
{
    public partial class TermsNavPage : ContentPage
    {
        private readonly string _termId;

        public TermsNavPage()
        {
            InitializeComponent();
        }

        public TermsNavPage(string termId)
        {
            InitializeComponent();
            _termId = termId;
        }

        private async void navTerm1PageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesNavPage());
        }

        private async void navTermPage2Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesNavPage());
        }

        private async void navTermPage3Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesNavPage());
        }

        private async void navSetNotificationsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationsNavPage());
        }

        private async void addTermButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermsNavPage());
        }

        private async void editTerm_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var termId = button.CommandParameter.ToString();
            // Handle editing for specific term
            await Navigation.PushAsync(new EditTermsNavPage(termId));
        }
    }
}