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
    public sealed partial class TaskList : Page
    {
        public TaskList()
        {
            this.InitializeComponent();

            listBox.ItemsSource = App.Assignments;

            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = App.Assignments;
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task1 task;
            AssignmentDTO selectedTask = listBox.SelectedItem as AssignmentDTO;


            if (selectedTask == null)
            {
                return;
            }
            using (var client = new HttpClient())
            {
                var response = "";
                Task thtask = Task.Run(async () =>
                {

                    response = await client.GetStringAsync(App.BaseUri + "api/tasks/"+selectedTask.TaskID); // sends GET request
                });
                thtask.Wait(); // Wait
                task = JsonConvert.DeserializeObject<Task1>(response);
            }

            

            this.Frame.Navigate(typeof(TaskDetail),task);
        }
    }
}
