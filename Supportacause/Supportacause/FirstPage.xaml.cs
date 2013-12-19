using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Threading;

namespace Supportacause
{
    public partial class FirstPage : PhoneApplicationPage
    {
        
        private Popup popup;
        private BackgroundWorker backroungWorker;

        
        public FirstPage()
        {
            InitializeComponent();
            Random rnd = new Random();
            int irandom = rnd.Next(1, 6);

            ShellTile appTile = ShellTile.ActiveTiles.First();
            if (appTile != null)
            {
                StandardTileData newTile = new StandardTileData
                {
                    Title = "Support A Cause",
                    BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                    Count = irandom,
                    BackTitle = "Support A Cause",
                    BackBackgroundImage = new Uri("FlipBackground.png", UriKind.Relative),
                    BackContent = "Help various NGO'S."
                };
                appTile.Update(newTile);
            }
            
            ShowSplash();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string lockscreenKey = "WallpaperSettings";
            string lockscreenValue = "0";

            bool lockscreenValueExists = NavigationContext.QueryString.TryGetValue(lockscreenKey, out lockscreenValue);

            if (lockscreenValueExists)
            {
                MessageBox.Show("Wait till App opens.. We will update your Wallpaper thereafter");
                LockHelper("DefaultLockScreen.jpg", true);
            }
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

        private void ShowSplash()
        {
            this.popup = new Popup();
            this.popup.Child = new SplashScreenControl();
            this.popup.IsOpen = true;
            StartLoadingData();
        }

        private void StartLoadingData()
        {
            backroungWorker = new BackgroundWorker();
            backroungWorker.DoWork += new DoWorkEventHandler(backroungWorker_DoWork);
            backroungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backroungWorker_RunWorkerCompleted);
            backroungWorker.RunWorkerAsync();
        }

        void backroungWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //here we can load data
            Thread.Sleep(9000);
        }

        void backroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.popup.IsOpen = false;
                NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
                BTN.Visibility = Visibility.Visible;
            }
            );
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }
    }
}