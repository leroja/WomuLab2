﻿using Lab2.Models;
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
    public sealed partial class TaskDetail : Page
    {

        private Task1 task;
        public TaskDetail()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Task1)
            {
                task = (Task1)e.Parameter;
                Title.Text = "Task title: " + task.Title;
                StartTime.Text = "Start time of Task: " + task.BeginDateTime.ToString();
                Deadline.Text = "Dedline For Task: " + task.DeadlineDateTime.ToString();
                Requirements.Text = "Requirements: " + task.Requirements;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = "";
                Task thtask = Task.Run(async () =>
                {
                    
                    await client.DeleteAsync(App.BaseUri + "api/assignments?taskID=" + task.TaskID + "&userID=" + App.user.UserID);
                    //response = await client.GetStringAsync(App.BaseUri + "api/tasks"); // sends GET request
                });
                thtask.Wait(); // Wait
            }

            var delTemp = App.Assignments.Where(x => x.TaskID == task.TaskID && x.UserID == App.user.UserID);

            AssignmentDTO del = delTemp.FirstOrDefault();

            // remove from assignments list

            App.Assignments.Remove(del);


            //this.Frame.GoBack();
            this.Frame.Navigate(typeof(TaskList));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
            //this.Frame.Navigate(typeof(TaskList));
        }
    }
}
