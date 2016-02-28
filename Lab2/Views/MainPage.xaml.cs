using Newtonsoft.Json;
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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Lab2.Views;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab2
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.BaseUri + "/api/Assignments?UserID=" + App.user.UserID); // sends GET request
                });
                task.Wait(); // Wait
                ObservableCollection<AssignmentDTO> list = JsonConvert.DeserializeObject<ObservableCollection<AssignmentDTO>>(response);
                App.Assignments = list;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.BaseUri + "api/tasks"); // sends GET request
                 });
                task.Wait(); // Wait
                List<Task1> list = JsonConvert.DeserializeObject<List<Task1>>(response);
                list.Sort((x, y) => DateTime.Compare(x.BeginDateTime, y.BeginDateTime));
                taskList.ItemsSource = list;
                App.tasks = list;
            }
        }



        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }


        private void TaskList_Click(object sender, RoutedEventArgs e)
        {
                this.Frame.Navigate(typeof(TaskList));
        }

        private async void taskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task1 selectedTask = taskList.SelectedItem as Task1;
            this.Frame.Navigate(typeof(DetailsView),selectedTask);
        }

        private void DoneTask_Click(object sender, RoutedEventArgs e)
        {
            DateTime current = DateTime.Now;

            taskList.ItemsSource = null;

            List<Task1> list = App.tasks;
            List<Task1> temp = new List<Task1>();
            foreach (Task1 task in list)
            {

                if (task.DeadlineDateTime > current)
                {
                    temp.Add(task);
                }
            }
            taskList.ItemsSource = temp;
        }

        private void TaskNameSearch_Click(object sender, RoutedEventArgs e)
        {
            taskList.ItemsSource = null;

            if (TaskName.Text.Equals(""))
            {
                taskList.ItemsSource = App.tasks;
            }
            else
            {
                List<Task1> list = App.tasks;
                List<Task1> temp = new List<Task1>();
                foreach (Task1 task in list)
                {

                    if (task.Title.Contains(TaskName.Text))
                    {
                        temp.Add(task);
                    }
                }
                taskList.ItemsSource = temp;
            }
        }

        private void TaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            taskList.ItemsSource = null;

            if (TaskName.Text.Equals(""))
            {
                taskList.ItemsSource = App.tasks;
            }
            else
            {
                List<Task1> list = App.tasks;
                List<Task1> temp = new List<Task1>();
                foreach (Task1 task in list)
                {
                    if (task.Title.Contains(TaskName.Text))
                    {
                        temp.Add(task);
                    }
                }
                taskList.ItemsSource = temp;
            }
        }

        private void Filter_button_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = StartPicker.Date.Date;
            DateTime deadline = DeadlinePicker.Date.Date;

            List<Task1> list = App.tasks;
            List<Task1> temp = new List<Task1>();
            foreach (Task1 task in list)
            {
                DateTime tempStart = task.BeginDateTime.Date;
                DateTime tempDeadline = task.DeadlineDateTime.Date;

                if (tempDeadline <= deadline && tempStart >= start)
                {
                    temp.Add(task);
                }
            }
            taskList.ItemsSource = temp;
        }
    }
}
