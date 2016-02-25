using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab2.Models;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Lab2
{
    public sealed partial class DetailsView : Page
    {
        public DetailsView()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Task1){
                Task1 user = (Task1)e.Parameter;
                TaksName.Text = user.Title;
                StartTime.Text = user.BeginDateTime.ToString();
                EndTime.Text = user.DeadlineDateTime.ToString();
                Requirements.Text = user.Requirements;


            }
        }
    }
}
