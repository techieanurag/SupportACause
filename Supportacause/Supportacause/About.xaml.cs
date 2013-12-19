using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Supportacause
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }
    }
}