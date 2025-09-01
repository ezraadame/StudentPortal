using StudentPortal.Models;
using StudentPortal.Pages.NavigationPage;
using StudentPortal.Pages.NavigationPages.ReportsRelated;
using StudentPortal.Services;
using System.Collections.ObjectModel;

namespace StudentPortal.Pages.NavigationPages.HomePageRelated;

public partial class HomeNavPage : ContentPage
{
    private List<Courses> _allCourses = [];
    public ObservableCollection<Courses> SearchResults { get; set; } = [];
    private ObservableCollection<Term> _terms = [];
    public HomeNavPage()
    {
        InitializeComponent();

        SearchResultsView.ItemsSource = SearchResults;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAllCoursesByUser();
    }


    private async Task LoadAllCoursesByUser()
    {
        try
        {
            _allCourses = await DBService.GetCoursesByUser(UserSession.CurrentUserId);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load courses: " + ex.Message, "OK");
        }
    }
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (sender is Grid grid && grid.BindingContext is Courses selectedCourse)
            {
                SearchEntry.Text = string.Empty;
                HideSearchResults();
                await Navigation.PushAsync(new NavigationPage.CourseViewNavPage(selectedCourse.Id));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to open course: " + ex.Message, "OK");
        }

    }
    private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            HideSearchResults();
            return;
        }


        if (searchText.Length < 2)
        {
            HideSearchResults();
            return;
        }
        await PerformSearch(searchText);
    }
    private async Task PerformSearch(string searchText)
    {
        try
        {
            var searchLower = searchText.ToLower();


            var matchingCourses = _allCourses.Where(course =>
            {
                bool nameMatch = course.Name?.ToLower().Contains(searchLower, StringComparison.CurrentCultureIgnoreCase) == true;
                bool instructorMatch = course.InstructorName?.ToLower().Contains(searchLower, StringComparison.CurrentCultureIgnoreCase) == true;
                bool statusMatch = course.Status?.ToLower().Contains(searchLower, StringComparison.CurrentCultureIgnoreCase) == true;

                return nameMatch || instructorMatch || statusMatch;

            }).Take(5);

            var matchingCoursesList = matchingCourses.ToList();



            SearchResults.Clear();
            foreach (var course in matchingCourses)
            {
                SearchResults.Add(course);

            }
            ShowSearchResults();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Search failed: " + ex.Message, "OK");
        }
    }

    private void ShowSearchResults()
    {
        if (SearchResults.Count > 0)
        {
            SearchResultsFrame.IsVisible = true;
            SearchResultsView.IsVisible = true;
            NoResultsLabel.IsVisible = false;
        }
        else
        {
            SearchResultsFrame.IsVisible = false;
            SearchResultsView.IsVisible = false;
            NoResultsLabel.IsVisible = true;
        }
    }

    private void HideSearchResults()
    {
        SearchResultsFrame.IsVisible = false;
        SearchResultsView.IsVisible = false;
        NoResultsLabel.IsVisible = false;
    }
    private async void StudentClassworkButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TermsNavPage());
    }

    private async void NavSetNotificationsButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationsNavPage());
    }

    private async void ReportButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReportsNav());
    }

    private async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        bool confirmLogout = await DisplayAlert(
        "Confirm Logout",
        "Are you sure you want to logout?",
        "Yes",
        "No");

        if (confirmLogout)
        {
            UserSession.Logout();
        }
    }

    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            bool confirmLogout = await DisplayAlert(
                "Exit App",
                "Do you want to logout and exit?",
                "Logout",
                "Stay");

            if (confirmLogout)
            {
                UserSession.Logout();
            }
        });

        return true;
    }
}