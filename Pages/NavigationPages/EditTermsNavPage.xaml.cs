namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditTermsNavPage : ContentPage
    {
        private readonly string _termId;

        public EditTermsNavPage()
        {
            InitializeComponent();
        }

        public EditTermsNavPage(string termId)
        {
            InitializeComponent();
            _termId = termId;
        }
    }
}