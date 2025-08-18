namespace StudentPortal.Pages.NavigationPage
{
    public partial class EditAssessmentNavPage : ContentPage
    {
        private readonly string _assessmentsId;

        public EditAssessmentNavPage()
        {
            InitializeComponent();
        }

        public EditAssessmentNavPage(string assessmentsId)
        {
            InitializeComponent();
            _assessmentsId = assessmentsId;
        }
    }
}