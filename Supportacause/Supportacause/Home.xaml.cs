using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Supportacause
{
    public partial class Home : PhoneApplicationPage
    {
        public static string Category="children";
        public Home()
        {
            InitializeComponent();
        }

        private async void LockHelper(string filePathOfTheImage, bool isAppResource)
        {
            try
            {
                var isProvider = Windows.Phone.System.UserProfile.LockScreenManager.IsProvidedByCurrentApplication;
                if (!isProvider)
                {
                    // If you're not the provider, this call will prompt the user for permission.
                    // Calling RequestAccessAsync from a background agent is not allowed.
                    var op = await Windows.Phone.System.UserProfile.LockScreenManager.RequestAccessAsync();

                    // Only do further work if the access was granted.
                    isProvider = op == Windows.Phone.System.UserProfile.LockScreenRequestResult.Granted;
                }

                if (isProvider)
                {
                    // At this stage, the app is the active lock screen background provider.

                    // The following code example shows the new URI schema.
                    // ms-appdata points to the root of the local app data folder.
                    // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package.
                    var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                    var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                    // Set the lock screen background image.
                    Windows.Phone.System.UserProfile.LockScreen.SetImageUri(uri);

                    // Get the URI of the lock screen background image.
                    var currentImage = Windows.Phone.System.UserProfile.LockScreen.GetImageUri();
                    System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
                }
                else
                {
                    MessageBox.Show("You said no, so I can't update your background.");
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }





        private void HealthBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "health";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ChildrenBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "children";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void DisabledBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "disabled";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void EducationBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "education";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ElderlyBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "elderly";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void HumanBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "all";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void EploymentBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "employment";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void EnvironmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "environment";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void WomanBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "women";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void YouthsBtn_Click(object sender, RoutedEventArgs e)
        {
            Category = "youths";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            LockHelper("DefaultLockScreen.jpg", true);
        }

        private async void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            // Launch URI for the lock screen settings screen.
            var op = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));

        }
    }
}