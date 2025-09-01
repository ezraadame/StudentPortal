using StudentPortal.Pages.NavigationPages.LogInPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Models
{
    public static class UserSession
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUsername { get; set; } = string.Empty;

        public static void SetCurrentUser(Users user)
        {
            CurrentUserId = user.Id;
            CurrentUsername = user.Username ?? string.Empty;
            System.Diagnostics.Debug.WriteLine($"UserSession: Set current user to {user.Id} ({user.Username})");
        }

        public static void Clear()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
        }

        public static async void Logout()
        {
            Clear();
            Application.Current.MainPage = new NavigationPage(new LogIn())
            {
                BackgroundColor = Colors.LightSteelBlue
            };
        }
    }
}
