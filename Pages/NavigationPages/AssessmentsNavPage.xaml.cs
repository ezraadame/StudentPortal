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
        private int _courseId;

        public AssessmentsNavPage(int courseId)
        {
            InitializeComponent();
            _courseId = courseId;
            AssessmentCollection.ItemsSource = _assessments;
            DisplayAlert("Debug", $"CoursesNavPage created with courseId: {courseId}", "OK");

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAssessments();
        }

        private async Task LoadAssessments()
        {
            await DisplayAlert("Debug", $"Loading assessments for courseID: {_courseId}", "OK");
            var assessments = await DBService.GetAssessmentsByCourse(_courseId);
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
            await Navigation.PushAsync(new AddAssessment(_courseId));
        }
    }
}