using Lab2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lab2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var Client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                 {
                     response = await Client.GetStringAsync(App.BaseUri + "api/users");
                 });
                task.Wait();
                List<User> list = JsonConvert.DeserializeObject<List<User>>(response);
                userList.ItemsSource = list;
            }
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User SelectetUser = userList.SelectedItem as User;
            App.user = SelectetUser;
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
