using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StudentPortal.Pages.NavigationPage
{
    public partial class TermsNavPage : ContentPage
    {
        //private readonly string _termId;
        private ObservableCollection<Term> _terms = new();

        public TermsNavPage()
        {
            InitializeComponent();
            TermsCollection.ItemsSource = _terms;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTerms();
        }

        private async Task LoadTerms()
        {
            var terms = await DBService.GetTerms();
            _terms.Clear();
            foreach (var term in terms)
                _terms.Add(term);
        }

        private async void navSetNotificationsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationsNavPage());
        }

        private async void addTermButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermsNavPage());
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var termToEdit = button?.CommandParameter as Term;
            await Navigation.PushAsync(new EditTermsNavPage(termToEdit));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var termToDelete = button?.CommandParameter as Term;

            if (termToDelete != null)
            {
                bool userConfirmed = await DisplayAlert(
                    "Delete Term",
                    $"Are you sure you want to delete '{termToDelete.Name}'?",
                    "Delete",
                    "Cancel"
                    );

                if (userConfirmed)
                {
                    await DBService.DeleteTerm(termToDelete);
                    _terms.Remove(termToDelete);

                    await DisplayAlert("Success", "Term deleted successfully", "OK");
                }
            }
        }

        private async void navTermPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesNavPage());
        }
    }
}