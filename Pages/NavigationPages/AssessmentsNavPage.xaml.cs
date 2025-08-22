using StudentPortal.Services;
using StudentPortal.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StudentPortal.Pages.NavigationPages;


namespace StudentPortal.Pages.NavigationPage
{
    public partial class AssessmentsNavPage : ContentPage
    {
        private ObservableCollection<Assessments> _assessments = new();

        public AssessmentsNavPage()
        {
            InitializeComponent();
            AssessmentCollection.ItemsSource = _assessments;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAssessments();
        }

        private async Task LoadAssessments()
        {
            var assessments = await DBService.GetAssessments();
            _assessments.Clear();
            foreach (var assessment in assessments)
                _assessments.Add(assessment);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var assessmentToEdit = button.CommandParameter as Assessments;
            await Navigation.PushAsync(new EditAssessmentNavPage(assessmentToEdit));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var assessmentToDelete = button?.CommandParameter as Assessments;

            if (assessmentToDelete != null)
            {
                bool userConfirmed = await DisplayAlert
                    (
                        "Delete Term",
                        $"Are you sure you want to delete '{assessmentToDelete.Name}'?",
                        "Delete",
                        "Cancel"
                    );

                if (userConfirmed)
                {
                    await DBService.DeleteAssessment(assessmentToDelete);
                    _assessments.Remove(assessmentToDelete);

                    await DisplayAlert("Success", "Assessment deleted successfully", "OK");
                }
            }
        }

        

        private async void AddAssessmentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessment());
        }
    }
}