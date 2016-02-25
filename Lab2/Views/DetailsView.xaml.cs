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
            if (e.Parameter is Task1)
            {
                Task1 user = (Task1)e.Parameter;
                TaksName.Text = user.Title;
                StartTime.Text = user.BeginDateTime.ToString();
                EndTime.Text = user.DeadlineDateTime.ToString();
                Requirements.Text = user.Requirements;

                AssignmentDTO ass = getTaskStatus(user);

            }

        }

        private AssignmentDTO getTaskStatus(Task1 user)
        {
            List<AssignmentDTO> temp = new List<AssignmentDTO>();
            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.BaseUri + "api/Status"); // sends GET request
                });
                task.Wait(); // Wait
                List<AssignmentDTO> list = JsonConvert.DeserializeObject<List<AssignmentDTO>>(response);
                temp = list;
            }
            if (temp.Count == 0)
            {
                return (null);
            }
            else if (temp.Count == 1)
            {
                AssignmentDTO ass = new AssignmentDTO
                {
                    UserID = temp.First().UserID,
                    TaskID = temp.First().TaskID,
                    UserForName = temp.First().UserForName,
                    UserLastName = temp.First().UserLastName,
                    TaskTitle = temp.First().TaskTitle
                };
                return (ass);
            }
            else {
                AssignmentDTO tri = new AssignmentDTO
                {
                    UserForName = null
                };

                return (tri);
            }
        }
    }
            
}
