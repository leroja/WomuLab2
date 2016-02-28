using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab2.Models;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;
using Windows.UI.Popups;

namespace Lab2
{
    public sealed partial class DetailsView : Page
    {
        Task1 user;
        public DetailsView()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> list;
            if (e.Parameter is Task1)
            {
                user= (Task1)e.Parameter;
                TaksName.Text = "Task title: "+user.Title;
                StartTime.Text = "Start time of task: "+user.BeginDateTime.ToString();
                EndTime.Text = "Deadline for task: "+user.DeadlineDateTime.ToString();
                Requirements.Text = "Requirements: "+ user.Requirements;


                using (var client = new HttpClient())
                {
                    var response = "";
                    Task task = Task.Run(async () =>
                    {
                        response = await client.GetStringAsync(App.BaseUri + "/api/Assignments?TaskID=" + user.TaskID); // sends GET request
                    });
                    task.Wait(); // Wait
                    list = JsonConvert.DeserializeObject<List<string>>(response);
                }

                Status.Text = "All assigned users: \r\n" + String.Join(", ", ((List<string>)list).ToArray()); ;

            }

        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = null;

            Assignment test = new Assignment
            {
                TaskID = user.TaskID,
                UserID = App.user.UserID
            };
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(test);

                Task task = Task.Run(async () =>
                {
                    StringContent till = new StringContent(json);
                    response = await client.PostAsync(App.BaseUri + "api/Assignments?UserId=" + App.user.UserID + "&TaskID=" + user.TaskID, till);
                });
                task.Wait();
            }
            if (response.ReasonPhrase.Equals("Not Found"))
            {
                var dialog = new MessageDialog("User already assigned to task");
                await dialog.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            this.Frame.Navigate(typeof(MainPage));
        }
    }
            
}
