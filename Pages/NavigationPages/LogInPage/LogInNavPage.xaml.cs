using StudentPortal.Models;
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

        await LogInButton.ScaleTo(0.95, 100, Easing.CubicIn);
        await LogInButton.ScaleTo(1, 100, Easing.CubicOut);
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
                
                var loggedInUser = await DBService.GetUserByUsername(username);

                if (loggedInUser != null)
                {
                    
                    UserSession.SetCurrentUser(loggedInUser);

                    Application.Current.MainPage = new Microsoft.Maui.Controls.NavigationPage(new HomeNavPage())
                    {
                        BackgroundColor = Colors.LightSteelBlue
                    };

                    System.Diagnostics.Debug.WriteLine($"DEBUG: User signed in: userId {loggedInUser.Id}");
                }
                else
                {
                    await DisplayAlert("Login Failed", "User data could not be retrieved", "OK");
                }
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid username or password", "OK");
                PasswordEntry.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"? INITIALIZATION FAILED AT: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"? STACK TRACE: {ex.StackTrace}");
            System.Diagnostics.Debug.WriteLine($"? INNER EXCEPTION: {ex.InnerException?.Message}");
            await DisplayAlert("Error", "An error occurred during login. Please try again.", "OK");
        }
        
    }
}