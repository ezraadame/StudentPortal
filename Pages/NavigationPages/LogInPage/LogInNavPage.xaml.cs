using StudentPortal.Pages.NavigationPage;
using StudentPortal.Pages.NavigationPages.HomePageRelated;
using StudentPortal.Services;
namespace StudentPortal.Pages.NavigationPages.LogInPage;

public partial class LogIn : ContentPage
{
	public LogIn()
	{
		InitializeComponent();
        
	}

    private async void LogInButton_Clicked(object sender, EventArgs e)
    {

        await SignIn();
    }

    private async Task SignIn()
    {
        string? username = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both username and password", "OK");
            return;
        }
        try
        {
            bool isValidUser = await DBService.ValidateUser(username, password);

            if (isValidUser)
            {
                await Navigation.PushAsync(new HomeNavPage());
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid username or password", "OK");
                PasswordEntry.Text = string.Empty;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "An error occurred during login. Please try again.", "OK");
        }
    }
}