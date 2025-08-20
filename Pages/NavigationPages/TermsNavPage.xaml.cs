using StudentPortal.Models;
using StudentPortal.Services;
using System.Collections.ObjectModel;

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

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void navTermPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesNavPage());
        }
    }
}