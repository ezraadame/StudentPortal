using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class AssessmentsNavPage : ContentPage
    {
        private readonly string _assessmentsId;

        public AssessmentsNavPage()
        {
            InitializeComponent();
        }

        public AssessmentsNavPage(string assessmentsId)
        {
            InitializeComponent();
            _assessmentsId = assessmentsId;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var assessmentId = button.CommandParameter.ToString();
            await Navigation.PushAsync(new EditAssessmentNavPage(assessmentId));
        }
    }
}