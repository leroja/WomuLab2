using Lab2.Models;
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
    public sealed partial class TaskDetail : Page
    {

        private AssignmentDTO assignment;
        public TaskDetail()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is AssignmentDTO)
            {
                assignment = (AssignmentDTO)e.Parameter;
                Title.Text = "Task title: " + assignment.Title;
                StartTime.Text = "Start time of Task: " + assignment.BeginDateTime.ToString();
                Deadline.Text = "Dedline For Task: " + assignment.DeadlineDateTime.ToString();
                Requirements.Text = "Requirements: " + assignment.Requirements;

                Users.Text = "All Assigned users: \r\n" + String.Join(", ", ((List<string>)assignment.Users).ToArray()); ;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                Task thtask = Task.Run(async () =>
                {
                    await client.DeleteAsync(App.BaseUri + "api/assignments?taskID=" + assignment.TaskID + "&userID=" + App.user.UserID);
                });
                thtask.Wait(); // Wait
            }

            var delTemp = App.Assignments.Where(x => x.TaskID == assignment.TaskID);

            AssignmentDTO del = delTemp.FirstOrDefault();

            App.Assignments.Remove(del);


            this.Frame.GoBack();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
