using StudentPortal.Pages.NavigationPage;

namespace StudentPortal.Pages.NavigationPages.HomePageRelated;

public partial class HomeNavPage : ContentPage
{
	public HomeNavPage()
	{
		InitializeComponent();
	}

    private async void StudentClassworkButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TermsNavPage());
    }

    private async void navSetNotificationsButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationsNavPage());
    }

    private void ReportButton_Clicked(object sender, EventArgs e)
    {

    }
}