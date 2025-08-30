using StudentPortal.Pages.NavigationPage;
using StudentPortal.Pages.NavigationPages.HomePageRelated;
namespace StudentPortal.Pages.NavigationPages.LogInPage;

public partial class LogIn : ContentPage
{
	public LogIn()
	{
		InitializeComponent();
	}

    private async void LogInButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomeNavPage());
    }
}