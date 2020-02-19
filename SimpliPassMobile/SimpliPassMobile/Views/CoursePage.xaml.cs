﻿using System;
using System.Collections.Generic;
using SimpliPassMobile.Models;
using SimpliPassMobile.ViewModels;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;

namespace SimpliPassMobile.Views
{
    public partial class CoursePage : ContentPage
    {
        List<object> courseList;

        public CoursePage(string Name)
        {
            InitializeComponent();
            CurrDept.Text = "Department of " + Name;
            getCourses(Name);
            BindingContext = new CourseViewModel(courseList);
        }

        private void getCourses(string currDept)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(Constants.API_BASE_URL + Constants.COURSE + Constants.DEPARTMENT_COURSES + currDept).Result;
            courseList = JsonConvert.DeserializeObject<List<object>>(response);
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var temp = e.Item as CourseModel;
            await Navigation.PushAsync(new CourseDetailsPage(temp.Id, temp.Name, temp.Department, temp.Difficulty, temp.DifficultyCount, temp.SectionRatings));
        }
    }
}
