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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.BaseUri+"api/tasks"); // sends GET request
                 });
                task.Wait(); // Wait
                List<Task1> list = JsonConvert.DeserializeObject<List<Task1>>(response);
                taskList.ItemsSource = list;
            }
        }

        private async void taskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // when we get here the user has selected a customer
            Task1 selectedTask = taskList.SelectedItem as Task1;
            //MessageBox.Show(selectedTask.Title + " is selected");

            var dialog = new MessageDialog(selectedTask.Title + " is selected");
            await dialog.ShowAsync();


            //using (var client = new HttpClient())
            //{
            //    var response = "";
            //    Task task = Task.Run(async () =>
            //    {
            //        response = await client.GetStringAsync(App.BaseUri + "api/tasks/" + selectedTask.TaskID); // sends GET request
            //    });
            //    task.Wait(); // Wait
            //    List<Task1> list = JsonConvert.DeserializeObject<List<Task1>>(response);
            //}
        }
    }
    public class User
    {
        public int UserID { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
    }

    public class Task1
    {
        public int TaskID { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
    }

    public class AssignmentDTO
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string UserForName { get; set; }
        public string UserLastName { get; set; }
        public string TaskTitle { get; set; }


    }
}
